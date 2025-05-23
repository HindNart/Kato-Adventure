using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject questionBuy;
    [SerializeField] private GameObject healthDetail;
    [SerializeField] private GameObject antidoteDetail;
    // [SerializeField] private GameObject shopNotify;

    private void Awake()
    {
        if (shopPanel != null && questionBuy != null && healthDetail != null && antidoteDetail != null)
        {
            questionBuy.SetActive(false);
            shopPanel.SetActive(false);
            healthDetail.SetActive(false);
            antidoteDetail.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CloseShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the shop!");
            if (questionBuy != null)
            {
                questionBuy.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player out the shop!");
            if (questionBuy != null)
            {
                questionBuy.SetActive(false);
            }
        }
    }

    public void OpenShop()
    {
        if (shopPanel != null) shopPanel.SetActive(true);
        if (questionBuy != null) questionBuy.SetActive(false);
    }

    public void CloseShop()
    {
        if (shopPanel != null) shopPanel.SetActive(false);
        if (healthDetail != null) healthDetail.SetActive(false);
        if (antidoteDetail != null) antidoteDetail.SetActive(false);
    }

    public void ShowHealthDetail()
    {
        if (healthDetail != null) healthDetail.SetActive(true);
        if (antidoteDetail != null) antidoteDetail.SetActive(false);
    }

    public void ShowAntidoteDetail()
    {
        if (healthDetail != null) healthDetail.SetActive(false);
        if (antidoteDetail != null) antidoteDetail.SetActive(true);
    }
}
