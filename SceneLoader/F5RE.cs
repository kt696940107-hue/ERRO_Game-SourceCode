using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class F5RE : MonoBehaviour
{
    public KeyCode keyToLoadScene = KeyCode.F5; // 指定的按键为 F5
    public string sceneToLoad = "YourSceneName"; // 要加载的场景名称

    void Update()
    {
        // 检测玩家是否按下了指定的按键
        if (Input.GetKeyDown(keyToLoadScene))
        {
            // 加载指定的场景
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}