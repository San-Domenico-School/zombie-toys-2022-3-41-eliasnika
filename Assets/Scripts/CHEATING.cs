using UnityEngine;
using TMPro;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverText; // Assign in Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(true); // Force enable the text object
                gameOverText.enabled = true; // Force enable TextMeshPro component
                Debug.Log("Game Over Text Unhidden!");
            }
            else
            {
                Debug.LogError("Game Over Text is not assigned in the Inspector!");
            }
        }
    }
}
