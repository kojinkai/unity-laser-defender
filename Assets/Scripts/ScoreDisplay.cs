using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    // config params
    TextMeshProUGUI scoreText;
    GameController gameController;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        scoreText.text = gameController.GetScore().ToString();
    }
}
