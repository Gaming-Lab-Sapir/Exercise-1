using System.Collections;
using UnityEngine;

public class collectableSponer : MonoBehaviour
{
    [SerializeField] GameObject collectablePrefab;
    [SerializeField] int spawnAmount = 10;
    [SerializeField] float spawnDelayMin = 0.1f;
    [SerializeField] float spawnDelayMax = 0.5f;
    [SerializeField] Transform minBound;
    [SerializeField] Transform maxBound;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnCoinsOverTime());
    }

    IEnumerator SpawnCoinsOverTime()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));
            Instantiate(collectablePrefab, getRandomSpawnPoint(), Quaternion.identity);

        }
    }

    private Vector2 getRandomSpawnPoint()
    {
        return new Vector2(Random.Range(minBound.position.x, maxBound.position.x), minBound.position.y);
    }

}
