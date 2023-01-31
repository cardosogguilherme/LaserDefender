using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool shouldApplyCameraShake;
    CameraShake cameraShake;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer == null) return;
        
        TakeDamage(damageDealer.GetDamage());
        PlayHitEffect();
        ShakeCamera();

        damageDealer.Hit();
    }

    private void ShakeCamera()
    {
        if(cameraShake != null && shouldApplyCameraShake)
        {
            cameraShake.Play();
        }
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
