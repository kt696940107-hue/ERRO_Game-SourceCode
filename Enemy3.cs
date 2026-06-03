using System.Collections;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public int health;
    public float moveSpeed = 3f;
    public float trackingRange = 5f;
    public float attackRange = 1f;
    public int maxHealth = 100;
    public int attackDamage = 10;
    public DropItem[] dropItems;
    public int maxDropCount = 3;
    public int flashCount = 5;
    public float flashSpeed = 0.1f;
    public Color flashColor = Color.red;
    public AudioClip damageSound;
    public GameObject deathEffectPrefab;

    private int currentHealth;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private float nextMoveTime = 0f;
    private float moveDuration = 0f;
    private int currentDirection = 1;

    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab;
        [Range(0f, 1f)]
        public float dropProbability;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        CheckAttackRange();
    }

    void Move()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < trackingRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // 根据怪物与玩家的位置关系调整朝向
            //if (direction.x < 0)
            {
                spriteRenderer.flipX = false; // 向左移动时翻转图像
            }
           // else if (direction.x > 0)
            {
                spriteRenderer.flipX = true; // 向右移动时恢复图像
            }
        }
        else
        {
            // 检查是否需要改变移动方向
           // if (Time.time >= nextMoveTime)
            {
                // 重新计时
              //  moveDuration = Random.Range(3f, 2f); // 随机移动持续时间为3~6秒
              //  nextMoveTime = Time.time + moveDuration;

                // 随机选择移动方向
              //  currentDirection = Random.Range(0, 2) == 0 ? -1 : 1; // 0为向左，1为向右

                // 更新朝向
               // spriteRenderer.flipX = currentDirection < 0;
            }

            // 移动
           // transform.Translate(Vector2.right * moveSpeed * currentDirection * Time.deltaTime);

            // 根据移动方向调整朝向
           // if (currentDirection < 0)
            {
                spriteRenderer.flipX = false; // 向左移动时翻转图像
            }
           // else if (currentDirection > 0)
            {
                spriteRenderer.flipX = true; // 向右移动时恢复图像
            }
        }
    }
    void CheckAttackRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < attackRange)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.DamagePlayer(attackDamage);
        }
    }

    void Die()
    {
        // 播放死亡特效
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        // 生成掉落物品
        if (dropItems != null && dropItems.Length > 0)
        {
            for (int i = 0; i < maxDropCount; i++)
            {
                foreach (var dropItem in dropItems)
                {
                    if (Random.Range(0f, 1f) <= dropItem.dropProbability)
                    {
                        Instantiate(dropItem.itemPrefab, transform.position, Quaternion.identity);
                        break;
                    }
                }
            }
        }

        // 销毁怪物对象
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
            PlayDamageSound();
        }
    }

    private void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    IEnumerator FlashRed()
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashSpeed);

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashSpeed);
        }
    }
}
