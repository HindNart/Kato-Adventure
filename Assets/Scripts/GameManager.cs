using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    public bool gamePaused = false;
    public bool gameStarted = false;
    private bool gameSaved = false;
    [SerializeField] private Button buttonContinue;

    private Player player;
    private int playerCoin = 0;
    private int playerOrb = 0;
    public int playerLives { get; private set; } = 3;
    public bool playerDead = false;

    public GameObject doubleDmgIcon;

    public int collectedGems = 0;
    public Image[] gemIcons = new Image[3];

    private readonly int healthPotionCost = 20;
    private readonly int powerPotionCost = 50;
    public int healthPotionQuantity = 0;
    public int powerPotionQuantity = 0;

    private int selectedPotionIndex = 0;
    public int[] potionQuantities { get; private set; }
    private string[] potionTypes = { "health", "power" };
    public string potionType { get; private set; }
    public Sprite[] potionImages;
    [SerializeField] private Image potionImage;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI potionText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI orbText;
    [SerializeField] private TextMeshProUGUI healthPotionPriceText;
    [SerializeField] private TextMeshProUGUI powerPotionPriceText;
    [SerializeField] private TextMeshProUGUI winTitle;
    [SerializeField] private Image winImg;

    private GameObject virtualCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ShowCursor();
    }

    private void Start()
    {
        AudioManager.Instance.PlayMenuMusic();

        // playerCoin = PlayerPrefs.GetInt("PlayerCoin", 0);
        // playerOrb = PlayerPrefs.GetInt("PlayerOrb", 0);
        // playerLives = PlayerPrefs.GetInt("PlayerLives", 3);
        // healthPotionQuantity = PlayerPrefs.GetInt("HealthPotionQuantity", 0);
        // powerPotionQuantity = PlayerPrefs.GetInt("PowerPotionQuantity", 0);
        // potionQuantities = new int[] { healthPotionQuantity, powerPotionQuantity };
    }

    private void OnApplicationQuit()
    {
        if (playerLives > 0)
        {
            SavedPlayerData();
        }
        else
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void Update()
    {
        potionQuantities = new int[] { healthPotionQuantity, powerPotionQuantity };

        GameObject healthPriceObj = GameObject.FindGameObjectWithTag("HealthPriceTxt");
        if (healthPriceObj != null && healthPriceObj.activeInHierarchy)
        {
            healthPotionPriceText = healthPriceObj.GetComponent<TextMeshProUGUI>();
        }

        GameObject powerPriceObj = GameObject.FindGameObjectWithTag("PowerPriceTxt");
        if (powerPriceObj != null && powerPriceObj.activeInHierarchy)
        {
            powerPotionPriceText = powerPriceObj.GetComponent<TextMeshProUGUI>();
        }

        for (int i = 0; i < collectedGems; i++)
        {
            if (i < gemIcons.Length)
            {
                if (gemIcons[i] != null && !gemIcons[i].enabled)
                {
                    gemIcons[i].enabled = true;
                }
            }
        }

        // if (gameStarted)
        // {
        //     if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        //     {
        //         PauseGame();
        //     }
        //     else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused)
        //     {
        //         ResumeGame();
        //     }
        // }
    }

    // public void PauseGame()
    // {
    //     gamePaused = true;
    //     Time.timeScale = 0f;
    //     // ShowCursor();
    //     if (pausePanel != null)
    //     {
    //         pausePanel.GetComponent<Image>().enabled = true;
    //         pausePanel.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    //         foreach (var image in pausePanel.GetComponentsInChildren<Image>())
    //         {
    //             image.enabled = true;
    //         }
    //     }
    // }

    // public void ResumeGame()
    // {
    //     gamePaused = false;
    //     Time.timeScale = 1f;
    //     // HideCursor();
    //     if (UIManager.Instance.menuPausePanel != null)
    //     {
    //         UIManager.Instance.menuPausePanel.SetActive(false);
    //     }
    // }

    public void GetObjects()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("Player not found in the scene!");
        }
        else
        {
            Debug.Log("Player found successfully.");
        }
        hpText = GameObject.FindGameObjectWithTag("HpTxt").GetComponent<TextMeshProUGUI>();
        livesText = GameObject.FindGameObjectWithTag("LivesTxt").GetComponent<TextMeshProUGUI>();
        coinText = GameObject.FindGameObjectWithTag("CoinTxt").GetComponent<TextMeshProUGUI>();
        orbText = GameObject.FindGameObjectWithTag("OrbTxt").GetComponent<TextMeshProUGUI>();

        GameObject winTitleObj = GameObject.FindGameObjectWithTag("WinTitle");
        if (winTitleObj != null && winTitleObj.activeInHierarchy)
        {
            winTitle = winTitleObj.GetComponent<TextMeshProUGUI>();
            winTitle.enabled = false;
        }

        GameObject winImgObj = GameObject.FindGameObjectWithTag("WinImg");
        if (winImgObj != null && winImgObj.activeInHierarchy)
        {
            winImg = winImgObj.GetComponent<Image>();
            winImg.enabled = false;
        }

        GameObject potionImgObj = GameObject.FindGameObjectWithTag("PotionImg");
        if (potionImgObj != null && potionImgObj.activeInHierarchy)
        {
            potionImage = potionImgObj.GetComponent<Image>();
        }

        GameObject potionTxtObj = GameObject.FindGameObjectWithTag("PotionTxt");
        if (potionTxtObj != null && potionTxtObj.activeInHierarchy)
        {
            potionText = potionTxtObj.GetComponent<TextMeshProUGUI>();
        }

        doubleDmgIcon = GameObject.FindGameObjectWithTag("DoubleDmgIcon");
        if (doubleDmgIcon != null && doubleDmgIcon.activeInHierarchy)
        {
            doubleDmgIcon.GetComponent<Image>().enabled = false;
        }

        for (int i = 0; i < gemIcons.Length; i++)
        {
            var gemIconObj = GameObject.FindGameObjectWithTag($"Gem{i}");
            if (gemIconObj != null && gemIconObj.activeInHierarchy)
            {
                gemIcons[i] = gemIconObj.GetComponent<Image>();
                gemIcons[i].enabled = false;
            }
            else
            {
                Debug.LogError($"Gem{i} not found in the scene!");
            }
        }

        virtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera");
        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera not found in the scene!");
        }
    }

    private void FixedUpdate()
    {
        GameObject btnContinue = GameObject.FindGameObjectWithTag("ContinueBtnMenu");
        if (btnContinue != null && btnContinue.activeInHierarchy)
        {
            buttonContinue = btnContinue.GetComponent<Button>();
        }
        if (buttonContinue != null)
        {
            if (PlayerPrefs.HasKey("RespawnPointX") && PlayerPrefs.HasKey("RespawnPointY") && PlayerPrefs.HasKey("RespawnPointZ"))
            {
                buttonContinue.interactable = true;
            }
            else
            {
                buttonContinue.interactable = false;
            }
        }

        if (hpText != null) hpText.text = $"{Mathf.FloorToInt(player.currentHp)}/100";
        if (coinText != null) coinText.text = $"x{playerCoin}";
        if (orbText != null) orbText.text = $"x{playerOrb}";
        if (livesText != null) livesText.text = $"x{playerLives}";
        if (healthPotionPriceText != null) healthPotionPriceText.text = $"{healthPotionCost}";
        if (powerPotionPriceText != null) powerPotionPriceText.text = $"{powerPotionCost}";
        UpdatePotionSelection(selectedPotionIndex);
    }

    public void AddCoin()
    {
        playerCoin++;
        Debug.Log("Coin added. Current Coin: " + playerCoin);
    }

    public void AddOrb()
    {
        playerOrb++;
        Debug.Log("Orb added. Current Orb: " + playerOrb);
        if (playerOrb == 100)
        {
            GainLife();
            playerOrb = 0;
        }
    }

    public void AddHealthPotion()
    {
        healthPotionQuantity++;
        Debug.Log("HealthPotionQuantity added. Current HealthPotionQuantity: " + healthPotionQuantity);
    }

    public void AddPowerPotion()
    {
        powerPotionQuantity++;
        Debug.Log("PowerPotionQuantity added. Current PowerPotionQuantity: " + powerPotionQuantity);
    }

    public void PurchaseHealthPotion()
    {
        if (playerCoin >= healthPotionCost)
        {
            playerCoin -= healthPotionCost;
            AddHealthPotion();
            Debug.Log("Health potion purchased. Remaining coin: " + playerCoin);
        }
        else
        {
            UIManager.Instance.shopNotify.SetActive(true);
            Debug.Log("Not enough coin to purchase health potion.");
        }
    }

    public void PurchasePowerPotion()
    {
        if (playerCoin >= powerPotionCost)
        {
            playerCoin -= powerPotionCost;
            AddPowerPotion();
            Debug.Log("Power potion purchased. Remaining coin: " + playerCoin);
        }
        else
        {
            UIManager.Instance.shopNotify.SetActive(true);
            Debug.Log("Not enough coin to purchase power potion.");
        }
    }

    public void SelectPotion()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedPotionIndex = (selectedPotionIndex - 1 + potionImages.Length) % potionImages.Length;
            UpdatePotionSelection(selectedPotionIndex);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            selectedPotionIndex = (selectedPotionIndex + 1) % potionImages.Length;
            UpdatePotionSelection(selectedPotionIndex);
        }
    }

    private void UpdatePotionSelection(int i)
    {
        if (potionImage != null && potionImages != null && potionQuantities != null && potionTypes != null &&
            i >= 0 && i < potionImages.Length && i < potionQuantities.Length && i < potionTypes.Length)
        {
            potionImage.sprite = potionImages[i];
            potionText.text = potionQuantities[i].ToString();
            potionType = potionTypes[i];
        }
    }

    public void LoseLife()
    {
        playerLives--;

        if (playerLives > 0)
        {
            Debug.Log("Life lost. Remaining lives: " + playerLives);
            StartCoroutine(RespawnDelay());
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(1f);
        player.Respawn();
    }

    private void GainLife()
    {
        playerLives++;
        Debug.Log("Life gained. Total lives: " + playerLives);
    }

    private void SavedPlayerData()
    {
        PlayerPrefs.SetFloat("PlayerHp", player.currentHp);
        PlayerPrefs.SetInt("PlayerCoin", playerCoin);
        PlayerPrefs.SetInt("PlayerOrb", playerOrb);
        PlayerPrefs.SetInt("PlayerLives", playerLives);
        PlayerPrefs.SetInt("HealthPotionQuantity", healthPotionQuantity);
        PlayerPrefs.SetInt("PowerPotionQuantity", powerPotionQuantity);
        PlayerPrefs.SetInt("CollectedGems", collectedGems);

        if (player != null)
        {
            if (player.checkPointPosition != null)
            {
                Vector3 respawnPosition = player.checkPointPosition.position;
                PlayerPrefs.SetFloat("RespawnPointX", respawnPosition.x);
                PlayerPrefs.SetFloat("RespawnPointY", respawnPosition.y);
                PlayerPrefs.SetFloat("RespawnPointZ", respawnPosition.z);
            }
            else
            {
                Vector3 respawnPosition = player.transform.position;
                PlayerPrefs.SetFloat("RespawnPointX", respawnPosition.x);
                PlayerPrefs.SetFloat("RespawnPointY", respawnPosition.y);
                PlayerPrefs.SetFloat("RespawnPointZ", respawnPosition.z);
            }
        }
    }

    private IEnumerator LoadPlayerData()
    {
        yield return new WaitForSeconds(0.1f);
        player.currentHp = PlayerPrefs.GetFloat("PlayerHp", 100f);
        playerCoin = PlayerPrefs.GetInt("PlayerCoin", 0);
        playerOrb = PlayerPrefs.GetInt("PlayerOrb", 0);
        playerLives = PlayerPrefs.GetInt("PlayerLives", 3);
        healthPotionQuantity = PlayerPrefs.GetInt("HealthPotionQuantity", 0);
        powerPotionQuantity = PlayerPrefs.GetInt("PowerPotionQuantity", 0);
        potionQuantities = new int[] { healthPotionQuantity, powerPotionQuantity };
        collectedGems = PlayerPrefs.GetInt("CollectedGems", 0);

        Vector3 respawnPosition = new(
            PlayerPrefs.GetFloat("RespawnPointX", player.transform.position.x),
            PlayerPrefs.GetFloat("RespawnPointY", player.transform.position.y),
            PlayerPrefs.GetFloat("RespawnPointZ", player.transform.position.z)
        );
        player.transform.position = respawnPosition + Vector3.right;
        yield return new WaitForSeconds(0.1f);
        virtualCamera.GetComponent<Cinemachine.CinemachineConfiner2D>().m_BoundingShape2D = player.mapBoundary;
    }

    public void LoadMenu()
    {
        gameStarted = false;
        Time.timeScale = 1f;
        // ResumeGame();
        SavedPlayerData();
        SceneManager.LoadScene("Menu");
        AudioManager.Instance.PlayMenuMusic();
    }

    public void StartGame()
    {
        gameStarted = true;

        playerCoin = 0;
        playerOrb = 0;
        playerLives = 3;
        healthPotionQuantity = 0;
        powerPotionQuantity = 0;
        potionQuantities = new int[2] { healthPotionQuantity, powerPotionQuantity };
        collectedGems = 0;
        PlayerPrefs.DeleteAll();

        Invoke(nameof(GetObjects), 0.05f);
        SceneManager.LoadScene("Game");
    }

    public void LoadGame()
    {
        gameStarted = true;
        SceneManager.LoadScene("Game");
        Invoke(nameof(GetObjects), 0.05f);
        StartCoroutine(LoadPlayerData());
    }

    public IEnumerator GameOver()
    {
        player.currentHp = 100f;
        playerCoin = 0;
        playerOrb = 0;
        playerLives = 3;
        healthPotionQuantity = 0;
        powerPotionQuantity = 0;
        potionQuantities = new int[2] { healthPotionQuantity, powerPotionQuantity };
        collectedGems = 0;
        PlayerPrefs.DeleteKey("RespawnPointX");
        PlayerPrefs.DeleteKey("RespawnPointY");
        PlayerPrefs.DeleteKey("RespawnPointZ");

        ShowCursor();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameOver");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }

    public IEnumerator AnimateWin()
    {
        if (winTitle != null)
        {
            winTitle.enabled = true;
        }
        yield return new WaitForSeconds(0.25f);

        var winTitleRect = winTitle.GetComponent<RectTransform>();

        float elapsed = 0f;
        float duration = 1.5f;

        Vector2 startPos = winTitleRect.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0, 600); // Di chuyển lên trên 100 đơn vị

        winTitle.enabled = true;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            // Sử dụng hàm EaseOutQuad để di chuyển chậm dần
            // float easedT = 1 - Mathf.Pow(1 - t, 2);

            winTitleRect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        winTitleRect.anchoredPosition = endPos;

        yield return new WaitForSeconds(3f);

        if (winImg != null)
        {
            winImg.enabled = true;
        }
        yield return new WaitForSeconds(0.25f);

        while (winImg.fillAmount < 1f)
        {
            winImg.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.01f); // Thêm delay để hiệu ứng chậm lại
        }

        yield return new WaitForSeconds(5f);
        PlayerPrefs.DeleteKey("RespawnPointX");
        SceneManager.LoadScene("Menu");
    }
}
