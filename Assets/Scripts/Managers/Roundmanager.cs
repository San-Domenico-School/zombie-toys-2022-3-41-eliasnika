using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public EnemySpawner[] spawners;
    public int round = 1;
    public int enemiesPerRound = 5;
    public TextMeshProUGUI roundText;

    [Header("UI + FX")]
    public Animator roundAnimator; // ← drag RoundText Animator here
    public AudioSource roundSFX;   // ← optional: drag AudioSource here

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

            // Update text and play animation here instead
            round++;
            if (roundText != null)
            {
                roundText.text = "Round " + round;
            }

            if (roundAnimator != null)
            {
                roundAnimator.SetTrigger("RoundTransition");
            }

            if (roundSFX != null)
            {
                roundSFX.Play();
            }

            // Delay enemy spawn only
            Invoke(nameof(StartRound), 2f);
        }
    }



    void NextRound()
    {
        round++;

        // Update the text BEFORE the animation plays
        if (roundText != null)
        {
            roundText.text = "Round " + round;
        }

        // Trigger the animation
        if (roundAnimator != null)
        {
            roundAnimator.SetTrigger("RoundTransition");
        }

        // Optional: Play round sound
        if (roundSFX != null)
        {
            roundSFX.Play();
        }

        // Wait for animation to finish before spawning enemies
        Invoke(nameof(StartRound), 2f); // ← adjust time if animation is shorter/longer
        waitingForNextRound = false;
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

        // Reset flag so we can go to the next round later
        waitingForNextRound = false;
    }
}
