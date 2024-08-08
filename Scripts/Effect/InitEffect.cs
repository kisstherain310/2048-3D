using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InitEffect : MonoBehaviour
{
    [SerializeField] private float duration = 0.2f; // Thời gian hiệu ứng phóng to
    [SerializeField] private float halfDuration = 0.07f; // Thời gian hiệu ứng phóng to
    private Vector3 targetScale = Vector3.one; // Kích thước cuối cùng

    private Vector3 initScale;
    private Vector3 halfScale;
    public void growEffect()
    {
        StartCoroutine(ScaleUpCoroutine());
    }

    private IEnumerator ScaleUpCoroutine()
    {
        float elapsedTime = 0f;
        initScale = Vector3.zero;
        targetScale = Vector3.one;
        halfScale = new Vector3(0.9f, 0.9f, 0.9f);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(initScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            float t = elapsedTime / halfDuration;
            transform.localScale = Vector3.Lerp(targetScale, halfScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = halfScale;
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            float t = elapsedTime / halfDuration;
            transform.localScale = Vector3.Lerp(halfScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
}
