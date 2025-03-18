using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    private void Start()
    {
        Player.Instance.onSelectedCounterChanged += Player_onSelectedCounterChanged;
    }

    private void Player_onSelectedCounterChanged(object sender, Player.onSelectedCounterChangedEventArs e)
    {
        //e.selectedCounter
        if (e.selectedCounter == clearCounter) {
            Show();
        }
        else
        {
            Hide();
        }

    }
    private void Show()
    {
        visualGameObject.SetActive(true);
    }
    private void Hide()
    {
        visualGameObject?.SetActive(false);
    }
}
