using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xinput;
    private Animator animator;
    private bool isMoveing;

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
        xinput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(xinput * moveSpeed, rb.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jampforce);
        }

        Move();

    }

    private void Move()
    {
        isMoveing = rb.linearVelocity.x != 0;
        animator.SetBool("isMoveing", isMoveing);
    }
}