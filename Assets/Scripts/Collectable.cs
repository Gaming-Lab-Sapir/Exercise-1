using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float waitForPlayer = 10f;
    [SerializeField] private float delayForDestroy = 0.5f;

    private SpriteRenderer sprite;
    private bool playerNotReached = false;
    private bool playerTouched = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || playerNotReached) return;

        var player = collision.GetComponent<PlayerController>();
        if (player == null) return;

        // נספור רק דרך השחקן (PlayerController.AddCoins) – לא דרך ה-GameManager ישירות
        player.AddCoins();

        // נטרול הקוליידר כדי שלא יופעל פעמיים
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        playerTouched = true;
        if (sprite) sprite.color = Color.green;
        Destroy(gameObject, delayForDestroy);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(DelayDestruction());
        }
    }

    private IEnumerator DelayDestruction()
    {
        yield return new WaitForSeconds(waitForPlayer);

        if (!playerTouched)
        {
            playerNotReached = true;
            Destroy(gameObject, delayForDestroy);
        }
    }
}
