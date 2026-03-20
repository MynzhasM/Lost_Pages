using TMPro;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintText;

    [SerializeField] private string noBookMessage = "Завал... надо искать другой выход";
    [SerializeField] private string hasBookMessage = "Нажми E. Позволь мне открыть тебе путь.";

    [SerializeField] private Color noBookColor = Color.white;
    [SerializeField] private Color hasBookColor = Color.red;

    private PlayerInventory playerInventory;

    private void Start()
    {
        if (hintText != null)
            hintText.text = "";

        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (hintText == null) return;

        if (playerInventory != null && playerInventory.hasBook)
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

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (hintText == null) return;

        hintText.text = "";
        hintText.color = Color.white;
    }
}