using UnityEngine;
using UnityEngine.EventSystems;

public class DragablePanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 pickOffset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        pickOffset = transform.position - eventData.pointerPressRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        pickOffset = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pointerPos = eventData.pointerCurrentRaycast.worldPosition;

        if(pointerPos == Vector3.zero)
            return;

        transform.position = pointerPos + pickOffset;
    }
}
