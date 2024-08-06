using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIMain : MonoBehaviour
{
    public static Action OnComplete;
    [SerializeField] Button btn_Start;
    private void Start()
    {
        btn_Start.onClick.AddListener(OnStart);
    }
    private void OnStart()
    {
        this.gameObject.SetActive(false);
        OnComplete?.Invoke();
    }
}
