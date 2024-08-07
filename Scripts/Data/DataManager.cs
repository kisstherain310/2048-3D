using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public string GameState
    {
        get { return PlayerPrefs.GetString("GameState", ""); }
        set { PlayerPrefs.SetString("GameState", value); }
    }
    public string UserData
    {
        get { return PlayerPrefs.GetString("UserData", ""); }
        set { PlayerPrefs.SetString("UserData", value); }
    }
    public GameState gameState;
    public UserData userData;
    private List<int> listScore;
    private List<InforClassicCube> inforClassicCubes;
    private List<InforSpecialCube> inforSpecialCubes;
    private InforClassicCube nextCube;
    private int score;
    private int highScore;
    private int countBombCube;
    private int countJokerCube;
    private bool isVibrate;
    private bool isSound;
    private bool isMusic;
    private int indexMainBg;
    private int maxCube;
    private List<BaseCube> dataCubes;
    private bool haveData = false;

    public void SaveGameState()
    {
        SaveCube();
        SaveNextCube();
        SaveScore();
        SaveCountCube();
        SaveStateSetting();
        GameState gameState = new GameState
        {
            listCubes = inforClassicCubes,
            listSpecialCubes = inforSpecialCubes,
            nextCube = nextCube,
            score = score,
            highScore = highScore,
            countBombCube = countBombCube,
            countJokerCube = countJokerCube,
            isVibrate = isVibrate,
            isSound = isSound,
            isMusic = isMusic,
            indexMainBg = indexMainBg,
            maxCube = maxCube,
        };

        GameState = JsonUtility.ToJson(gameState);
    }
    public void SaveScoreUser(){
        if(listScore == null){
            listScore = new List<int>();
        }
        listScore.Add(GameManager.Instance.scoreManager.score);
        Generate.instance.ProcessUserData();
        for(int i = 0; i < listScore.Count; i++){
            Debug.Log(listScore[i]);
        }
        UserData userData = new UserData
        {
            listScore = listScore,
        };
        UserData = JsonUtility.ToJson(userData);
    }

    public void LoadGameState()
    {
        if (GameState.Length > 0)
        {
            haveData = true;
            gameState = JsonUtility.FromJson<GameState>(GameState);
            if (gameState.listCubes == null && gameState.listSpecialCubes == null)
            {
                haveData = false;
                return;
            }
            LoadCube(gameState);
            LoadSpecialCube(gameState);
            LoadNextCube(gameState);
            LoadScore(gameState);
            LoadCountCube(gameState);
            LoadStateSetting(gameState);
        }
        else haveData = false;
        if(UserData.Length > 0){
            userData = JsonUtility.FromJson<UserData>(UserData);
            listScore = userData.listScore;
        }
    }
    private void SaveCube()
    {
        inforClassicCubes = new List<InforClassicCube>();
        inforSpecialCubes = new List<InforSpecialCube>();
        dataCubes = GameManager.Instance.listCube.dataCubes;
        for(int i = 0; i < dataCubes.Count; i++)
        {
            if (dataCubes[i].poolTag == "ClassicCube")
            {
                InforClassicCube inforCube = new InforClassicCube
                {
                    number = dataCubes[i].GetComponent<Cube>().cubeNumber,
                    position = dataCubes[i].transform.position,
                    rotation = dataCubes[i].transform.rotation,
                    isMainCube = dataCubes[i].isMainCube,
                };
                inforClassicCubes.Add(inforCube);
            }
            else
            {
                InforSpecialCube inforCube = new InforSpecialCube
                {
                    tag = dataCubes[i].poolTag,
                    position = dataCubes[i].transform.position,
                    rotation = dataCubes[i].transform.rotation,
                    isMainCube = dataCubes[i].isMainCube,
                };
                inforSpecialCubes.Add(inforCube);
            }
        }
        maxCube = GameManager.Instance.classicCubeManager.maxCube;
    }
    private void SaveNextCube()
    {
        nextCube = new InforClassicCube
        {
            number = GameManager.Instance.nextCube.cubeNumber,
            position = GameManager.Instance.nextCube.transform.position,
            rotation = GameManager.Instance.nextCube.transform.rotation,
            isMainCube = GameManager.Instance.nextCube.isMainCube,
        };
    }
    private void SaveScore()
    {
        score = GameManager.Instance.scoreManager.score;
        highScore = GameManager.Instance.scoreManager.highScore;
    }
    private void SaveCountCube(){
        countBombCube = GameManager.Instance.uIEvent.eventBombCube.countBombCube;
        countJokerCube = GameManager.Instance.uIEvent.eventJokerCube.countJokerCube;
    }
    private void SaveStateSetting(){
        isVibrate = SoundManager.instance.isVibrate;
        isSound = SoundManager.instance.isSound;
        isMusic = SoundManager.instance.isMusic;
        indexMainBg = BgManager.instance.indexMainBg;
    }
    private void LoadCube(GameState gameState)
    {
        for(int i = 0; i < gameState.listCubes.Count; i++)
        {
            Vector3 position = Utilities.toVector3(gameState.listCubes[i].position.x, gameState.listCubes[i].position.y, gameState.listCubes[i].position.z);
            Quaternion rotation = Utilities.toQuaternion(gameState.listCubes[i].rotation.x, gameState.listCubes[i].rotation.y, gameState.listCubes[i].rotation.z, gameState.listCubes[i].rotation.w);
            GameManager.Instance.classicCubeManager.SpawnCube(gameState.listCubes[i].number, position, rotation, gameState.listCubes[i].isMainCube);
        }
        GameManager.Instance.classicCubeManager.maxCube = gameState.maxCube;
    }
    private void LoadSpecialCube(GameState gameState)
    {
        for(int i = 0; i < gameState.listSpecialCubes.Count; i++)
        {
            string tag = gameState.listSpecialCubes[i].tag;
            Vector3 position = Utilities.toVector3(gameState.listSpecialCubes[i].position.x, gameState.listSpecialCubes[i].position.y, gameState.listSpecialCubes[i].position.z);
            Quaternion rotation = Utilities.toQuaternion(gameState.listSpecialCubes[i].rotation.x, gameState.listSpecialCubes[i].rotation.y, gameState.listSpecialCubes[i].rotation.z, gameState.listSpecialCubes[i].rotation.w);
            if (tag == "BombCube") GameManager.Instance.bombCubeManager.SpawnCube(tag, position, rotation, gameState.listSpecialCubes[i].isMainCube);
            if (tag == "JokerCube") GameManager.Instance.jokerCubeManager.SpawnCube(tag, position, rotation, gameState.listSpecialCubes[i].isMainCube);
        }
    }
    private void LoadNextCube(GameState gameState)
    {
        GameManager.Instance.nextCube.EditCube(gameState.nextCube.number);
    }
    private void LoadScore(GameState gameState)
    {
        GameManager.Instance.scoreManager.SetScore(gameState.score, gameState.highScore);
    }
    private void LoadCountCube(GameState gameState){
        GameManager.Instance.uIEvent.eventBombCube.SetCount(gameState.countBombCube);
        GameManager.Instance.uIEvent.eventJokerCube.SetCount(gameState.countJokerCube);
    }
    private void LoadStateSetting(GameState gameState){
        SoundManager.instance.SetState(gameState.isVibrate, gameState.isSound, gameState.isMusic);
        GameManager.Instance.uIEvent.RestoreSetting();
        BgManager.instance.ChangeToNewSprite(gameState.indexMainBg);
    }
    public void ClearData()
    {
        GameState = "";
    }
    public bool CheckData()
    {
        return GameState.Length > 0;
    }
}
