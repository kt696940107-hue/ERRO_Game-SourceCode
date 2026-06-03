using System.Collections;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    public float rotationSpeed = 180f;
    public float lifeTime = 5f;
    public GameObject explosionEffectPrefab;

    private Transform target;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyAfterTime());
        FindNearestTarget();
    }

    void Update()
    {
        if (target != null)
        {
            // 计算朝向目标的方向
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            // 计算旋转后的方向
            Vector2 rotatedDirection = Vector2.Lerp(transform.up, directionToTarget, Time.deltaTime * rotationSpeed);

            // 设置旋转后的方向
            transform.up = rotatedDirection;

            // 同时朝目标移动
            rb.velocity = rotatedDirection * speed;
        }
        else
        {
            // 如果 target 为 null，可以进行相应的处理，例如停止子弹的移动或销毁子弹
            // 你可以根据实际需求进行调整
            Debug.LogWarning("Target is null. Stopping bullet movement or taking other actions.");
            rb.velocity = Vector2.zero; // 停止子弹移动
            // 或者
            Destroy(gameObject); // 销毁子弹
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject explosionEffectInstance = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            Enemy enemy = other.GetComponent
            <Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
            Destroy(explosionEffectInstance, 1.0f);
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }
    }
}
