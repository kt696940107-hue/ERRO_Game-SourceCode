using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 要跟随的目标对象（玩家角色）
    public float smoothSpeed = 0.125f; // 摄像机平滑移动的速度
    public Vector3 offset; // 摄像机相对于目标的偏移量

    void FixedUpdate()
    {
        if (target != null)
        {
            // 计算摄像机的目标位置
            Vector3 desiredPosition = target.position + offset;
            // 使用Lerp函数平滑地移动摄像机
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // 更新摄像机的位置
            transform.position = smoothedPosition;
        }
    }
}
