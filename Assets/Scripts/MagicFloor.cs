using UnityEngine;
using System.Collections;

public class MagicFloor : MonoBehaviour
{
    [SerializeField] private float duration = 4f;

    private Coroutine activeCoroutine;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        gameObject.SetActive(true);
        activeCoroutine = StartCoroutine(DeactivateAfterTime());
    }

    public float GetDuration()
    {
        return duration;
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(duration);

        gameObject.SetActive(false);
        activeCoroutine = null;
    }
}