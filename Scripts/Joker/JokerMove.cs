using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JokerMove : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] public float maxPosx = 1.63f;
    private Vector3 offset;
    private bool isDragging = false; 

    private JokerCube jokerCube;

    public void getCube(JokerCube cube)
    {
        this.jokerCube = cube;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!jokerCube.isMainCube) return;
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
       if(!jokerCube.isMainCube) return;
        if (isDragging)
        {
            Vector3 newPos = GetMouseWorldPos() + offset;
            if(newPos.x > maxPosx)newPos.x = maxPosx;
            if(newPos.x < -maxPosx) newPos.x = -maxPosx;
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if(!jokerCube.isMainCube) return;
        isDragging = false;
        jokerCube.handlePointerUp();
    }
}
