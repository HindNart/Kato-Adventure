using UnityEngine;

public class ActiveTrap : MonoBehaviour
{
    [SerializeField] private float dropSpeed = 1f;
    [SerializeField] private GameObject trapObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (trapObject != null)
            {
                trapObject.GetComponent<Rigidbody2D>().gravityScale = dropSpeed;
            }
            else
            {
                Debug.LogWarning("Trap object is not assigned in the inspector.");
            }
        }
    }
}
