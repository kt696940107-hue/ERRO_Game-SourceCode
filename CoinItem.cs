using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    public AudioClip collectSound; // 要播放的音效
    private bool isCollected = false; // 標誌是否已經被收集
    private AudioSource playerAudioSource; // 玩家的 AudioSource

    void Start()
    {
        // 取得玩家的 AudioSource
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerAudioSource = player.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            CapsuleCollider2D capsuleCollider = other.GetComponent<CapsuleCollider2D>();
            if (capsuleCollider != null)
            {
                int randomValue = Random.Range(1, 5); // 生成 1 到 4 之間的隨機整數
                CoinUI.CurrentCoinQuantity += randomValue;

                // 播放音效
                if (playerAudioSource != null && collectSound != null)
                {
                    playerAudioSource.PlayOneShot(collectSound);
                }

                isCollected = true; // 設置標誌為已經被收集
                Destroy(gameObject); // 如果你還需要破壞物件，這行可以保留
            }
        }
    }
}
