using UnityEngine;
using UnityEngine.UI;

public class AssignBtnMenuEvent : MonoBehaviour
{
    public Button btnStart;
    public Button btnContinue;
    public Button btnQuit;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            btnStart.onClick.RemoveAllListeners();
            btnStart.onClick.AddListener(GameManager.Instance.StartGame);

            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(GameManager.Instance.LoadGame);

            btnQuit.onClick.RemoveAllListeners();
            btnQuit.onClick.AddListener(GameManager.Instance.Quit);
        }
    }
}
