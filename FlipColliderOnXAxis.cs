using UnityEngine;

public class FlipColliderOnXAxis : MonoBehaviour
{
    void Update()
    {
        // 獲取當前物體在 X 軸上的移動方向
        float moveDirection = Input.GetAxisRaw("Horizontal");

        // 根據移動方向調整朝向
        if (moveDirection < 0)
        {
            // 向左移動時翻轉物件
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            // 向右移動時恢復物件
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}