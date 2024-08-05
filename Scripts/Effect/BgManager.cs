using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BgManager : MonoBehaviour
{
    // Gán sprite mới thông qua Inspector
    public static BgManager Instance;
    [SerializeField] private Sprite[] listSprite;
    [SerializeField] private List<Button> listButtons;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        Instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        // Gắn sự kiện click cho mỗi Button
        for (int i = 0; i < listButtons.Count; i++)
        {
            int index = i; // Cần biến cục bộ để tránh vấn đề về closure
            listButtons[i].onClick.AddListener(() => ChangeToNewSprite(index));
        }
    }

    public void ChangeToNewSprite(int index)
    {
        spriteRenderer.sprite = listSprite[index];
        GameManager.Instance.uIEvent.CloseShop();
    }
}
