using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private static int ID = 1;
    [SerializeField] public CubeUI cubeUI;
    [SerializeField] public CubeX2Move cubeX2Move;
    [SerializeField] public GameObject Line;
    [SerializeField] private float pushForce = 20f;
    [SerializeField] private float maxPosx = 1.63f;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public int cubeNumber;   
    [SerializeField] public bool isMainCube = true;
    [HideInInspector] public int CubeID;
    private Vector3 offset;
    private bool isDragging = false; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float getMaxPosx()
    {
        return maxPosx;
    }
    public void EditCube(int number)
    {
        CubeID = ID++;
        cubeNumber = number;
        cubeUI.EditCube(number);
    }
    private void Update()
    {
        if(!isMainCube) return;
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }
        else if (Input.GetMouseButton(0))
        {
            OnPointerDrag(0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnPointerUp();
        }
    }
    private void OnPointerDown()
    {
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }
    
    private void OnPointerDrag(float dragDelta)
    {
       
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
    
    private void OnPointerUp()
    {
        isDragging = false;
        SetMainCube(false);
        SetActiveLine(false);
        AddForce();
        GameManager.Instance.classicCubeManager.SpawnClassicCube();
    }

    public void SetMainCube(bool isMainCube)
    {
        this.isMainCube = isMainCube;
    }
    public void SetActiveLine(bool isActive)
    {
        Line.SetActive(isActive);
    }
    private void AddForce()
    {
        rb.AddForce (Vector3.forward * pushForce, ForceMode.Impulse) ;
    }
}
