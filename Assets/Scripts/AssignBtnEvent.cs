using UnityEngine;
using UnityEngine.UI;

public class AssignBtnEvent : MonoBehaviour
{
    [SerializeField] private Button btnBuyHealthPotion;
    [SerializeField] private Button btnBuyAntidotePotion;
    [SerializeField] private Button btnResumeGame;
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnSound;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            btnBuyHealthPotion.onClick.RemoveAllListeners();
            btnBuyHealthPotion.onClick.AddListener(GameManager.Instance.PurchaseHealthPotion);

            btnBuyAntidotePotion.onClick.RemoveAllListeners();
            btnBuyAntidotePotion.onClick.AddListener(GameManager.Instance.PurchasePowerPotion);

            btnHome.onClick.RemoveAllListeners();
            btnHome.onClick.AddListener(GameManager.Instance.LoadMenu);
        }

        // if (UIManager.Instance != null)
        // {
        //     btnResumeGame.onClick.RemoveAllListeners();
        //     btnResumeGame.onClick.AddListener(UIManager.Instance.ResumeGame);
        // }

        if (AudioManager.Instance != null)
        {
            btnSound.onClick.RemoveAllListeners();
            btnSound.onClick.AddListener(() => AudioManager.Instance.ToggleVolume(btnSound));
        }
    }
}
