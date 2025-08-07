using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float xinput;
    private bool isMoveing;
    private bool faceRight = true;
    private int faceDirection = 1;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jampforce;

    
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
        Move();
        FlipConteroller();
    }

    private void AnimationController()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jamp();
        }
    }

    private void Movement()
    {
        rb.linearVelocity = new Vector2(xinput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jamp()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jampforce);
    }

    private void Move()
    {
        isMoveing = rb.linearVelocity.x != 0;
        animator.SetBool("isMoveing", isMoveing);
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
}   