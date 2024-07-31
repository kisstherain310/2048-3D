using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLineCollisionHandler : MonoBehaviour
{
    private Vector3 bumbPosition = Vector3.zero;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("ClassicCube")) 
        {
            Cube cube = other.GetComponent<Cube>();
            if(cube != null) {
                if(!cube.isMainCube && cube.rb.velocity.magnitude < 1f && GameManager.Instance.gameStatus.IsLose()) {
                    GameManager.Instance.GameOver();
                    FXManager.Instance.PlayFX(bumbPosition, 4);
                }
            }
        }
    }
}
