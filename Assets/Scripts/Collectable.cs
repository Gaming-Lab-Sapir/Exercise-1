using System.Collections;
using UnityEditor.XR;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] float delayForDestroy = 0.4f;
    [SerializeField] float waitForPlayer = 0.5f;
    SpriteRenderer sprite;
    bool playerNotReached = false;
    bool playerTouched = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !playerNotReached)
        {
            playerTouched = true;
            sprite.color = Color.green;
            Destroy(gameObject, delayForDestroy);
            return;
        }
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
            sprite.color = Color.red;
            Destroy(gameObject, delayForDestroy);
        }
    }
}
