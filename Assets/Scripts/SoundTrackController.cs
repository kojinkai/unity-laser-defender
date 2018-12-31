using UnityEngine;

public class SoundTrackController : MonoBehaviour
{
    private void Awake()
    {
        // We want this soundTrackController to persist across every scene (singleton)
        // so we check if there is an existing instance and reuse that, destroying the attempted new
        // instance in the process.
        // GetType returns the type of the current gameobject the script is attached to

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
