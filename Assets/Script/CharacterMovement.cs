using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float jumpForce = 16f;
    public float speed = 8f;
    private float horizontal;
    private bool m_FacingRight = true;
    private float distanceToGround = 0.2f;
    private float distanceToCeiling = 0.2f;

    private int playerLayerInt, platformLayerInt;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ceilingLayer;
    private void Start()
    {
        playerLayerInt = LayerMask.NameToLayer("Player");
        platformLayerInt = LayerMask.NameToLayer("Platform");

        Debug.Log(playerLayerInt);
        Debug.Log(platformLayerInt);
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && GroundChecking())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        CielingChecking();
        Flip();
    }

    private void CielingChecking()
    {
        if( rb.velocity.y > 0)
        {
            if (Physics2D.OverlapCircle(ceilingCheck.position, distanceToCeiling, ceilingLayer))
            {
                Debug.Log("contact");
                Physics2D.IgnoreLayerCollision(playerLayerInt, platformLayerInt, true) ;
            }
        }
        if (rb.velocity.y < 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayerInt, platformLayerInt, false);
        }
    }


    private bool GroundChecking()
    {
        return Physics2D.OverlapCircle(groundCheck.position, distanceToGround, groundLayer);
    }

    void FixedUpdate() =>   rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    private void Flip()
    {
        if(m_FacingRight && horizontal < 0f || !m_FacingRight && horizontal> 0f)
        {
            m_FacingRight = !m_FacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
