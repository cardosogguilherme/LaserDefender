using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer == null) return;
        
        TakeDamage(damageDealer.GetDamage());
        
        damageDealer.Hit();
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0) Destroy(gameObject);
    }
}
