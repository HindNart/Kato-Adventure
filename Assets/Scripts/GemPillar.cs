using UnityEngine;

public class GemPillar : MonoBehaviour
{
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private int gemIndex = 0;

    private void Start()
    {
        gemPrefab.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player"));
            if (playerCollider != null)
            {
                playerCollider.gameObject.GetComponent<Player>().PlaceGemOnPedestal();
                gemPrefab.SetActive(true);
                if (GameManager.Instance.gemIcons[gemIndex].enabled == true)
                {
                    GameManager.Instance.gemIcons[gemIndex].enabled = false;
                }
            }
        }
    }
}