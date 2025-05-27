using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public float speed = 2f;
    private float groundWidth;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        // groundWidth = 24.9f;
        groundWidth = GetComponent<Renderer>().bounds.size.x;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= startPosition.x - groundWidth)
        {
            transform.position += new Vector3(groundWidth, 0, 0);
        }
    }
}