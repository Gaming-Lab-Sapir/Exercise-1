using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int minCoins = 3;
    [SerializeField] private int maxCoins = 8;

    public int TargetCoins { get; private set; }
    public int CollectedCoins { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { 
        Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f; 
        TargetCoins = Random.Range(minCoins, maxCoins + 1);
        CollectedCoins = 0;
        Debug.Log($"Target: {TargetCoins}");
    }

    public bool TryAddCoin()
    {
        if (CollectedCoins >= TargetCoins) return false;
        CollectedCoins++;
        Debug.Log($"Collect {CollectedCoins}/{TargetCoins}");
        return true;
    }

    public bool AllCollected() => CollectedCoins >= TargetCoins;
}
