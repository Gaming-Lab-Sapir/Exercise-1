using UnityEngine;
using TMPro;

public class FinishZone : MonoBehaviour
{
    [SerializeField] private string winnerMessage = "WINNER — GAME OVER";
    [SerializeField] private bool pauseOnWin = true;
    [SerializeField] private TMP_Text winnerText;

    private void Start()
    {
        if (winnerText != null)
        {
            winnerText.gameObject.SetActive(false);
            winnerText.text = "";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryFinish(other.gameObject);
    }

    private void TryFinish(GameObject go)
    {
        if (!go.CompareTag("Player")) return;
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.AllCollected())
        {
            if (winnerText != null)
            {
                winnerText.gameObject.SetActive(true);
                winnerText.text = winnerMessage;   
            }

            Debug.Log(winnerMessage);

            if (pauseOnWin)
            {
                Time.timeScale = 0f;
            }
        }
        else
        {
            Debug.Log("Not enough coins: " + GameManager.Instance.CollectedCoins + "/" + GameManager.Instance.TargetCoins);
        }
    }
}
