using TMPro;
using UnityEngine;

public class BookPickup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private string pickupMessage = "Нажмите E, чтобы взять книгу";

    [SerializeField] private CenterTextUI centerTextUI;

    private bool playerInRange = false;
    private bool pickedUp = false;

    private void Start()
    {
        if (hintText != null)
            hintText.text = "";
    }

    private void Update()
    {
        if (pickedUp) return;

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUpBook();
        }
    }

    private void PickUpBook()
    {
        pickedUp = true;

        Debug.Log("Book picked up!");

        PlayerInventory player = FindObjectOfType<PlayerInventory>();

        if (player != null)
        {
            player.hasBook = true;
            player.pages = 3;
        }
        else
        {
            Debug.LogWarning("PlayerInventory не найден!");
        }

        if (hintText != null)
            hintText.text = "";

        gameObject.SetActive(false);

        if (centerTextUI != null)
        {
            centerTextUI.Show("Вы получили магическую книгу");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (hintText != null)
        {
            hintText.color = Color.white;
            hintText.text = pickupMessage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (hintText != null)
            hintText.text = "";
    }
}