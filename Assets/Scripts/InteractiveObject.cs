using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private string id;
    public string ID => id;
    [SerializeField] ReferenceInteractiveDictionary targetObjects;

    private RectTransform rectTransform;
    private Canvas canvas;
    private GraphicRaycaster raycaster;

    private Vector3 orgPos;
    private LevelManager levelManager;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        raycaster = canvas.GetComponent<GraphicRaycaster>();
    }

    private void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        orgPos = gameObject.transform.position;
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
            if(!result.gameObject.TryGetComponent<InteractiveObject>(out var interactiveObject)) { continue; }
            if (!targetObjects.TryGetValue(interactiveObject.ID, out var actions))
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
                        }
                        break;
                }

            }
            return;
        }
        backToOrgPos();
    }

    private void backToOrgPos()
    {
        transform.DOMove(orgPos, .1f);
    }
}
