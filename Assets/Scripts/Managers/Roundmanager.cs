using UnityEngine;
using TMPro;


public class RoundManager : MonoBehaviour
{
    public EnemySpawner[] spawners;
    public int round = 1;
    public int enemiesPerRound = 5;
    public TextMeshProUGUI roundText;

    private bool waitingForNextRound = false;

    void Start()
    {
        StartRound();
    }

    void Update()
    {
        if (!waitingForNextRound && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            waitingForNextRound = true;
            Invoke(nameof(NextRound), 3f);
        }
    }

    void NextRound()
    {
        round++;
        StartRound();
        waitingForNextRound = false;
    }

    void StartRound()
    {
        if (roundText != null)
        {
            roundText.text = "Round " + round;
        }

        int totalEnemies = enemiesPerRound + (round * 2);
        int perSpawner = Mathf.CeilToInt((float)totalEnemies / spawners.Length);

        foreach (EnemySpawner spawner in spawners)
        {
            spawner.SetMaxEnemies(totalEnemies);
            spawner.SpawnWave(perSpawner);
        }
    }
}
