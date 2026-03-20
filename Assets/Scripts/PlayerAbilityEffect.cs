using System.Collections;
using UnityEngine;

public class PlayerAbilityEffect : MonoBehaviour
{
    [SerializeField] private GameObject effectObject;

    private ParticleSystem[] particleSystems;
    private Coroutine currentRoutine;

    private void Awake()
    {
        if (effectObject != null)
        {
            particleSystems = effectObject.GetComponentsInChildren<ParticleSystem>(true);
            effectObject.SetActive(false);
        }
    }

    public void PlayEffect(float duration)
    {
        if (effectObject == null) return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(EffectRoutine(duration));
    }

    private IEnumerator EffectRoutine(float duration)
    {
        effectObject.SetActive(true);

        if (particleSystems != null)
        {
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Clear();
                ps.Play();
            }
        }

        yield return new WaitForSeconds(duration);

        if (particleSystems != null)
        {
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }

        effectObject.SetActive(false);
        currentRoutine = null;
    }
}