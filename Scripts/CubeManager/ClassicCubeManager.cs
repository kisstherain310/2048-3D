using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClassicCubeManager : MonoBehaviour
{
    private ListCube listCube;
    public Transform defaultCubeSpawnPoint;
    public int maxCube = 64;
    private GameObject cubeParent;

    public void Initialize(ListCube listCube, Transform defaultCubeSpawnPoint, GameObject cubeParent)
    {
        this.listCube = listCube;
        this.defaultCubeSpawnPoint = defaultCubeSpawnPoint;
        this.cubeParent = cubeParent;
    }
    public void SetParent(GameObject cubeParent)
    {
        this.cubeParent = cubeParent;
    }
    public void InitClassicCube()
    {
        int number = 2;
        if (GameManager.Instance.nextCube.cubeNumber > 0) number = GameManager.Instance.nextCube.cubeNumber;
        Cube newCube = CreateNewCube(defaultCubeSpawnPoint.position, true, number);
        newCube.initEffect.growEffect();
        GameManager.Instance.SetMainCube(newCube);
        SoundManager.instance.PlayClip(AudioType.BoxShot);
    }
    public Cube SpawnCubeX2(Vector3 spawnPoint, int number)
    {
        Cube newCube = CreateNewCube(spawnPoint, false, number);
        return newCube;
    }
    // ----------------- Helper Method -----------------
    public void InitializeCube()
    {
        InitClassicCube();
        GameManager.Instance.SpawnNextCube();
        GameManager.Instance.dataManager.SaveGameState();
    }
    public void SpawnClassicCube()
    {
        StartCoroutine(ISpawnClassicCube());
    }
    IEnumerator ISpawnClassicCube()
    {
        float deltaTime = 0f;
        while(deltaTime < 0.2f)
        {
            deltaTime += Time.deltaTime;
            GameManager.Instance.moveManager.isActive = false;
            yield return null;
        }
        GameManager.Instance.moveManager.isActive = true;
        if (GameManager.Instance.mainCube == null && GameManager.Instance.gameStatus.IsPlaying()) InitializeCube();
    }
    // ----------------- Create and Destroy -----------------
    private Cube CreateNewCube(Vector3 position, bool condition, int number)
    {
        Cube newCube = ObjectPooler.Instance.SpawnFromPool("ClassicCube", position, Quaternion.identity).GetComponent<Cube>();
        newCube.SetMainCube(condition);
        newCube.SetActiveLine(condition);
        newCube.trail.OffTrail();
        newCube.EditCube(number);
        if(number > maxCube){
            maxCube = number;
            Invoke("OpenNotiCube", 1f);
        }

        listCube.AddCube(newCube);
        listCube.AddDataCube(newCube);
        newCube.transform.SetParent(cubeParent.transform);
        return newCube;
    }
    private void OpenNotiCube() => GameManager.Instance.uIEvent.eventButton.OpenNotiCube();

    public void SpawnCube(int number, Vector3 position, Quaternion rotation, bool isMainCube)
    {
        Cube newCube = ObjectPooler.Instance.SpawnFromPool("ClassicCube", position, rotation).GetComponent<Cube>();
        newCube.SetMainCube(isMainCube);
        if (isMainCube)
        {
            newCube.SetActiveLine(true);
            GameManager.Instance.SetMainCube(newCube);
        }
        else newCube.SetActiveLine(false);
        newCube.trail.OffTrail();
        newCube.EditCube(number);

        listCube.AddCube(newCube);
        listCube.AddDataCube(newCube);
        newCube.transform.SetParent(cubeParent.transform);
    }

    public void DestroyCube(Cube cube)
    {
        cube.spawnEffect.StopEffect();
        cube.transform.SetParent(null);
        ObjectPooler.Instance.ReturnToPool("ClassicCube", cube.gameObject);
        listCube.RemoveCube(cube);
        listCube.RemoveDataCube(cube);
    }
}
