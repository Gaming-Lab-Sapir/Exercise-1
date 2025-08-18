using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpSpeed = 6f;

    private bool isGrounded;
    private float directionX;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("Rigidbody2D is null!");
    }

    private void Update()
    {
        directionX = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            HandleJump();
    }

    private void HandleJump()
    {
        isGrounded = false;
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionX * speed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    enum Movements
    {
        Horizontal,
        Vertical
    }
}
