using TMPro;
using UnityEngine;

public class PagesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pagesText;
    [SerializeField] private PlayerInventory playerInventory;

    private void Start()
    {
        if (pagesText != null)
            pagesText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (pagesText == null || playerInventory == null) return;

        if (playerInventory.hasBook)
        {
            if (!pagesText.gameObject.activeSelf)
                pagesText.gameObject.SetActive(true);

            pagesText.text = "Pages: " + playerInventory.pages;
        }
        else
        {
            if (pagesText.gameObject.activeSelf)
                pagesText.gameObject.SetActive(false);
        }
    }
}