using UnityEngine;
using UnityEngine.UI;

public class AssignBtnGameOverEvent : MonoBehaviour
{
    public Button btnContinue;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(GameManager.Instance.LoadMenu);
        }
    }
}
