using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventBombCube : MonoBehaviour
{
    public void OnMouseDown()
    {
        VibrationManagerX.Vibrate();
        GameManager.Instance.bombCubeManager.SpawnBombCube();
        GameManager.Instance.moveManager.isActive = true;
    }
}
