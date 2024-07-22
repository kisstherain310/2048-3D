using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier = 1.5f; // Hệ số phóng to
    [SerializeField] private float duration = 0.5f; // Thời gian phóng to và thu nhỏ

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }
    // ---- Explore Effect --------------------------------
    // Effect when the cube is Spawned
    public void ExploreEffect()
    {
        StartCoroutine(ScaleCoroutine());
    }

    private IEnumerator ScaleCoroutine()
    {
        float elapsedTime = 0f;
        // Phóng to đối tượng
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleMultiplier, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Đảm bảo đối tượng đạt kích thước tối đa
        transform.localScale = originalScale * scaleMultiplier;

        elapsedTime = 0f;
        // Thu nhỏ đối tượng về kích thước ban đầu
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale * scaleMultiplier, originalScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Đảm bảo đối tượng trở về kích thước ban đầu
        transform.localScale = originalScale;
    }
}
