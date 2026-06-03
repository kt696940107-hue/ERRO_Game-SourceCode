using UnityEngine;

public class DestroyAfterEffect : MonoBehaviour
{
    public float duration = 2.0f; // 特效持續時間

    void Start()
    {
        // 在指定時間後銷毀物件
        Destroy(gameObject, duration);
    }
}
