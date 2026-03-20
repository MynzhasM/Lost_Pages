using System.Collections;
using TMPro;
using UnityEngine;

public class CenterTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private float showTime = 2f;

    private Coroutine currentRoutine;

    private void Start()
    {
        if (textUI != null)
            textUI.gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        if (textUI == null) return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(message));
    }

    private IEnumerator ShowRoutine(string message)
    {
        textUI.text = message;
        textUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(showTime);

        textUI.gameObject.SetActive(false);
        currentRoutine = null;
    }
}