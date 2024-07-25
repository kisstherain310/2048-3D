using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public ListCube listCube;
    [SerializeField] public Cube nextCube;
    [SerializeField] private Transform defaultCubeSpawnPoint;
    [SerializeField] public ScoreManager scoreManager;
    [SerializeField] public VibrationManager vibrationManager;
    [SerializeField] public ClassicCubeManager classicCubeManager;
    [SerializeField] public JokerCubeManager jokerCubeManager;
    [SerializeField] public BombCubeManager bombCubeManager;
    [SerializeField] public PointCubeManager pointCubeManager;
    [SerializeField] public DataManager dataManager;

    [HideInInspector] public BaseCube mainCube = null;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        InitComponents();
    }
    private void Start()
    {
        if (!dataManager.haveData) classicCubeManager.SpawnClassicCube();
        SpawnNextCube();
    }
    // ----------------- Init -----------------
    private void InitComponents()
    {
        InitScoreManager();
        InitClassicCubeManager();
        InitBombCubeManager();
        InitJokerCubeManager();
    }
    private void InitScoreManager()
    {
        scoreManager.InitScore();
    }
    private void InitClassicCubeManager()
    {
        classicCubeManager.Initialize(listCube, defaultCubeSpawnPoint);
    }
    private void InitJokerCubeManager()
    {
        jokerCubeManager.Initialize(listCube, defaultCubeSpawnPoint);
    }
    private void InitBombCubeManager()
    {
        bombCubeManager.Initialize(listCube, defaultCubeSpawnPoint);
    }
    public void SpawnNextCube()
    {
        nextCube.EditCube(GenerateRandomNumber());
        nextCube.GetComponent<NextCubeMove>().MoveEffect();
    }
    // ----------------- Helper Method -----------------
    public int GenerateRandomNumber() // 3 cube liên tiếp không được giống nhau
    {
        int number = (int)Mathf.Pow(2, Random.Range(1, 7));
        while (number == classicCubeManager.lastNumber || number == classicCubeManager.lastOfLastNumber)
        {
            number = (int)Mathf.Pow(2, Random.Range(1, 7));
        }
        classicCubeManager.lastOfLastNumber = classicCubeManager.lastNumber;
        classicCubeManager.lastNumber = number;
        return number;
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
