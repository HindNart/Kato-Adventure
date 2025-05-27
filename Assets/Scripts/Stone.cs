using UnityEngine;

public class Stone : MonoBehaviour
{
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         Vector2 contactPoint = other.contacts[0].point;
    //         Vector2 center = other.collider.bounds.center;

    //         if (Mathf.Abs(contactPoint.x - center.x) > Mathf.Abs(contactPoint.y - center.y))
    //         {
    //             other.gameObject.GetComponent<Animator>().SetBool("push", true);
    //         }
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         other.gameObject.GetComponent<Animator>().SetBool("push", false);
    //     }
    // }
}
