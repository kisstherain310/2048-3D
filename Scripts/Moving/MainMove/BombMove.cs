using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMove : BaseMove
{
    private BombCube  bombCube;
    public void GetCube(BombCube bombCube)
    {
        this.bombCube = bombCube;
    }
    protected override bool IsMainCube()
    {
        return bombCube.isMainCube;
    }
    protected override void HandlePointerUp()
    {
        bombCube.handlePointerUp();
    }
}
