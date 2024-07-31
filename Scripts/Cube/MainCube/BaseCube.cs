using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseCube : MonoBehaviour
{
    public static int ID = 1;
    public string poolTag;
    [SerializeField] public GameObject line;
    [SerializeField] public Trail trail;
    [SerializeField] private float pushForce = 20f;
    [SerializeField] public bool isMainCube = true;
    [SerializeField] public InitEffect initEffect;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public int CubeID;

    private void SetID()
    {
        CubeID = BaseCube.ID++;
    }
    protected abstract void SetPoolTag();
    protected abstract void InitCubeMove();
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SetID();
        SetPoolTag();
        InitCubeMove();
    }
    // ---- Event System --------------------------------
    public virtual void handlePointerUp()
    {
        StartCoroutine(TrailAction());
        SetMainCube(false);
        SetActiveLine(false);
        ApplyPushForce();
        SpawnNewCube();
        UpdateDefaultPosition();
    }
    IEnumerator TrailAction()
    {
        trail.OnTrail();
        yield return new WaitForSeconds(1f);
        trail.OffTrail();
    }
    public void OffTrail()
    {
        trail.OffTrail();
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
        rb.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
    }
    private void SpawnNewCube()
    {
        GameManager.Instance.classicCubeManager.SpawnClassicCube();
        GameManager.Instance.SetMainCubeNull();
        VibrationManagerX.Vibrate();
    }
    private void UpdateDefaultPosition(){
        GameManager.Instance.UpdateDefaultPosition(transform.position);
    }
}
