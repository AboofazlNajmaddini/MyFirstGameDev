using UnityEngine;

public class player : Entety
{

    [Header("Basic Features")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jampforce;
    private float xinput;


    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashdioration;
    [SerializeField] private float dashCoolDown;
    private float dashTime;
    private float dashCoolDownTimer;

    [Header("Attack info")]
    private float comboTime = 2;
    private float comboTimeCounter;
    private bool isAttacking;
    private int AttackCombo;

    protected override void Start()
    {
        base.Start();
    }

    
    protected override void Update()
    {
        base.Update();
        Movement();
        AnimationController();
        CheckInput();
        FlipConteroller();
        ComboChecker();
        TimeChecker();
        CollisionChecks();
    }


    // Character abilities
    public void AttackTrigerd()
    { 
        isAttacking = false;
        AttackCombo++;
    }


    private void Movement()
    {
        if (isAttacking) rb.linearVelocity = new Vector2(0, 0);
        else if (dashTime > 0) rb.linearVelocity = new Vector2( dashSpeed * faceDirection, 0);
        else rb.linearVelocity = new Vector2( xinput * moveSpeed , rb.linearVelocity.y);
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
        if (isGrounded) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jampforce);
    }

    private void ComboChecker()
    {
        if (comboTimeCounter < 0)
        {
            AttackCombo = 0;
        }
        if (AttackCombo > 2)
        {
            AttackCombo = 0;
        }
    }


    // Checkers
    private void CheckInput()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) Jamp();
        if (Input.GetKeyDown(KeyCode.LeftShift)) DashAbility();
        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded )
        {
            isAttacking = true;
            comboTimeCounter = comboTime;
        }
    }


    private void TimeChecker()
    {
        comboTimeCounter -= Time.deltaTime;
        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;
    }



    // Conterollers
    private void AnimationController()
    {
        bool isMoveing = rb.linearVelocity.x != 0;
        Debug.Log(rb.linearVelocity.x);
        animator.SetBool("isMoveing", isMoveing);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetInteger("AttackCombo", AttackCombo);
    }

    private void FlipConteroller()
    {
        if (rb.linearVelocity.x > 0 && !faceRight)
            Flip();
        else if (rb.linearVelocity.x < 0 && faceRight)
            Flip();
    }

}   