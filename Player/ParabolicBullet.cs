using System.Collections;
using UnityEngine;

public class ParabolicBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float gravity = 9.81f; // 重力加速度
    public Rigidbody2D rb;
    public GameObject impactEffect;

    private Vector2 initialVelocity;

    // Start is called before the first frame update
    void Start()
    {
        // 計算初始速度以便使子彈成拋物線軌跡移動
        initialVelocity = transform.right * speed;

        // 將初始速度套用到 Rigidbody
        rb.velocity = initialVelocity;
    }

    private void Update()
    {
        // 每幀更新子彈的 Y 方向速度，以模擬重力影響
        rb.velocity += Vector2.down * gravity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Bullet collided with: " + hitInfo.gameObject.name);

        // 檢查碰撞物體的標籤
        if (hitInfo.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Dealing damage to enemy!");
            }
            else
            {
                Debug.Log("Enemy component not found on the collided object!");
            }
        }
        else if (hitInfo.CompareTag("Ground"))
        {
            // 可以在這裡處理子彈與地面的碰撞
        }
        else if (hitInfo.CompareTag("Item"))
        {
            // 忽略與標籤為 "Item" 的碰撞框的碰撞
            Debug.Log("Ignoring collision with Item");
            return;
        }
        else if (hitInfo.CompareTag("Bullet"))
        {
            // 忽略與標籤為 "Bullet" 的碰撞框的碰撞
            Debug.Log("Ignoring collision with Bullet");
            return;
        }

        // 在碰撞點創建特效
        GameObject impactEffectInstance = Instantiate(impactEffect, transform.position, transform.rotation);

        // 銷毀子彈
        Destroy(gameObject);

        // 銷毀特效（可以設置延遲時間，或者在特效的 Particle System 完成後銷毀）
        Destroy(impactEffectInstance, 1.0f); // 這裡的 1.0f 是延遲時間，可以根據實際情況調整
    }
}
