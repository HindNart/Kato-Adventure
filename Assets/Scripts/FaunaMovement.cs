using UnityEngine;

public class FaunaMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float distance = 5f;

    public bool isMovingRight = true;
    private Vector3 startPosition;
    private Animator anim;
    public float waitTime = 1.5f;
    private float waitTimer = 0.0f;
    private bool isWaiting = false;

    void Start()
    {
        startPosition = transform.position;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        float leftBound = startPosition.x - distance;
        float rightBound = startPosition.x + distance;

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0.0f;
            }
            else
            {
                anim.SetBool("walk", false);
                return;
            }
        }

        if (isMovingRight && !isWaiting)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            anim.SetBool("walk", true);
            if (transform.position.x >= rightBound)
            {
                isMovingRight = false;
                GetComponent<SpriteRenderer>().flipX = !isMovingRight;
                isWaiting = true;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            anim.SetBool("walk", true);
            if (transform.position.x <= leftBound)
            {
                isMovingRight = true;
                GetComponent<SpriteRenderer>().flipX = !isMovingRight;
                isWaiting = true;
            }
        }
    }
}
