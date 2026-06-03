using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string targetSceneName; // 可以在检视面板中输入目标场景名称

    void OnTriggerStay2D(Collider2D other)
    {
        // 检查是否碰撞到了玩家并且玩家按下了 E 键
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            // 将目标场景名称存储到 PlayerPrefs 中
            PlayerPrefs.SetString("TargetSceneName", targetSceneName);
            // 加载过渡场景
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
