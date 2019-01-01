using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] public int scoreValue = 50;

    [Header("Attack Params")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] public GameObject redLaserPrefab;
    [SerializeField] public GameObject explosionParticles;
    [SerializeField] public float explosionDuration = 1f;

    [Header("Sound FX")]
    [SerializeField] public AudioClip enemyDestroyedSFX;
    [SerializeField] public AudioClip enemyFireSFX;
    [SerializeField] [Range(0, 1)] float enemyDestroyedSFXVolume = 1f;
    [SerializeField] [Range(0, 1)] float enemyFireSFXVolume = 1f;

    void Start()
    {
        ResetShotCounterWithRandomInterval();
    }

    private void ResetShotCounterWithRandomInterval()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountdownAndShoot();
    }

    private void CountdownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        AudioSource.PlayClipAtPoint(enemyFireSFX, Camera.main.transform.position, enemyFireSFXVolume);
        GameObject laser = Instantiate(
                redLaserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        ResetShotCounterWithRandomInterval();
    }

    private void RegisterDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        GameObject destroyExplosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(destroyExplosion, explosionDuration);
        AudioSource.PlayClipAtPoint(enemyDestroyedSFX, Camera.main.transform.position, enemyDestroyedSFXVolume);
        FindObjectOfType<GameController>().AddToScore(scoreValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealerController damageDealerController = collision.gameObject.GetComponent<DamageDealerController>();
        if (!damageDealerController) { return; }
        RegisterDamage(damageDealerController.GetDamage());
        // Destroy the laser
        damageDealerController.Hit();
    }
}
