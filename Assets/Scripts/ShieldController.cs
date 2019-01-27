using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [Header("Player Object")]
    [SerializeField] PlayerController player;

    [Header("Shield Config")]
    [SerializeField] float verticalOffset = 1.5f;
    [SerializeField] float shieldDuration = 1f;
    [SerializeField] int shieldCapacity = 100;
   
    [Header("Animator")]
    [SerializeField] Animator animator;

    Vector3 shieldOffset;

    private void Start()
    {
        shieldOffset = new Vector3(0, -verticalOffset, 0);
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            transform.position = player.transform.position - shieldOffset;
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

    private void DestroyShield() {
        Destroy(gameObject);
    }

    private void RegisterDamage(int damage)
    {
        shieldCapacity -= damage;

        if (shieldCapacity <= 0)
        {
            DestroyShield();
        }
        else 
        {
            ActivateShield();
        }
    }

    private void ActivateShield()
    {
        animator.SetTrigger("ActivateShield");
    }

    public void DeactivateShield()
    {
        animator.SetTrigger("DeactivateShield");

    }

}
