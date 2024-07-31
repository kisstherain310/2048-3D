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
    [SerializeField] public BoardManager boardManager;
    [SerializeField] public ClassicCubeManager classicCubeManager;
    [SerializeField] public JokerCubeManager jokerCubeManager;
    [SerializeField] public BombCubeManager bombCubeManager;
    [SerializeField] public PointCubeManager pointCubeManager;
    [SerializeField] public DataManager dataManager;
    [SerializeField] public LevelManager levelManager;
    [SerializeField] public UIGameLose uiGameLose;
    [HideInInspector] public GameStatus gameStatus;
    [HideInInspector] public BaseCube mainCube = null;
    

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        InitComponents();
    }
    private void Start()
    {
        boardManager.SpawnBoard(levelManager.GetMainLevel());
        if (!dataManager.CheckData()) { // Nếu không có dữ liệu thì khởi tạo lại game
            InitGame();
        } else if(mainCube == null){ // Nếu có dữ liệu nhưng kh có cube ở điểm bắt đầu thì khởi tạo lại mainCube
            classicCubeManager.SpawnClassicCube();
        }
        gameStatus = new GameStatus();
    }
    // ----------------- Init -----------------

    public void InitGame(){
        SetMainCubeNull();
        classicCubeManager.SetParent(levelManager.GetMainLevel());
        jokerCubeManager.SetParent(levelManager.GetMainLevel());
        bombCubeManager.SetParent(levelManager.GetMainLevel());
        classicCubeManager.SpawnClassicCube();
        scoreManager.InitScore();
        gameStatus.OnPlay();
    }
    private void InitComponents()
    {
        InitClassicCubeManager();
        InitBombCubeManager();
        InitJokerCubeManager();
    }
    private void InitClassicCubeManager()
    {
        classicCubeManager.Initialize(listCube, defaultCubeSpawnPoint, levelManager.GetMainLevel());
    }
    private void InitJokerCubeManager()
    {
        jokerCubeManager.Initialize(listCube, defaultCubeSpawnPoint, levelManager.GetMainLevel());
    }
    private void InitBombCubeManager()
    {
        bombCubeManager.Initialize(listCube, defaultCubeSpawnPoint, levelManager.GetMainLevel());
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
    public void UpdateDefaultPosition(Vector3 position)
    {
        classicCubeManager.defaultCubeSpawnPoint.position = position;
        jokerCubeManager.defaultCubeSpawnPoint.position = position;
        bombCubeManager.defaultCubeSpawnPoint.position = position;
    }
    public void SpawnNextCube(){
        nextCube.EditCube(GenerateRandomNumber());
        nextCube.GetComponent<NextCubeMove>().MoveEffect();
    }
    public void DestroyMainCube(){
        ObjectPooler.Instance.ReturnToPool(mainCube.GetComponent<BaseCube>().poolTag, mainCube.gameObject);
        if(mainCube.GetComponent<BaseCube>().poolTag == "ClassicCube") listCube.RemoveCube(mainCube.GetComponent<Cube>());
        listCube.RemoveDataCube(mainCube.GetComponent<BaseCube>());
        SetMainCubeNull();
    }
    public void SetMainCubeNull(){
        mainCube = null;
    }
    public void GameOver(){
        uiGameLose.Show();
        gameStatus.OnGameOver();
    }
    
    public void OnPlay(){
        uiGameLose.Hide();
        levelManager.OnReplay();
    }
}
