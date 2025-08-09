using UnityEngine;

public class Entety : MonoBehaviour
{
    [Header("Collision info")]
    protected  Rigidbody2D rb;
    protected  Animator animator;


    [Header("some info")]
    [SerializeField]protected LayerMask groundLayer;
    [SerializeField] protected float groundCheckDis;
    [SerializeField] protected Transform groundTransform;
    protected bool isGrounded;
    protected  bool faceRight = true;
    protected  int faceDirection = 1;



    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        CollisionChecks();
    }

    protected virtual void Flip()
    {
        faceDirection *= -1;
        faceRight = !faceRight;
        transform.Rotate(0, -180, 0);
    }

    protected virtual void CollisionChecks() => isGrounded = Physics2D.Raycast(groundTransform.position, Vector2.down, groundCheckDis, groundLayer);

    protected virtual void OnDrawGizmos() => Gizmos.DrawLine(groundTransform.position, new Vector3(groundTransform.position.x, groundTransform.position.y - groundCheckDis));

}
