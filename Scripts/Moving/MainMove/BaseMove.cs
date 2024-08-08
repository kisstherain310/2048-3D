using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseMove : MonoBehaviour, 
IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] public float maxPosx = 1.63f;
    protected Vector3 offset;
    protected bool isDragging = false;
    // ---- Event System --------------------------------
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!IsMainCube()) return;
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        if (!IsMainCube()) return;
        if (isDragging)
        {
            Vector3 newPos = (GetMouseWorldPos() + offset) * 1.33f;
            if (newPos.x > maxPosx) newPos.x = maxPosx;
            if (newPos.x < -maxPosx) newPos.x = -maxPosx;
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
        }
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (!IsMainCube()) return;
        isDragging = false;
        HandlePointerUp();
    }
    // ---- Helper Methods --------------------------------
    protected Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    protected abstract bool IsMainCube();
    protected abstract void HandlePointerUp();
}
