using TMPro;
using UnityEngine;
using System.Collections;

public class BarrierInteraction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private GameObject barrierObject;

    [SerializeField] private string noBookMessage = "Завал... надо искать другой выход";
    [SerializeField] private string hasBookMessage = "Нажми E. Позволь мне открыть тебе путь.";

    [SerializeField] private Color noBookColor = Color.white;
    [SerializeField] private Color hasBookColor = Color.red;

    [SerializeField] private PageUseEffect pageUseEffect;

    private bool playerInside = false;
    private bool destroyed = false;
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();

        if (hintText != null)
            hintText.text = "";
    }

    private void Update()
    {
        if (destroyed || !playerInside || playerInventory == null) return;

        if (playerInventory.hasBook && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DestroyBarrierSequence());
        }
    }

    private IEnumerator DestroyBarrierSequence()
    {
        destroyed = true;

        if (playerInventory.pages > 0)
            playerInventory.pages--;

        // 1. эффект (тряска + затемнение)
        if (pageUseEffect != null)
            pageUseEffect.PlayEffect();

        // 2. ждём чуть-чуть
        yield return new WaitForSeconds(0.3f);

        // 3. уничтожаем завал
        if (barrierObject != null)
            barrierObject.SetActive(false);

        playerInside = false;

        // 4. показываем текст игрока
        if (hintText != null)
        {
            hintText.text = "Что это за тряска?..";
            hintText.color = Color.white;
        }

        // 5. ждём
        yield return new WaitForSeconds(2f);

        // 6. очищаем текст
        if (hintText != null)
        {
            hintText.text = "";
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || destroyed) return;

        playerInside = true;
        UpdateHint();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;

        if (hintText != null)
        {
            hintText.text = "";
            hintText.color = Color.white;
        }
    }

    private void UpdateHint()
    {
        if (hintText == null || playerInventory == null) return;

        if (playerInventory.hasBook)
        {
            hintText.text = hasBookMessage;
            hintText.color = hasBookColor;
        }
        else
        {
            hintText.text = noBookMessage;
            hintText.color = noBookColor;
        }
    }
}