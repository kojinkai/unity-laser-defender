using UnityEngine;

// Destroys laser prefabs when they move off the game canvas
public class ProjectileDestroyerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile") {
            Destroy(collision.gameObject);
        }
    }
}
