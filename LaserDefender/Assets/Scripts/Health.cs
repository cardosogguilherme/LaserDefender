using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool shouldApplyCameraShake;
    CameraShake cameraShake;
    ScoreKeeper scoreKeeper;

    AudioPlayer audioPlayer;
    LevelManager levelManager;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer == null) return;
        
        TakeDamage(damageDealer.GetDamage());
        PlayHitEffect();
        ShakeCamera();
        audioPlayer.PlayDamageTakenClip();

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

        if (health <= 0)
        {
            if (!isPlayer)
            {
                scoreKeeper.AddPoints(score);
            }
            else
            {
                //scoreKeeper.ResetScore();
                levelManager.LoadGameOver();
            }
            Debug.Log(scoreKeeper.GetScore());

            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
