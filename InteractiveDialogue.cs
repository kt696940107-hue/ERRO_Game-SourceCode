using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveDialogue : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string[] dialogLines;  // 存儲對話的內容
    public float typingSpeed;  // 文字打印速度

    private bool isPlayerInRange;
    private bool isDialoguePlaying;
    private int currentLineIndex;
    private bool isEventTriggered;  // 事件是否已触发
    private PlayerController playerController;  // 玩家控制脚本的引用

    // 添加一个标志，表示是否玩家按下空格键跳过当前段落
    private bool skipDialogue;

    void Start()
    {
        dialogBox.SetActive(false);
        isDialoguePlaying = false;
        currentLineIndex = 0;
        isEventTriggered = false;
        skipDialogue = false;

        // 获取玩家控制脚本的引用
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (isPlayerInRange && !isDialoguePlaying && !isEventTriggered)
        {
            StartDialogue();
        }

        // 在 Update 中检测玩家是否按下空格键以跳过对话
        if (Input.GetKeyDown(KeyCode.Space) && isDialoguePlaying)
        {
            skipDialogue = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void StartDialogue()
    {
        if (currentLineIndex < dialogLines.Length)
        {
            isDialoguePlaying = true;
            dialogBox.SetActive(true);

            // 禁用玩家控制脚本
            playerController.enabled = false;

            StartCoroutine(TypeDialog());
        }
    }

    IEnumerator TypeDialog()
    {
        dialogText.text = "";  // 清空對話框的文字

        foreach (char letter in dialogLines[currentLineIndex].ToCharArray())
        {
            if (skipDialogue) // 如果玩家按下空格键，则直接显示整个段落
            {
                dialogText.text = dialogLines[currentLineIndex];
                skipDialogue = false;
                break;
            }

            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // 對話播放完畢
        isDialoguePlaying = false;
        currentLineIndex++;

        if (currentLineIndex >= dialogLines.Length)
        {
            // 所有對話都已經播放完畢，關閉對話框
            dialogBox.SetActive(false);
            isEventTriggered = true;  // 设置事件已触发

            // 启用玩家控制脚本
            playerController.enabled = true;
        }
    }
}
