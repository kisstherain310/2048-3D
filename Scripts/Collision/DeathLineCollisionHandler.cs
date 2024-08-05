using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLineCollisionHandler : MonoBehaviour
{
    private Vector3 bumbPosition = Vector3.zero;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("ClassicCube") || other.CompareTag("BombCube") || other.CompareTag("JokerCube")) 
        {
            BaseCube cube = other.GetComponent<BaseCube>();
            if(cube.transform.parent != null) {
                if(!cube.isMainCube && cube.rb.velocity.magnitude < 1f && GameManager.Instance.gameStatus.IsPlaying()) {
                    GameManager.Instance.GameOver();
                    FXManager.Instance.PlayFX(bumbPosition, FXType.ReplayEffect);
                    SoundManager.instance.PlayClip(AudioType.SmallSuccess);
                }
            }
        }
    }
}
