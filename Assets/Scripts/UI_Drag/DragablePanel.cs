using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragablePanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float m_Speed = 0.1f;
    [SerializeField] private List<GameObject> m_SnapAnchors = new();
    [SerializeField] private float m_DistanceThreshold;
    [SerializeField] private Vector3 m_OriginalPosition;

    private void Awake()
    {
        m_OriginalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //snap to closest anchor or original position
        StopAllCoroutines();
        
        var closestDistance = float.MaxValue;
        Vector3 closestAnchorPos = m_OriginalPosition;

        foreach(var anchor in m_SnapAnchors)
        {
            var distance = Vector3.Distance(transform.position, anchor.transform.position);
            if( distance < closestDistance )
            {
                closestDistance = distance;
                closestAnchorPos = anchor.transform.position;
            }
        }

        m_OriginalPosition = closestAnchorPos;

        StartCoroutine(UpdatePosition(m_OriginalPosition));
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pointerPos = eventData.pointerCurrentRaycast.worldPosition;

        if(pointerPos == Vector3.zero)
            return;

        StopAllCoroutines();
        StartCoroutine(UpdatePosition(pointerPos));
    }

    IEnumerator UpdatePosition(Vector3 newPosition)
    {
        while(Vector3.Distance(transform.position, newPosition) > m_DistanceThreshold)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, m_Speed);
            yield return null;
        }

        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SnapAnchor"))
        {
            m_SnapAnchors.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(m_SnapAnchors.Contains(collision.gameObject))
        {
            m_SnapAnchors.Remove(collision.gameObject);
        }
    }
}
