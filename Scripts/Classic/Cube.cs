using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private static int ID = 1;
    [SerializeField] public CubeUI cubeUI;
    [SerializeField] public CubeMove cubeMove;
    [SerializeField] public CubeX2Move cubeX2Move;
    [SerializeField] public SpawnEffect spawnEffect;
    [SerializeField] public GameObject Line;
    [SerializeField] private float pushForce = 20f;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public int cubeNumber;   
    [SerializeField] public bool isMainCube = true;
    [HideInInspector] public int CubeID;
    private float maxPosx = 1.63f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cubeMove.getCube(this);
    }

    public float getMaxPosx()
    {
        return cubeMove.maxPosx;
    }
    public void EditCube(int number)
    {
        CubeID = ID++;
        cubeNumber = number;
        cubeUI.EditCube(number);
    }

    public void handlePointerUp()
    {
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
