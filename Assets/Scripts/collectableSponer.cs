using System.Collections;
using UnityEngine;

public class collectableSponer : MonoBehaviour
{
    [SerializeField] GameObject collectablePrefab;
    [SerializeField] int spawnAmount = 30;
    [SerializeField] float spawnDelayMin = 0.5f;
    [SerializeField] float spawnDelayMax = 2f;
    [SerializeField] Transform minBound;
    [SerializeField] Transform maxBound;
     
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
