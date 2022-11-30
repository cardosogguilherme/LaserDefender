using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    public bool isFiring;
    public bool hadFinishedFiring = true;
    private Coroutine firingCoroutine;

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

            yield return new WaitForSecondsRealtime(firingRate);

            hadFinishedFiring = true;
        }
    }
}
