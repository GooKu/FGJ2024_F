using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private string id;
    public string ID => id;
    [SerializeField] ReferenceInteractiveDictionary targetObjects;
    [SerializeField] List<InteractiveData> interactives;

    private RectTransform rectTransform;
    private Canvas canvas;

    private Vector3 orgPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
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
        //TODO: check reference
        transform.DOMove(orgPos, .1f);
    }
}
