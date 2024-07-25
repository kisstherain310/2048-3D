using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubePoint : MonoBehaviour
{
    [SerializeField] public PointMove pointMove;
    [SerializeField] private TMP_Text pointText;
    // ---- Helper Method --------------------------------
    public void CreatePoint(int number, Color color)
    {
        SetPoint(number);
    }
    private void SetPoint(int point)
    {
        pointText.text = '+' + point.ToString();
    }
}
