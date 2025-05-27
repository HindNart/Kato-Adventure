using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Player player;
    private bool isFacingRight = true;

    [Header("Movement")]
    [SerializeField] private float speed = 5.5f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 10.0f;
    private bool canDoubleJump;

    [Header("GroundCheck")]
    [SerializeField] private float groundCheckSize = 0.1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("WallCheck")]
    [SerializeField] private float wallCheckSize = 0.1f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("WallMovement")]
    [SerializeField] private float wallSlideSpeed = 2.0f;
    private bool isWallSliding;

    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.5f;
    private float wallJumpTimer;
    public Vector2 wallJumpForce = new Vector2(5f, 10f);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Movement();
        Jump();
        HandleJumpAnimations();
        ProcessWallSlide();
        ProcessWallJump();
        AttackAnim();
        UsePotion();
    }

    private void Movement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        AudioManager.Instance.PlayPlayerWalkSound();
        anim.SetBool("run", moveInput != 0);

        Flip();
    }

    private void Flip()
    {
        if (isFacingRight && rb.velocity.x < 0 || !isFacingRight && rb.velocity.x > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GroundedCheck())
            {
                // AudioManager.Instance.PlayJumpSound();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                // AudioManager.Instance.PlayJumpSound();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
            else if (wallJumpTimer > 0f)
            {
                // AudioManager.Instance.PlayJumpSound();
                isWallJumping = true;
                rb.velocity = new Vector2(wallJumpDirection * wallJumpForce.x, wallJumpForce.y);
                wallJumpTimer = 0f;

                Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
            }
        }
    }

    private void HandleJumpAnimations()
    {
        if (!GroundedCheck())
        {
            if (rb.velocity.y > 0)
            {
                SetJumpAnimationStates(true, false, false);
                if (GroundedCheck())
                {
                    anim.SetBool("jump_start", false);
                }
            }
            else if (rb.velocity.y < 0)
            {
                SetJumpAnimationStates(false, false, true);
            }
        }
        else
        {
            SetJumpAnimationStates(false, false, false);
        }
    }

    private void SetJumpAnimationStates(bool jumpStart, bool jump, bool jumpEnd)
    {
        anim.SetBool("jump_start", jumpStart);
        anim.SetBool("jump", jump);
        anim.SetBool("jump_end", jumpEnd);
    }

    private void ProcessWallSlide()
    {
        if (WallCheck() && !GroundedCheck())
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
            anim.SetBool("wall_slide", true);
        }
        else
        {
            isWallSliding = false;
            anim.SetBool("wall_slide", false);
        }
    }

    private void ProcessWallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = true;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    private bool GroundedCheck()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, groundLayer);
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckSize, wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckSize);
    }

    private void AttackAnim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("attack", true);
        }
        if (Input.GetMouseButtonDown(1) && GroundedCheck())
        {
            anim.SetBool("attack_cmb", true);
        }
    }

    public void FinishAttack()
    {
        anim.SetBool("attack", false);
        anim.SetBool("attack_cmb", false);
    }

    private void UsePotion()
    {
        GameManager.Instance.SelectPotion();

        if (Input.GetKeyDown(KeyCode.F))
        {
            player.UsePotion(GameManager.Instance.potionType);
        }
    }
}
