using UnityEngine;

public class Boss4Area : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject bossArea;
    private bool bossSummoned = false;

    private void Start()
    {
        if (bossPrefab != null)
        {
            bossPrefab.SetActive(false);
        }
        if (bossArea != null)
        {
            bossArea.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.collectedGems == 0 && !bossSummoned)
        {
            bossPrefab.SetActive(true);
            bossArea.SetActive(true);
            bossSummoned = true;

            AudioManager.Instance.PlayBossBattleTheme();
        }
        else if (GameManager.Instance.collectedGems > 0 && bossSummoned)
        {
            bossPrefab.SetActive(false);
            bossArea.SetActive(false);
            bossSummoned = false;
        }
    }
}