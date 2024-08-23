using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombCubeManager : CubeManagerBase<BombCube>
{
    public void SpawnBombCube()
    {
        SpawnCube("BombCube");
    }

    public void DestroyBombCube(BombCube bombCube)
    {
        DestroyCube(bombCube);
    }
    public override void PlaySound()
    {
        SoundManager.instance.PlayClip(AudioType.BombShot);
    }
}