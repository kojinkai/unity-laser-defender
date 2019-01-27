using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] public float gameOverDelay = 2;
    [SerializeField] public AudioClip startGameClip;
    [Range(0f, 1.0f)]
    [SerializeField] public float startClipVolume = .2f;
    [SerializeField] public Animator animator;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayLoadGameClip()
    {
        AudioSource.PlayClipAtPoint(startGameClip, Camera.main.transform.position, startClipVolume);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
        FindObjectOfType<GameController>().ResetGame();
    }

    public void LoadGameOver()
    {
        // We use an animation event to trigger ShowGameOverScreen when this animation finishes
        animator.SetTrigger("FadeOut");
        //StartCoroutine(ShowGameOverScreen());
    }

    //private IEnumerator ShowGameOverScreen()
    //{
    //    yield return new WaitForSeconds(gameOverDelay);
    //    SceneManager.LoadScene("Game Over");
    //}

    public void ShowGameOverScreen()
    {
        SceneManager.LoadScene("Game Over");    
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
