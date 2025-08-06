using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xinput;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float jampforce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xinput*moveSpeed, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space)) // Check if the space key is pressed down
        {
            rb.velocity = new Vector2(rb.velocity.x, jampforce); // Reset vertical velocity to prevent double jump
        }

    }
}
