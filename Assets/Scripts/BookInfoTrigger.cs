using TMPro;
using UnityEngine;

public class BookInfoTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintText;

    [TextArea]
    [SerializeField] private string message = 
        "Магическая книга.\nВ ней заключено зло.\nЕё сила опасна.";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (hintText != null)
        {
            hintText.text = message;
            hintText.color = Color.white;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (hintText != null)
        {
            hintText.text = "";
        }
    }
}