using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xinput;
    private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jampforce;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        
    }

    
    void Update()
    {
        Movement();
        CheckInput();
        animatorConteroller();

    }

    private void CheckInput()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jamp();
        }
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xinput * moveSpeed, rb.velocity.y);
    }

    private void Jamp()
    {
        rb.velocity = new Vector2(rb.velocity.x, jampforce);
    }

    private void animatorConteroller()
    {
        bool isMoveing = rb.velocity.x != 0;
        animator.SetBool("isMoveing", isMoveing);
    }
}