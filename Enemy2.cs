using System.Collections;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    // 可在 Unity 編輯器中設置的屬性
    public int health;
    public float moveSpeed = 3f;
    public float trackingRange = 5f;
    public float attackRange = 1f;
    public int maxHealth = 100;
    public int attackDamage = 10;
    public DropItem[] dropItems; // 用於設定掉落的物品和機率
    public int maxDropCount = 3; // 最多掉落的物品數量
    public int flashCount = 5; // 閃爍次數
    public float flashSpeed = 0.1f; // 閃爍速度
    public Color flashColor = Color.red; // 閃爍顏色
    public AudioClip damageSound; // 新增受傷音效

    // 目前屬性
    private int currentHealth;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource; // 新增 AudioSource 組件

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
        audioSource = GetComponent<AudioSource>(); // 取得 AudioSource 組件
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
            // 如果玩家在追蹤範圍內，朝玩家衝刺
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            // 玩家不在追蹤範圍內，隨機移動
            if (Random.Range(0f, 1f) < 0.02f)
            {
                // 有 2% 的機率改變移動方向
                moveSpeed *= -1f;
            }

            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
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
        // 在這裡實現怪物的攻擊行為，例如扣除玩家血量
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.DamagePlayer(attackDamage);
        }
    }

    void Die()
    {
        // 在這裡實現怪物死亡行為，例如播放死亡動畫、掉落物品等
        StartCoroutine(FlashRed());
        if (dropItems != null && dropItems.Length > 0)
        {
            for (int i = 0; i < maxDropCount; i++)
            {
                foreach (var dropItem in dropItems)
                {
                    // 根據機率判斷是否掉落物品
                    if (Random.Range(0f, 1f) <= dropItem.dropProbability)
                    {
                        // 生成掉落物品
                        Instantiate(dropItem.itemPrefab, transform.position, Quaternion.identity);
                        break; // 最多掉落一個，直接離開迴圈
                    }
                }
            }
        }

        // 銷毀怪物物件
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        // 在這裡實現怪物受傷行為，例如扣除怪物血量
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
            PlayDamageSound(); // 播放受傷音效
        }
    }

    private void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource
.PlayOneShot(damageSound);
        }
    }

    IEnumerator FlashRed()
    {
        // 閃爍紅光效果
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashSpeed);

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashSpeed);
        }
    }
}
