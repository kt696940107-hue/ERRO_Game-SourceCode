using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoInteractiveDialogue : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string[] dialogLines;  // 存储对话的内容
    public float typingSpeed;  // 文字打印速度
    public string nextSceneName;  // 下一个场景的名称

    public AudioClip typingSound;  // 打字音效
    private AudioSource audioSource;  // 音效源

    private bool isDialoguePlaying;
    private int currentLineIndex;
    private Coroutine typingCoroutine;  // 打字的协程引用

    private bool isSkipping;

    void Start()
    {
        dialogBox.SetActive(false);
        isDialoguePlaying = false;
        currentLineIndex = 0;
        isSkipping = false;

        audioSource = GetComponent<AudioSource>();  // 获取音效源组件

        // 立即触发对话
        StartDialogue();
    }

    void Update()
    {
        if (isDialoguePlaying && Input.GetKeyDown(KeyCode.Space) && !isSkipping)  // 空格键可以跳过对话
        {
            if (isSkipping)
            {
                // 如果正在跳过，则立即显示完整内容
                CompleteCurrentLine();
            }
            else
            {
                // 如果不在跳过状态，则开始跳过当前段落
                StartCoroutine(SkipParagraph());
            }
        }
    }

    void StartDialogue()
    {
        if (dialogLines.Length > 0)
        {
            isDialoguePlaying = true;
            dialogBox.SetActive(true);

            // 开启打字协程
            typingCoroutine = StartCoroutine(TypeDialog());
        }
        else
        {
            // 对话内容为空，直接进入下一个场景
            GoToNextScene();
        }
    }

    IEnumerator TypeDialog()
    {
        dialogText.text = "";  // 清空对话框的文字

        for (int i = 0; i < dialogLines.Length; i++)
        {
            string line = dialogLines[i];
            dialogText.text = "";

            for (int j = 0; j < line.Length; j++)
            {
                dialogText.text += line[j];
                PlayTypingSound();  // 播放打字音效
                yield return new WaitForSeconds(typingSpeed);

                if (isSkipping)
                {
                    // 如果在跳过状态，则立即显示完整内容并跳出循环
                    CompleteCurrentLine();
                    break;
                }
            }

            if (!isSkipping && i < dialogLines.Length - 1)
            {
                // 如果不在跳过状态且不是最后一行，则等待一段时间后清空对话框
                yield return new WaitForSeconds(1.0f);
                dialogText.text = "";
                yield return new WaitForSeconds(0.5f);
            }
        }

        // 对话播放完毕
        isDialoguePlaying = false;
        dialogBox.SetActive(false);
        GoToNextScene();
    }

    void PlayTypingSound()
    {
        if (typingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(typingSound);
        }
    }

    void CompleteCurrentLine()
    {
        // 显示完整内容
        StopCoroutine(typingCoroutine);
        dialogText.text = dialogLines[currentLineIndex];

        // 对话播放完毕
        currentLineIndex++;

        if (currentLineIndex >= dialogLines.Length)
        {
            // 所有对话都已经播放完毕，关闭对话框并进入下一个场景
            dialogBox.SetActive(false);
            isDialoguePlaying = false;
            GoToNextScene();
        }
    }

    IEnumerator SkipParagraph()
    {
        isSkipping = true;

        while (currentLineIndex < dialogLines.Length)
        {
            CompleteCurrentLine();
            yield return new WaitForSeconds(0.5f);
        }

        isSkipping = false;
    }

    void GoToNextScene()
    {
        // 切换到下一个场景
        SceneManager.LoadScene(nextSceneName);
    }
}
