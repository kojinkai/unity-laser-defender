using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100;

    [Header("Attack Params")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] public GameObject redLaserPrefab;

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
            Destroy(gameObject);
        }
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
