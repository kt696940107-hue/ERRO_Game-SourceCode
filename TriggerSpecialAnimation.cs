using UnityEngine;

public class TriggerSpecialAnimation : MonoBehaviour
{
    private Animator animator;
    private bool playerInside;

    void Start()
    {
        // 取得角色的 Animator 組件
        animator = GetComponent<Animator>();

        // 確保 Animator 組件存在
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the character.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 確認碰撞的物體是否是玩家
        if (other.CompareTag("Player"))
        {
            // 播放特定的動畫
            animator.SetTrigger("TriggerAnimation");
            // 設置玩家在碰撞器內
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 確認碰撞的物體是否是玩家，並且玩家之前在碰撞器內
        if (other.CompareTag("Player") && playerInside)
        {
            // 設置玩家不在碰撞器內
            playerInside = false;
        }
    }

    void Update()
    {
        // 如果玩家不在碰撞器內，將動畫設置為 Idle
        if (!playerInside)
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            // 如果玩家在碰撞器內，將 Idle 動畫設置為 false
            animator.SetBool("Idle", false);
        }
    }
}
