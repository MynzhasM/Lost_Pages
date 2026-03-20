using TMPro;
using UnityEngine;

public class BridgeHintTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private string message = "Нажми E. Зачем рисковать, если я могу помочь?";
    [SerializeField] private Color textColor = Color.red;
    [SerializeField] private MagicFloor magicFloor;

    [SerializeField] private CenterTextUI centerTextUI;

    [SerializeField] private PlayerAbilityEffect playerAbilityEffect;

    [SerializeField] private PageUseEffect pageUseEffect;

    private bool playerInside = false;
    private PlayerInventory player;

    private void Start()
    {
        player = FindObjectOfType<PlayerInventory>();

        if (hintText != null)
            hintText.text = "";
    }

    private void Update()
    {
        if (!playerInside || player == null) return;

        if (player.hasBook && Input.GetKeyDown(KeyCode.E))
        {
            if (player.pages > 0)
            {
                player.pages--;

                if (pageUseEffect != null)
                {
                    pageUseEffect.PlayEffect();
                }

                if (magicFloor != null)
                    magicFloor.Activate();
            }

            if (centerTextUI != null)
            {
                centerTextUI.Show("Способность активна: Ходьба по воздуху");
            }

            if (playerAbilityEffect != null && magicFloor != null)
            {
                playerAbilityEffect.PlayEffect(magicFloor.GetDuration());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = true;

        if (hintText != null)
        {
            hintText.text = message;
            hintText.color = textColor;
        }
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
}