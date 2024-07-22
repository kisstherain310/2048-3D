using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCube : MonoBehaviour
{
    [SerializeField] public GameObject line;
    [SerializeField] private float pushForce = 20f;
    [SerializeField] public bool isMainCube = true;
    [HideInInspector] public Rigidbody rb;

    protected abstract void InitCubeMove();
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        InitCubeMove();
    }

    // ---- Event System --------------------------------
    public virtual void handlePointerUp()
    {
        SetMainCube(false);
        SetActiveLine(false);
        ApplyPushForce();
        SpawnNewCube();
    }
    public virtual void SetMainCube(bool isMainCube)
    {
        this.isMainCube = isMainCube;
    }
    public virtual void SetActiveLine(bool isActive)
    {
        line.SetActive(isActive);
    }
    private void ApplyPushForce()
    {
        rb.AddForce (Vector3.forward * pushForce, ForceMode.Impulse) ;
    }
    private void SpawnNewCube(){
        GameManager.Instance.classicCubeManager.SpawnClassicCube();
    }
}
