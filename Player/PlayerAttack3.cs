using System.Collections;
using UnityEngine;

public class PlayerAttack3 : MonoBehaviour
{
    public int damage;
    public float startTime;
    public float time;
    public AudioClip attackSound;
    public bool UpAttack;

    private Animator anim;
    private PolygonCollider2D m_collider2D;
    private AudioSource audioSource;
    private bool isAnimating = false;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        m_collider2D = GetComponent<PolygonCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isAnimating)
        {
            return;
        }

        // 检查是否按下攻击键并且按住上箭头键
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetButtonDown("Attack"))
        {
            UpAttack = true;
            anim.SetTrigger("UpAttack");
            StartCoroutine(StartAttack());
        }
    }

    private IEnumerator StartAttack()
    {
        isAnimating = true;
        DisableOtherAttackScripts();

        yield return new WaitForSeconds(startTime);
        m_collider2D.enabled = true;
        PlayAttackSound();
        StartCoroutine(DisableHitBox());
        yield return new WaitForSeconds(time);
        m_collider2D.enabled = false;
        yield return new WaitForSeconds(startTime + time);
        isAnimating = false;
        EnableOtherAttackScripts();
    }

    private void DisableOtherAttackScripts()
    {
        var attackScript1 = GetComponent<PlayerAttack>();
        var attackScript2 = GetComponent<PlayerAttack2>();
        if (attackScript1 != null) attackScript1.enabled = false;
        if (attackScript2 != null) attackScript2.enabled = false;
    }

    private void EnableOtherAttackScripts()
    {
        var attackScript1 = GetComponent<PlayerAttack>();
        var attackScript2 = GetComponent<PlayerAttack2>();
        if (attackScript1 != null) attackScript1.enabled = true;
        if (attackScript2 != null) attackScript2.enabled = true;
    }

    private IEnumerator DisableHitBox()
    {
        yield return new WaitForSeconds(time);
        m_collider2D.enabled = false;
    }

    private void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.GetComponent<Enemy>() != null)
        {
            Enemy slimeEnemy = other.GetComponent<Enemy>();
            if (slimeEnemy != null)
            {
                slimeEnemy.TakeDamage(damage);
                Debug.Log("Player attacked enemy! Damage: " + damage);
            }
        }
    }
}
