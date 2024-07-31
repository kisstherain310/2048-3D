using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvent : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.Instance.OnPlay();
    }
}
