using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;
    public float fakeLoadingTime = 2.0f; // 模拟加载时间，单位：秒

    void Start()
    {
        // 启动协程来加载目标场景
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // 确保将 "TargetSceneName" 替换为你实际的场景名称
        string targetSceneName = PlayerPrefs.GetString("TargetSceneName"); // 从 PlayerPrefs 获取目标场景名称
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetSceneName);
        operation.allowSceneActivation = false; // 禁止场景立即激活

        float elapsedTime = 0f;

        while (!operation.isDone)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01((operation.progress / 0.9f) * (elapsedTime / fakeLoadingTime));
            progressBar.value = progress;

            // 如果加载进度达到了90%并且模拟加载时间也达到了指定时间，则激活场景
            if (operation.progress >= 0.9f && elapsedTime >= fakeLoadingTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
