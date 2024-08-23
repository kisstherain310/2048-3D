using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] public float maxPosx = 1.63f;
    protected Vector3 offset;
    private BaseCube baseCube;
    [HideInInspector] public bool isActive = false;
    // ---- Event System --------------------------------
    public void SetCube(BaseCube baseCube)
    {
        this.baseCube = baseCube;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isActive || !baseCube.isMainCube) return;
        offset = baseCube.transform.position - GetMouseWorldPos();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isActive || !baseCube.isMainCube) return;
        Vector3 newPos = (GetMouseWorldPos() + offset);
        if (newPos.x > maxPosx) newPos.x = maxPosx;
        if (newPos.x < -maxPosx) newPos.x = -maxPosx;
        baseCube.transform.position = new Vector3(newPos.x, baseCube.transform.position.y, baseCube.transform.position.z);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isActive || !baseCube.isMainCube) return;
        baseCube.handlePointerUp();
    }
    // ---- Helper Methods --------------------------------
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(baseCube.transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
