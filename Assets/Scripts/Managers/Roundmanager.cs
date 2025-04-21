using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    [Header("Game Settings")]
    public EnemySpawner[] spawners;
    public int round = 1;
    public int enemiesPerRound = 5;

    [Header("UI")]
    public TextMeshProUGUI roundText;
    public AudioSource roundSFX;

    private bool waitingForNextRound = false;

    void Start()
    {
        UpdateRoundText();
        StartRound();
    }

    void Update()
    {
        if (!waitingForNextRound && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            waitingForNextRound = true;

            round++;

            UpdateRoundText();

            if (roundSFX != null)
                roundSFX.Play();

            Invoke(nameof(StartRound), 2f); // delay for breathing room
        }
    }

    void StartRound()
    {
        int totalEnemies = enemiesPerRound + (round * 2);
        int perSpawner = Mathf.CeilToInt((float)totalEnemies / spawners.Length);

        foreach (EnemySpawner spawner in spawners)
        {
            spawner.SetMaxEnemies(totalEnemies);
            spawner.SpawnWave(perSpawner);
        }

        waitingForNextRound = false;
    }

    void UpdateRoundText()
    {
        if (roundText != null)
            roundText.text = "Round " + round;
    }
}
