using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeUI : MonoBehaviour
{
    [SerializeField] TMP_Text[] text;
    public Color[] cubeColors;
    
    public Color color;
    private int number;
    private MeshRenderer meshRenderer;
    
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void EditCube(int number){
        Color color = GetColor(number);
        SetColor(color);
        SetNumber(number);
    }
    private Color GetColor (int number) {
        return cubeColors [ (int)(Mathf.Log (number) / Mathf.Log (2)) - 1 ] ;
    }
    
    private void SetColor(Color color)
    {
        this.color = color;
        meshRenderer.material.color = color;
    }
    
    private void SetNumber(int number)
    {
        this.number = number;
        foreach (var t in text)
        {
            t.text = number.ToString();
        }
    }
}
