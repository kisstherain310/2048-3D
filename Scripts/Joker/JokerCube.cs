using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerCube : MonoBehaviour
{
    [SerializeField] public GameObject Line;
    [SerializeField] private float pushForce = 20f;
    [SerializeField] private JokerMove cubeMove;
    [HideInInspector] public Rigidbody rb;
    [SerializeField] public bool isMainCube = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cubeMove.getCube(this);
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
