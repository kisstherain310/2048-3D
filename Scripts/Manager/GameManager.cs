using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform defaultCubeSpawnPoint;
    public ListCube listCube;
    public Cube nextCube;
    public ScoreManager scoreManager;
    public BoardManager boardManager;
    public MoveManager moveManager;
    public ClassicCubeManager classicCubeManager;
    public JokerCubeManager jokerCubeManager;
    public BombCubeManager bombCubeManager;
    public PointCubeManager pointCubeManager;
    public DataManager dataManager;
    public LevelManager levelManager;
    public UIEvent uIEvent;
    public UIGameLose uiGameLose;
    public GameStatus gameStatus;
    [HideInInspector] public BaseCube mainCube = null;


    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;

        ObjectPooler.Instance.Initialize();
        InitManagers();
    }
    private void Start()
    {
        // dataManager.LoadGameState();
        boardManager.SpawnBoard(levelManager.GetMainLevel());
        if (!dataManager.CheckData()) InitGame();
        else if (mainCube == null) classicCubeManager.SpawnClassicCube(); // Nếu có dữ liệu nhưng kh có cube ở điểm bắt đầu thì khởi tạo lại mainCube
    }
    // ----------------- Init -----------------

    public void InitGame()
    {
        SetMainCubeNull();
        defaultCubeSpawnPoint.position = new Vector3(0, -0.04999993f, 0);
        classicCubeManager.SetParent(levelManager.GetMainLevel());
        jokerCubeManager.SetParent(levelManager.GetMainLevel());
        bombCubeManager.SetParent(levelManager.GetMainLevel());
        classicCubeManager.SpawnClassicCube();
        gameStatus.OnPlay();
    }
    private void InitManagers()
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
        Vector3 newPosition = new Vector3(position.x, -0.04999993f, 0);
        classicCubeManager.defaultCubeSpawnPoint.position = newPosition;
        jokerCubeManager.defaultCubeSpawnPoint.position = newPosition;
        bombCubeManager.defaultCubeSpawnPoint.position = newPosition;
    }
    public void SpawnNextCube()
    {
        nextCube.EditCube(GenerateRandomNumber());
        nextCube.GetComponent<NextCubeMove>().MoveEffect();
    }
    public void SetMainCube(BaseCube cube)
    {
        mainCube = cube;
        moveManager.SetCube(cube);
    }
    public void DestroyMainCube()
    {
        ObjectPooler.Instance.ReturnToPool(mainCube.GetComponent<BaseCube>().poolTag, mainCube.gameObject);
        if (mainCube.GetComponent<BaseCube>().poolTag == "ClassicCube") listCube.RemoveCube(mainCube.GetComponent<Cube>());
        listCube.RemoveDataCube(mainCube.GetComponent<BaseCube>());
        SetMainCubeNull();
    }
    public void SetMainCubeNull()
    {
        mainCube = null;
    }
    public void GameOver()
    {
        uiGameLose.Show();
        gameStatus.OnGameOver();
        scoreManager.EditScore();
    }

    public void OnPlay()
    {
        uiGameLose.Hide();
        levelManager.OnReplay();
    }
}
