using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Config params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] int padding = 1;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] public GameObject greenLaserPrefab;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 1f;

    [Header("Sound FX")]
    [SerializeField] public AudioClip playerDestroyedSFX;
    [SerializeField] public AudioClip playerFireSFX;
    [SerializeField] [Range(0, 1)] float playerDestroyedSFXVolume = 1f;
    [SerializeField] [Range(0, 1)] float playerFireSFXVolume = 0.25f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start()
    {
        SetUpMoveBoundaries();
    }   

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleFire();
    }

    private void HandleFire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while(true) {
            // Kick off an endless firing loop within the coroutine until it is cancelled on keyup
            AudioSource.PlayClipAtPoint(playerFireSFX, Camera.main.transform.position, playerFireSFXVolume);
            GameObject laser = Instantiate(
                greenLaserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void RegisterDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, health);

        if (health <= 0)
        {
            DestroyPlayer();
        }
    }

    private void DestroyPlayer()
    {
        FindObjectOfType<LevelController>().LoadGameOver();
        AudioSource.PlayClipAtPoint(playerDestroyedSFX, Camera.main.transform.position, playerDestroyedSFXVolume);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealerController damageDealerController = collision.gameObject.GetComponent<DamageDealerController>();
        if (!damageDealerController) { return; }
        RegisterDamage(damageDealerController.GetDamage());
        // Destroy the laser
        damageDealerController.Hit();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    public int GetHealth()
    {
        return health;
    }
}
