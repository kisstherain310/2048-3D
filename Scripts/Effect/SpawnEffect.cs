using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier = 1.5f; // Hệ số phóng to
    [SerializeField] private float duration = 0.5f; // Thời gian phóng to và thu nhỏ

    private Vector3 originalScale;
    private Coroutine scaleCoroutine;
    // ---- Explore Effect --------------------------------
    // Effect when the cube is Spawned
    public void ExploreEffect()
    {
        // Nếu có coroutine đang chạy, hủy nó
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        // Bắt đầu một coroutine mới
        scaleCoroutine = StartCoroutine(ScaleCoroutine());
    }

    private IEnumerator ScaleCoroutine()
    {
        originalScale = Vector3.one;
        // ---- Phóng to đối tượng ----
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleMultiplier, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale * scaleMultiplier;

        // ---- Thu nhỏ đối tượng về kích thước ban đầu ----
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale * scaleMultiplier, originalScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one;
        scaleCoroutine = null;
    }
}
