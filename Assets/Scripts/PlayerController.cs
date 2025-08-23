using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpSpeed = 6f;
    [SerializeField] private TMP_Text coinsText;   

    private bool isGrounded;
    private float directionX;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D is null!");
        if (coinsText == null) Debug.LogWarning("Coins Text (TMP) is not assigned.");

        StartCoroutine(WaitAndInitUI());
    }

    private IEnumerator WaitAndInitUI()
    {
        while (GameManager.Instance == null) yield return null;
        UpdateCoinsUI();
    }

    private void Update()
    {
        directionX = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            HandleJump();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionX * speed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        isGrounded = false;
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.point.y < transform.position.y - 0.1f)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            float playerBottom = GetComponent<Collider2D>().bounds.min.y;

            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.point.y <= playerBottom + 0.05f)
                {
                    isGrounded = true;
                    return;
                }
            }

            isGrounded = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
          
    }

    public void AddCoins()
    {
        if (GameManager.Instance != null) 
        { 
            GameManager.Instance.TryAddCoin(); 
        }
        UpdateCoinsUI();
    }

    private void UpdateCoinsUI()
    {
        if (coinsText == null || GameManager.Instance == null) return;
        coinsText.text = $"{GameManager.Instance.CollectedCoins}/{GameManager.Instance.TargetCoins}";
    }
}
