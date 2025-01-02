using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public static bool enemySpawn;
    public static bool enemyDie;

    private bool enemyIsSpawning = false;

    private GameObject spawnPrefab;

    void Update()
    {
        if(enemySpawn && !enemyIsSpawning)
        {
            spawnPrefab = Instantiate(prefab, transform.position, Quaternion.identity);
            enemyIsSpawning = true;
        }

        if(enemyDie && enemyIsSpawning)
            Destroy(gameObject);
    }
}
