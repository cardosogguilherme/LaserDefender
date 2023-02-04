using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("Enemy AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.2f;

    [HideInInspector]
    public bool isFiring;
    [HideInInspector]
    public bool hadFinishedFiring = true;
    
    private Coroutine firingCoroutine;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null) 
        {
            firingCoroutine = StartCoroutine(FireContinuously()); 
        }
        else if(!isFiring && firingCoroutine != null)
        { 
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
        
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            var projectile = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity);

            var rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity= transform.up * projectileSpeed;
            }

            Destroy(projectile, projectileLifetime);
            hadFinishedFiring = false;

            float firingInterval = useAI ? GetFiringWaitTime() : baseFiringRate;

            audioPlayer.PlayShootingClip();

            yield return new WaitForSecondsRealtime(firingInterval);

            hadFinishedFiring = true;
        }
    }

    private float GetFiringWaitTime()
    {
        float timeToNextProjectile = UnityEngine.Random.Range(
            baseFiringRate - firingRateVariance,
            baseFiringRate + firingRateVariance);

        return Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
    }
}
