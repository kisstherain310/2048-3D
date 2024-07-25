using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public ListCube listCube;
    [SerializeField] public Cube nextCube;
    [SerializeField] private Transform defaultCubeSpawnPoint;
    [SerializeField] public ClassicCubeManager classicCubeManager;
    [SerializeField] public JokerCubeManager jokerCubeManager;
    [SerializeField] public BombCubeManager bombCubeManager;
    [SerializeField] public PointCubeManager pointCubeManager;

    [HideInInspector] public BaseCube mainCube = null;

    private void Awake()
    {
        Instance = this;
    }
    // ----------------- Init -----------------
    private void Start()
    {
        InitClassicCubeManager();
        InitBombCubeManager();
        InitJokerCubeManager();
        SpawnNextCube();
    }
    private void InitClassicCubeManager()
    {
        classicCubeManager.Initialize(listCube, defaultCubeSpawnPoint);
        classicCubeManager.SpawnClassicCube();
    }
    private void InitJokerCubeManager()
    {
        jokerCubeManager.Initialize(listCube, defaultCubeSpawnPoint);
    }
    private void InitBombCubeManager()
    {
        bombCubeManager.Initialize(listCube, defaultCubeSpawnPoint);
    }
    public void SpawnNextCube(){
        nextCube.EditCube(GenerateRandomNumber());
        nextCube.GetComponent<NextCubeMove>().MoveEffect();
    }
    // ----------------- Helper Method -----------------
    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 7));
    }
    public void DestroyMainCube()
    {
        ObjectPooler.Instance.ReturnToPool(mainCube.GetComponent<BaseCube>().poolTag, mainCube.gameObject);
        MainCubeIsNull();
    }
    public void MainCubeIsNull()
    {
        mainCube = null;
    }
}
