using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Taking Damage")]
    [SerializeField] AudioClip damageTakenClip;
    [SerializeField][Range(0f, 1f)] float damageTakenVolume = 1f;

    private static AudioPlayer instance;

    public AudioPlayer GetInstance()
    {
        return instance;
    }
    
    private void Awake()
    {
        ManageSingleton();
    }
    
    void ManageSingleton()
    {
        //int instanceCount = FindObjectsOfType(GetType()).Length;
        //if (instanceCount > 1)
        //{
        //    gameObject.SetActive(false);
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}

        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } 
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageTakenClip()
    {
        PlayClip(damageTakenClip, damageTakenVolume);
    }

    private void PlayClip(AudioClip targetClip, float volume)
    {
        if (targetClip != null)
        {
            AudioSource.PlayClipAtPoint(
                targetClip,
                Camera.main.transform.position,
                volume
                );
        }
    }
}
