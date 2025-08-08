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
    [SerializeField] private float dashCoolDown;
    [SerializeField] private float dashCoolDownTimer;

    [Header("Attack info")]
    private float comboTime = 2;
    private float comboTimeCounter;
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
        ComboChecker();
    }

    private void ComboChecker()
    {
        comboTimeCounter -= Time.deltaTime;
        if (comboTimeCounter < 0)
        {
            AttackCombo = 0;
        }
        if (AttackCombo > 2)
        {
            AttackCombo = 0;
        }
    }

    public void AttackTrigerd()
    { 
        isAttacking = false;
        AttackCombo++;
    }

    private void AnimationController()
    {      
        animator.SetBool("isMoveing", isMoveing);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetInteger("AttackCombo" , AttackCombo);
    }

    private void Movement()
    {
        isMoveing = rb.linearVelocity.x != 0;
        if (isAttacking) {
            rb.linearVelocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            rb.linearVelocity = new Vector2(dashSpeed * xinput, 0);
        }
        else 
        { 
            rb.linearVelocity = new Vector2(xinput * moveSpeed, rb.linearVelocity.y);
        }
        
    }

    private void CheckInput()
    {
        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;
        xinput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jamp();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
            comboTimeCounter = comboTime;
        }
    }

    private void DashAbility()
    {
        if (dashCoolDownTimer < 0)
        {
            dashCoolDownTimer = dashCoolDown;
            dashTime = dashdioration;
        }
    }
    private void Jamp()
    {
        if (isGrounded) { 
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jampforce);
        }
    }
    private void FlipConteroller()
    {
        if (rb.linearVelocity.x > 0 && !faceRight)
            Flip();
        else if (rb.linearVelocity.x < 0 && faceRight)
            Flip();
    }

    private void Flip()
    {
        faceDirection *= -1;
        faceRight = !faceRight;
        transform.Rotate(0 , -180 , 0);
    }


    // Physics and Gizmos
    private void CollisionChecks() => isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    private void OnDrawGizmos() => Gizmos.DrawLine(transform.position, new Vector3(transform.position.x ,transform.position.y - groundCheckDistance));

}   