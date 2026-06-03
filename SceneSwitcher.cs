using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string targetSceneName; // 目標場景的名稱
    public float transitionDelay = 1f; // 過渡延遲時間
    public Animator transitionAnimator; // 過渡動畫器

    private bool isSwitchingScene = false; // 是否正在切換場景

    private void Update()
    {
        // 按下指定按鍵觸發場景切換
        if (Input.GetKeyDown(KeyCode.Q) && !isSwitchingScene)
        {
            isSwitchingScene = true;
            StartCoroutine(SwitchScene());
        }
    }

    IEnumerator SwitchScene()
    {
        // 播放過渡動畫
        if (transitionAnimator != null)
            transitionAnimator.SetTrigger("Start");

        // 等待過渡延遲時間
        yield return new WaitForSeconds(transitionDelay);

        // 開始異步加載目標場景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);

        // 等待加載完成
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 停止過渡動畫
        if (transitionAnimator != null)
            transitionAnimator.SetTrigger("End");

        // 切換場景完成
        isSwitchingScene = false;
    }
}
