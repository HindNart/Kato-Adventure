using System.Collections;
using UnityEngine;

public class LootHit : MonoBehaviour
{
    public int maxHits = 1;
    public GameObject item;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (maxHits != 0 && other.CompareTag("Player"))
        {
            Hit();
            AudioManager.Instance.PlayChestOpenSound();
        }
    }

    public void Hit()
    {
        anim.SetTrigger("opening");

        maxHits--;

        if (maxHits == 0)
        {
            anim.SetTrigger("opened");
        }

        if (item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
}
