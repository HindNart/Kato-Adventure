using System.Collections;
using UnityEngine;

public class StrangeDoor : MonoBehaviour
{
    [SerializeField] private Sprite btn_pressed;
    [SerializeField] private GameObject strangeDoor;
    [SerializeField] private GameObject virtualCamera;
    private bool isDoorOpened = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDoorOpened)
        {
            // Move the camera to the door position
            virtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = strangeDoor.transform;
            StartCoroutine(FollowPlayer(other.gameObject.transform));
            // Start the door opening process
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        isDoorOpened = true;
        GetComponent<SpriteRenderer>().sprite = btn_pressed;
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds before opening the door
        // Open the door animation
        strangeDoor.GetComponent<Animator>().SetTrigger("open");
        strangeDoor.GetComponent<BoxCollider2D>().enabled = false;
        AudioManager.Instance.PlayDoorOpenSound();
        Destroy(strangeDoor, 1f);
        Debug.Log("Door opened!");
    }

    private IEnumerator FollowPlayer(Transform playerPosition)
    {
        yield return new WaitForSeconds(1.5f); // Wait for 1.5 seconds before following the player
        // Move the camera to follow the player
        virtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = playerPosition;
    }
}
