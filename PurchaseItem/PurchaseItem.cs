using System.Collections;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{
    public GameObject itemPrefab; // 要生成的道具预制体
    public int itemCost = 10; // 道具的花费
    public AudioClip purchaseSound; // 购买时的音效

    private bool playerInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否为玩家进入范围
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 检查是否为玩家离开范围
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        // 检查玩家是否在范围内并按下"E"键
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // 检查玩家金币数量是否足够购买道具
            if (CoinUI.CurrentCoinQuantity >= itemCost)
            {
                // 减少玩家金币数量
                CoinUI.CurrentCoinQuantity -= itemCost;

                // 生成道具
                Instantiate(itemPrefab, transform.position, Quaternion.identity);

                // 播放购买音效
                PlayPurchaseSound();
            }
            else
            {
                Debug.Log("Not enough coins to purchase the item.");
            }
        }
    }

    private void PlayPurchaseSound()
    {
        // 检查是否设置了购买音效并播放
        if (purchaseSound != null)
        {
            AudioSource.PlayClipAtPoint(purchaseSound, transform.position);
        }
    }
}
