using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveDistanceX = 5f;
    public float moveDistanceY = 5f;
    public float trackingRange = 10f; // 追踪范围

    private Transform player;
    private Vector2 originalPosition;
    private Vector2 targetPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
        CalculateTargetPosition();
    }

    void Update()
    {
        if (IsPlayerInTrackingRange())
        {
            // 如果玩家在追踪范围内，追踪玩家
            targetPosition = player.position;
        }
        else
        {
            // 否则继续随机移动
            Move();
        }
    }

    void Move()
    {
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            // 已经移动到目标位置，重新计算目标位置
            CalculateTargetPosition();
        }
    }

    void CalculateTargetPosition()
    {
        // 随机选择新的目标位置
        float randomX = Random.Range(originalPosition.x - moveDistanceX, originalPosition.x + moveDistanceX);
        float randomY = Random.Range(originalPosition.y - moveDistanceY, originalPosition.y + moveDistanceY);
        targetPosition = new Vector2(randomX, randomY);
    }

    bool IsPlayerInTrackingRange()
    {
        return Vector2.Distance(transform.position, player.position) <= trackingRange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞物体的标签
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(10); // 在这里设定伤害值
            }
        }
    }
}
