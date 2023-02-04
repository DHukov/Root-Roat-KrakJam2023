using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float jumpForce = 16f;
    public float speed = 8f;
    private float horizontal;
    private bool m_FacingRight = true;
    private float distanceToGround = 0.2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") || GroundChecking())
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        Flip();
    }
    private bool GroundChecking()
    {
        return Physics2D.OverlapCircle(groundCheck.position, distanceToGround, groundLayer);
    }

    void FixedUpdate() =>   rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
