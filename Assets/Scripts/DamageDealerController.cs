using UnityEngine;

public class DamageDealerController : MonoBehaviour
{
    [SerializeField] int damage = 100;
    public int GetDamage() {
        return damage;
    }

    public void Hit() {
        Destroy(gameObject);
    }
}
