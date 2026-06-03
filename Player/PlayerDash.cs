using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 20f; // 衝刺速度
    public float dashDistance = 5f; // 衝刺距離
    public float dashTime = 0.5f; // 衝刺時間
    public float cooldownTime = 1f; // 冷卻時間
    public KeyCode dashKey = KeyCode.LeftAlt; // 衝刺按鍵
    public Animator animator; // 角色的Animator組件

    private bool isDashing = false;
    private bool isCooldown = false;
    public int damage;
    private PolygonCollider2D m_collider2D;

    void Start()
    {
        m_collider2D = GetComponent<PolygonCollider2D>();

        if (animator == null)
        {
            // 如果未指定Animator，則嘗試從同一個遊戲物件中獲取
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (!isDashing && !isCooldown && Input.GetKeyDown(dashKey))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;

        // 播放衝刺動畫
        if (animator != null)
        {
            animator.SetTrigger("Dash");
        }

        // 計算目標位置
        Vector3 endPos = transform.position + transform.right * dashDistance;

        // 保存原始位置
        Vector3 startPos = transform.position;

        // 衝刺的時間
        float elapsed = 0f;

        while (elapsed < dashTime)
        {
            // 計算下一個位置
            float dashStep = dashSpeed * Time.deltaTime;

            // 偵測衝刺方向上的碰撞
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, dashStep);

            if (hit.collider != null && hit.collider.GetComponent<BoxCollider2D>() != null)
            {
                // 如果碰撞到了BoxCollider2D，則停止移動
                break;
            }

            transform.position = Vector3.MoveTowards(transform.position, endPos, dashStep);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 確保最終位置正確
        transform.position = endPos;

        // 恢復為非衝刺狀態
        isDashing = false;

        // 開始冷卻時間
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.GetComponent<Enemy>() != null)
        {
            // 检查目标物体是否具有 SlimeEnemy 组件
            Enemy slimeEnemy = other.GetComponent<Enemy>();
            if (slimeEnemy != null)
            {
                slimeEnemy.TakeDamage(damage);
                Debug.Log("Player attacked enemy! Damage: " + damage);
            }
        }
    }
}
