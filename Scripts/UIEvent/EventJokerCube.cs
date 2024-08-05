using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventJokerCube : MonoBehaviour
{
    public void OnMouseDown()
    {
        VibrationManagerX.Vibrate();
        GameManager.Instance.jokerCubeManager.SpawnJokerCube();
    }
}
