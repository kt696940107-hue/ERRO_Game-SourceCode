using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int blinks;
    public float time;
    public float dieTime;
    public float invincibleTime;
    public string restartSceneName; // 指定的重新开始场景名称
    public AudioClip damageSound;

    private Renderer myRender;
    private Animator anim;
    private bool isDead = false;
    private bool isInvincible = false;
    private PlayerController playerController;
    private AudioSource audioSource;

    private void Start()
    {
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            DamagePlayer(20);
        }
    }

    public void DamagePlayer(int damage)
    {
        if (isDead || isInvincible)
        {
            return;
        }

        health -= damage;
        HealthBar.HealthCurrent = health;

        if (health <= 0)
        {
            isDead = true;
            anim.SetTrigger("Die");

            // 停止玩家行动
            if (playerController != null)
            {
                playerController.enabled = false;
            }

            StartCoroutine(DelayedRestartLevel(dieTime));
        }
        else
        {
            isInvincible = true;
            StartCoroutine(InvincibleCooldown(invincibleTime));
            BlinkPlayer(blinks, time);
            PlayDamageSound();
        }
    }

    public void RestoreHealthInstant(int amount)
    {
        health = Mathf.Min(health + amount, HealthBar.HealthMax);
        HealthBar.HealthCurrent = health;
    }

    private IEnumerator DelayedRestartLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(restartSceneName);
        //SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator InvincibleCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        isInvincible = false;
    }

    private void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    private IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }

    private void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }
}
