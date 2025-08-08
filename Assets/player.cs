using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float xinput;
    private bool isMoveing;
    private bool faceRight = true;
    private int faceDirection = 1;
    private bool isGrounded;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jampforce;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashdioration;
    [SerializeField] private float dashTime;

    [Header("Attack info")]
    private bool isAttacking;
    private int AttackCombo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    
    [System.Obsolete]
    void Update()
    {
        Movement();
        AnimationController();
        CollisionChecks();
        CheckInput();
        FlipConteroller();
        dashTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashTime = dashdioration;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
        }
    }

    public void AttackTrigerd()
    {
        isAttacking = false;
    }


    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    private void AnimationController()
    {      
        isMoveing = rb.linearVelocity.x != 0;
        animator.SetBool("isMoveing", isMoveing);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetInteger("AttackCombo" , AttackCombo);
    }

    private void Movement()
    {
        if (dashTime > 0)
        {
            rb.linearVelocity = new Vector2(dashSpeed * xinput, 0);
        }
        else 
        { 
            rb.linearVelocity = new Vector2(xinput * moveSpeed, rb.linearVelocity.y);
        }
        
    }

    private void Jamp()
    {
        if (isGrounded) { 
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jampforce);
        }
    }

    private void CheckInput()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jamp();
        }
    }

    private void Flip()
    {
        faceDirection *= -1;
        faceRight = !faceRight;
        transform.Rotate(0 , -180 , 0);
    }
    private void FlipConteroller()
    {
        if (rb.linearVelocity.x > 0 && !faceRight)
            Flip();
        else if (rb.linearVelocity.x < 0 && faceRight)
            Flip();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x ,transform.position.y - groundCheckDistance));
    }
}   