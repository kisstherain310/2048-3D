using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCube : MonoBehaviour
{
    public string poolTag;
    [SerializeField] public GameObject line;
    [SerializeField] private float pushForce = 20f;
    [SerializeField] public bool isMainCube = true;
    [SerializeField] public InitEffect initEffect;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool isActive = false;
    [HideInInspector] public int CubeID;

    protected abstract void SetPoolTag();
    protected abstract void InitCubeMove();
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SetPoolTag();
        InitCubeMove();
    }

    // ---- Event System --------------------------------
    public virtual void handlePointerUp()
    {
        SetActive(true);
        SetMainCube(false);
        SetActiveLine(false);
        ApplyPushForce();
        SpawnNewCube();
    }
    public int GetCubeID()
    {
        return  CubeID;
    }
    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
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
        GameManager.Instance.MainCubeIsNull();
        GameManager.Instance.vibrationManager.VibratePhone();
    }
}
