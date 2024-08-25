using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractiveObject : StageObject, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] ReferenceInteractiveDictionary targetObjects;
    [SerializeField] public List<InteractiveObject> dismantleResults;

    [SerializeField] Sprite wordSprite;
    [SerializeField] Sprite imgSprite;

    private RectTransform rectTransform;
    private Canvas canvas;
    private GraphicRaycaster raycaster;
    private Image img;

    private Vector3 orgPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.FindObjectOfType<Canvas>();
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        wordSprite = GetComponent<Image>().sprite;
        img = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        orgPos = gameObject.transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        List<RaycastResult> raycastResults = GetEventSystemRaycastResults();
        if (imgSprite != null && IsPointerOverUIElement(raycastResults, "ChangeImgArea"))
        {
            img.sprite = imgSprite;
        }
        else if (wordSprite != null && IsPointerOverUIElement(raycastResults, "ChangeWordArea"))
        {
            img.sprite = wordSprite;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = rectTransform.position;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == gameObject) { continue; }
            //Debug.Log("Overlap detected with: " + result.gameObject.name);
            if(!result.gameObject.TryGetComponent<StageObject>(out var stageObject)) { continue; }
            stageObject.Touch(this);
            if (!targetObjects.TryGetValue(stageObject.ID, out var actions))
            {
                BackToOrgPos();
                return;
            }

            foreach (var action in actions)
            {
                switch (action.InteractionType)
                {
                    case InteractionType.Merge:
                        {
                            levelManager.Merge(gameObject, result.gameObject, action.Object);
                            if (!string.IsNullOrEmpty(action.StringData))
                            {
                                levelManager.Dialog(action.StringData);
                            }
                        }
                        break;
                    case InteractionType.Pass:
                        {
                            levelManager.Pass(this);
                        }break;
                    case InteractionType.Fail:
                        {
                            levelManager.Fail();
                        }break;
                    case InteractionType.Dialog:
                        {
                            levelManager.Dialog(action.StringData);
                            BackToOrgPos();
                        }
                        break;
                    case InteractionType.DeleteSelf:
                        {
                            if (!string.IsNullOrEmpty(action.StringData))
                            {
                                levelManager.Dialog(action.StringData);
                            }
                            levelManager.CheckAndRemoveObject(this);
                        } break;
                    case InteractionType.AddObject:
                        {
                            if (!string.IsNullOrEmpty(action.StringData))
                            {
                                levelManager.Dialog(action.StringData);
                            }
                            levelManager.CheckAndAddObjectInInventory(action.Object);
                            BackToOrgPos();
                        }
                        break;
                }
            }
            return;
        }
        BackToOrgPos();
    }

    public void BackToOrgPos()
    {
        transform.DOLocalMove(orgPos, .1f);
        img.sprite = wordSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(dismantleResults.Count == 0) { return; }
        if (eventData.clickCount < 2) { return; }
        levelManager.Dismantle(this, dismantleResults);
    }

    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults, string layerName)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];

            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer(layerName))
                return true;
        }

        return false;
    }

    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        return raysastResults;
    }
}
