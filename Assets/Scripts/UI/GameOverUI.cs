using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI recipiesDeliveredText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            Show();
            recipiesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipiesAmount().ToString();
        }
        else
        {
            Hide();
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
    }
}
