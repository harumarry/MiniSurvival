using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    [Header("BGM Clip")]
    [SerializeField] AudioClip bgmClip;

    [Header("SFX Clips")]
    [SerializeField] AudioClip playerHitClip;
    [SerializeField] AudioClip enemyHitClip;
    [SerializeField] AudioClip enemyDieClip;
    [SerializeField] AudioClip expGetClip;
    [SerializeField] AudioClip levelUpClip;
    [SerializeField] AudioClip skillSelectClip;
    [SerializeField] AudioClip gameOverClip;
    [SerializeField] AudioClip projectileFireClip;
    [SerializeField] AudioClip victoryClip;

    float enemyHitCooldown = 0.05f;
    float lastEnemyHitTime;

    private void Start()
    {
        PlayBgm();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void PlayBgm()
    {
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    public void PlayPlayerHit()
    {      
        PlaySfx(playerHitClip, 0.6f);      
    }
    
    public void PlayEnemyHit()
    {
        if (Time.unscaledTime - lastEnemyHitTime < enemyHitCooldown) return;

        PlaySfx(enemyHitClip, 0.3f);

        lastEnemyHitTime = Time.unscaledTime;
    }

    public void PlayEnemyDie()
    {
        PlaySfx(enemyDieClip, 0.5f);
    }

    public void PlayExpGet()
    {
        PlaySfx(expGetClip, 0.3f);
    }

    public void PlayLevelUp()
    {
        PlaySfx(levelUpClip, 0.2f);
    }

    public void PlaySkillSelect()
    {
        PlaySfx(skillSelectClip, 0.3f);
    }

    public void PlayGameOver()
    {
        PlaySfx(gameOverClip, 0.1f);
    }

    public void PlayProjectileFire()
    {
        PlaySfx(projectileFireClip, 0.3f);
    }

    public void PlayVictory()
    {
        PlaySfx(victoryClip, 0.5f);
    }

    void PlaySfx(AudioClip clip, float volume)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.PlayOneShot(clip, volume);
    }
}
