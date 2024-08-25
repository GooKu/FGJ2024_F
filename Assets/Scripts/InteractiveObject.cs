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

    private RectTransform rectTransform;
    private Canvas canvas;
    private GraphicRaycaster raycaster;

    private Vector3 orgPos;
    private LevelManagerBase levelManager;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.FindObjectOfType<Canvas>();
        raycaster = canvas.GetComponent<GraphicRaycaster>();
    }

    private void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManagerBase>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        orgPos = gameObject.transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
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
            if (!targetObjects.TryGetValue(stageObject.ID, out var actions))
            {
                backToOrgPos();
                return;
            }

            foreach (var action in actions)
            {
                switch (action.InteractionType)
                {
                    case InteractionType.Merge:
                        {
                            levelManager.Merge(gameObject, result.gameObject, action.Object);
                        }break;
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
                        }break;
                }
            }
            return;
        }
        backToOrgPos();
    }

    private void backToOrgPos()
    {
        transform.DOLocalMove(orgPos, .1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(dismantleResults.Count == 0) { return; }
        if (eventData.clickCount < 2) { return; }
        levelManager.Dismantle(this, dismantleResults);
    }
}
