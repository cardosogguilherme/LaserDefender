using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer == null) return;
        
        TakeDamage(damageDealer.GetDamage());
        PlayHitEffect();

        damageDealer.Hit();
    }

    private void PlayHitEffect()
    {
        if (hitEffect == null) return;

        var instance = Instantiate(
            hitEffect, 
            transform.position,
            Quaternion.identity);

        Destroy(
            instance.gameObject, 
            instance.main.duration + instance.main.startLifetime.constantMax);
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0) Destroy(gameObject);
    }
}
