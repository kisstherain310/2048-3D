using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventJokerCube : MonoBehaviour
{
    private bool isActive = true;
    public void OnMouseDown()
    {
        if (!isActive) return;
        VibrationManagerX.Vibrate();
        GameManager.Instance.jokerCubeManager.SpawnJokerCube();
        isActive = false;
        StartCoroutine(ActiveBombCube());
    }
    IEnumerator ActiveBombCube()
    {
        yield return new WaitForSeconds(2f);
        isActive = true;
    }
}
