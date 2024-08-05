using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PointCubeManager : MonoBehaviour
{
    private float time = 1f;
    private Vector3 pointStart;
    private Vector3 pointEnd;
    public void SpawnPointCube(Vector3 position, int point)
    {
        CubePoint cubePoint = ObjectPooler.Instance.SpawnFromPool("PointCube", position, Quaternion.identity).GetComponent<CubePoint>();
        cubePoint.CreatePoint(point);
        SetRace(position);
        cubePoint.pointMove.MoveEffect(pointStart, pointEnd);
        StartCoroutine(DestroyPointCubeAfterTime(cubePoint));

        GameManager.Instance.scoreManager.AddScore(point);
    }
    private void SetRace(Vector3 position)
    {
        pointStart = position + new Vector3(0.3f, -2f, 0);
        pointEnd = position + new Vector3(0.3f, 0f, 0);
    }
    IEnumerator DestroyPointCubeAfterTime(CubePoint cubePoint)
    {
        yield return new WaitForSeconds(time);
        DestroyPointCube(cubePoint);
    }
    private void DestroyPointCube(CubePoint cubePoint)
    {
        ObjectPooler.Instance.ReturnToPool("PointCube", cubePoint.gameObject);
    }
}
