using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("save", true);
            other.gameObject.GetComponent<Player>().checkPointPosition = transform;
        }
    }
}
