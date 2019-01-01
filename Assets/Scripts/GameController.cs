using UnityEngine;

public class GameController : MonoBehaviour
{

    // state
    int currentScore = 0;

    private void Awake()
    {
        // We want this gameStatusController to persist across every scene (singleton)
        // so we check if there is an existing instance and reuse that, destroying the attempted new
        // instance in the process.
        if (FindObjectsOfType<GameController>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void AddToScore(int scoreValue)
    {
        currentScore += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
