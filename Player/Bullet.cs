using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
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
