using System.Collections;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    private float duration = 0.2f;
    private Vector3 endScale;
    private Vector3 originalScale;
    private Coroutine scaleCoroutine;

    // Effect when the object is spawned
    public void ExploreEffect()
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleCoroutine());
    }
    public void StopEffect()
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        transform.localScale = Vector3.one;
    }

    private IEnumerator ScaleCoroutine()
    {
        transform.localScale = Vector3.one;
        yield return new WaitForSeconds(0.5f);
        originalScale = Vector3.one;
        endScale = new Vector3(1.4f, 1.4f, 1.4f);
        // Scale up
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, endScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;

        // Scale down
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(endScale, originalScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale; // Reset to original scale
        scaleCoroutine = null;
    }
}
