using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    [Header("Game Settings")]
    public EnemySpawner[] spawners;
    public int round = 1;
    public int enemiesPerRound = 5;

    [Header("UI + FX")]
    public TextMeshProUGUI roundText;
    public Animator roundAnimator; // Drag the RoundText's Animator here
    public AudioSource roundSFX;   // Optional round-start sound

    private bool waitingForNextRound = false;

    void Start()
    {
        StartRound();
    }

    void Update()
    {
        // Wait until all enemies are dead before triggering next round
        if (!waitingForNextRound && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            waitingForNextRound = true;

            round++; // Increment round number (actual text will update during animation)

            if (roundAnimator != null)
            {
                roundAnimator.SetTrigger("RoundTransition");
            }

            if (roundSFX != null)
            {
                roundSFX.Play();
            }

            // Delay spawning enemies to give animation time to finish
            Invoke(nameof(StartRound), 2f);
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

        // Ready to check for next round once enemies die
        waitingForNextRound = false;
    }

    // Called from animation event (midway through RoundText_Anim)
    public void UpdateRoundText()
    {
        if (roundText != null)
        {
            roundText.text = "Round " + round;
        }
    }
}
