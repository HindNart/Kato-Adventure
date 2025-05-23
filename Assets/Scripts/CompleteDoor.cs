using System.Collections;
using UnityEngine;

public class CompleteDoor : MonoBehaviour
{
    [SerializeField] private Transform targetDoor;
    [SerializeField] private GameObject tagetCameraBound;
    [SerializeField] private GameObject virtualCamera;

    public void TeleportToTargetDoor(GameObject player)
    {
        if (targetDoor != null && player != null)
        {
            player.transform.position = targetDoor.position;
            player.GetComponent<Player>().spawnPosition = targetDoor;
            virtualCamera.GetComponent<Cinemachine.CinemachineConfiner2D>().m_BoundingShape2D = tagetCameraBound.GetComponent<Collider2D>();
        }

        if (player.GetComponent<Player>().checkPointPosition != null)
        {
            player.GetComponent<Player>().checkPointPosition = null;
        }

        targetDoor.GetComponent<Collider2D>().enabled = false;

        if (targetDoor.name == "SwampExitDoor")
        {
            if (targetDoor.GetComponent<SpriteRenderer>().enabled == false)
            {
                targetDoor.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (targetDoor.name == "ForestDoor" || targetDoor.name == "ForestExitDoor")
        {
            AudioManager.Instance.PlayForestMusic();
        }
        else if (targetDoor.name == "CaveDoor" || targetDoor.name == "CaveExitDoor")
        {
            AudioManager.Instance.PlayCaveMusic();
        }
        else if (targetDoor.name == "SwampDoor" || targetDoor.name == "SwampExitDoor")
        {
            AudioManager.Instance.PlaySwampMusic();
        }
        else if (targetDoor.name == "TempleDoor")
        {
            AudioManager.Instance.PlayTempleMusic();
        }

        PlayerPrefs.SetFloat("tagetDoorX", targetDoor.position.x);
        PlayerPrefs.SetFloat("tagetDoorY", targetDoor.position.y);
        PlayerPrefs.SetFloat("tagetDoorZ", targetDoor.position.z);

        // if (GameManager.Instance.checkShopArea)
        // {
        //     GameManager.Instance.ShowCursor();
        // }
        // else
        // {
        //     GameManager.Instance.HideCursor();
        // }
    }

    private IEnumerator DelayedTeleport(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        TeleportToTargetDoor(player);
        yield return new WaitForSeconds(2f);
        targetDoor.GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DelayedTeleport(other.gameObject));
        }
    }
}
