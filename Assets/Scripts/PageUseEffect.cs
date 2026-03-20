using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PageUseEffect : MonoBehaviour
{
    [Header("Overlay")]
    [SerializeField] private Image overlay;
    [SerializeField] private float maxAlpha = 0.35f;
    [SerializeField] private float fadeInTime = 0.12f;
    [SerializeField] private float fadeOutTime = 0.35f;

    [Header("Camera Shake")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeStrength = 0.12f;

    private Coroutine currentRoutine;
    private Vector3 cameraStartLocalPos;

    private void Start()
    {
        if (overlay != null)
        {
            Color c = overlay.color;
            c.a = 0f;
            overlay.color = c;
        }

        if (cameraTransform != null)
        {
            cameraStartLocalPos = cameraTransform.localPosition;
        }
    }

    public void PlayEffect()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(EffectRoutine());
    }

    private IEnumerator EffectRoutine()
    {
        if (cameraTransform != null)
            cameraStartLocalPos = cameraTransform.localPosition;

        float t = 0f;
        Color c = overlay != null ? overlay.color : Color.black;

        while (t < fadeInTime)
        {
            t += Time.deltaTime;

            float normalized = Mathf.Clamp01(t / fadeInTime);

            if (overlay != null)
            {
                c.a = Mathf.Lerp(0f, maxAlpha, normalized);
                overlay.color = c;
            }

            yield return null;
        }

        t = 0f;

        while (t < Mathf.Max(fadeOutTime, shakeDuration))
        {
            t += Time.deltaTime;

            if (overlay != null && t <= fadeOutTime)
            {
                float overlayNormalized = Mathf.Clamp01(t / fadeOutTime);
                c.a = Mathf.Lerp(maxAlpha, 0f, overlayNormalized);
                overlay.color = c;
            }

            if (cameraTransform != null && t <= shakeDuration)
            {
                Vector2 randomOffset = Random.insideUnitCircle * shakeStrength;
                cameraTransform.localPosition = cameraStartLocalPos + new Vector3(randomOffset.x, randomOffset.y, 0f);
            }

            yield return null;
        }

        if (overlay != null)
        {
            c.a = 0f;
            overlay.color = c;
        }

        if (cameraTransform != null)
        {
            cameraTransform.localPosition = cameraStartLocalPos;
        }

        currentRoutine = null;
    }
}