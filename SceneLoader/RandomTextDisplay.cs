using UnityEngine;
using UnityEngine.UI;

public class RandomTextDisplay : MonoBehaviour
{
    public Text textDisplay;
    public string[] randomTexts;

    void Start()
    {
        // 检查是否连接了Text组件
        if (textDisplay == null)
        {
            Debug.LogError("Text component is not assigned to the script!");
            return;
        }

        // 检查是否至少有一个随机文本
        if (randomTexts.Length == 0)
        {
            Debug.LogError("No random texts assigned to the script!");
            return;
        }

        // 在UI上显示随机文本
        ShowRandomText();
    }

    void ShowRandomText()
    {
        // 从随机文本数组中随机选择一个文本
        int randomIndex = Random.Range(0, randomTexts.Length);
        string randomText = randomTexts[randomIndex];

        // 将随机文本显示在UI上的Text组件中
        textDisplay.text = randomText;
    }
}
