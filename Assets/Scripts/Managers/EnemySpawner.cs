using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Properties")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int maxEnemies = 20;

    [Header("Debugging Properties")]
    [SerializeField] bool canNotSpawn = false;

    EnemyHealth[] enemies;

    void Awake()
    {
        BuildPool();
    }

    void BuildPool()
    {
        enemies = new EnemyHealth[maxEnemies];
        for (int i = 0; i < maxEnemies; i++)
        {
            GameObject obj = Instantiate(enemyPrefab);
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            obj.transform.parent = transform;
            obj.SetActive(false);
            enemies[i] = enemy;
        }
    }

    public void SpawnWave(int count)
    {
        if (canNotSpawn) return;

        int spawned = 0;
        for (int i = 0; i < enemies.Length && spawned < count; i++)
        {
            if (!enemies[i].gameObject.activeSelf)
            {
                enemies[i].transform.position = transform.position;
                enemies[i].transform.rotation = transform.rotation;
                enemies[i].gameObject.SetActive(true);
                spawned++;
            }
        }
    }

    public void SetMaxEnemies(int newMax)
    {
        if (newMax <= maxEnemies) return;

        EnemyHealth[] newPool = new EnemyHealth[newMax];
        for (int i = 0; i < newMax; i++)
        {
            if (i < enemies.Length && enemies[i] != null)
            {
                newPool[i] = enemies[i];
            }
            else
            {
                GameObject obj = Instantiate(enemyPrefab);
                EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
                obj.transform.parent = transform;
                obj.SetActive(false);
                newPool[i] = enemy;
            }
        }

        enemies = newPool;
        maxEnemies = newMax;
    }
}
