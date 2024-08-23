using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JokerCubeManager : CubeManagerBase<JokerCube>
{
    public void SpawnJokerCube()
    {
        SpawnCube("JokerCube");
    }

    public void DestroyJokerCube(JokerCube jokerCube)
    {
        DestroyCube(jokerCube);
    }
    public override void PlaySound()
    {
        SoundManager.instance.PlayClip(AudioType.JokerShot);
    }
}
