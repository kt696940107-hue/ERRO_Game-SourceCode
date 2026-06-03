using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign_AU : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogBoxText;
    public string[] signTexts;  // 存储预设的文字
    public AudioClip soundEffect;  // 音效
    private bool isPlayerInSign;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        isPlayerInSign = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 如果玩家在标志范围内，且按下E键，则显示对话框
        if (isPlayerInSign)
        {
            ShowRandomText();
        }
    }

    // 显示随机文字
    private void ShowRandomText()
    {
        // 如果对话框已经激活，则关闭它
        if (dialogBox.activeSelf)
        {
           // dialogBox.SetActive(false);
        }
        else
        {
            // 随机选择一条文本显示在对话框中
            string randomText = signTexts[Random.Range(0, signTexts.Length)];
            dialogBoxText.text = randomText;
            dialogBox.SetActive(true);

            // 播放音效
            PlaySoundEffect();
        }
    }

    // 播放音效
    private void PlaySoundEffect()
    {
        if (soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }

    // 玩家进入标志范围内
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInSign = true;
        }
    }

    // 玩家离开标志范围
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInSign = false;

            // 在销毁对象之前检查是否为null
            if (dialogBox != null)
            {
                dialogBox.SetActive(false);
            }
        }
    }
}
