using System.Collections;
using UnityEngine;
public class Collectable : MonoBehaviour
{
    [SerializeField] private float waitForPlayer = 3f;     
    [SerializeField] private float delayForDestroy = 0.05f;  
    private SpriteRenderer sprite;
    private bool playerNotReached = false;
    private bool playerTouched = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || playerNotReached)
        {
            return;
        }

        var player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.AddCoins();
            playerTouched = true;
            sprite.color = Color.green;
            Destroy(gameObject, delayForDestroy);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(DelayDestruction());
        }
    }
    IEnumerator DelayDestruction()
    {
        yield return new WaitForSeconds(waitForPlayer);

        if (!playerTouched)
        {
            playerNotReached = true;
            Destroy(gameObject, delayForDestroy);
        }
    }
}
