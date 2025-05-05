using UnityEngine;
using TMPro;

public class RedHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public TMP_Text healthText;     // TextMeshPro UI for displaying health
    public TMP_Text gameOverText;   // TextMeshPro UI for Game Over message

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false); // Hide Game Over at start
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HEALTH: " + Mathf.RoundToInt(currentHealth);
        }
    }

    void Die()
    {
        Debug.Log("You died!");

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        // Optional: disable movement/input here
        // Example: GetComponent<PlayerMovement>().enabled = false;
    }
}
