using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerMove : BaseMove
{
    private JokerCube  jokerCube;
    public void GetCube(JokerCube jokerCube)
    {
        this.jokerCube = jokerCube;
    }
    protected override bool IsMainCube()
    {
        return jokerCube.isMainCube;
    }
    protected override void HandlePointerUp()
    {
        jokerCube.handlePointerUp();
    }
}
