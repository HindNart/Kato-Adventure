using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject controlsPanel;
    public GameObject shopNotify;
    public GameObject menuPausePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }

        if (shopNotify != null)
        {
            shopNotify.SetActive(false);
        }

        if (menuPausePanel != null)
        {
            menuPausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.IsGameStarted())
        {
            ToggleControlsPanel();
        }

        if (GameManager.Instance.gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.gamePaused)
            {
                PauseGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.gamePaused)
            {
                ResumeGame();
            }
        }
    }

    public void ToggleControlsPanel()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(!controlsPanel.activeSelf);
        }
    }

    public void PauseGame()
    {
        GameManager.Instance.gamePaused = true;
        Time.timeScale = 0f;
        // ShowCursor();
        if (menuPausePanel != null)
        {
            menuPausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        GameManager.Instance.gamePaused = false;
        Time.timeScale = 1f;
        // HideCursor();
        if (menuPausePanel != null)
        {
            menuPausePanel.SetActive(false);
        }
    }

    public void ShowOffShopNotify()
    {
        if (shopNotify != null)
        {
            shopNotify.SetActive(false);
        }
    }
}