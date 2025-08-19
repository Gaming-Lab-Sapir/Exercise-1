using UnityEngine;

public class Collectable : MonoBehaviour
{
    private SpriteRenderer sprite;
    private bool playerNotReached = false;
    private bool playerTouched = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerNotReached = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player touched the coin");
            playerTouched = true;
            sprite.enabled = false;
            Destroy(gameObject, 0.5f);//right now I'm not sure if it should be destroyed or not, we'll see later when doing the pick up logic.
        }
    }

}