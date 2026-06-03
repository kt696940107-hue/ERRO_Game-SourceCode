using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject[] items; // 存放不同種類的道具
    public float delayTime;
    public AudioClip openSound; // 寶箱打開的音效

    private bool canOpen;
    private bool isOpened;
    private Animator anim;
    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        isOpened = false;
        audioSource = GetComponent<AudioSource>(); // 獲取 AudioSource 組件
        audioSource.playOnAwake = false; // 確保音效不會在啟動時自動撥放
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canOpen && !isOpened)
            {
                OpenBox();
            }
        }
    }

    void OpenBox()
    {
        // 撥放寶箱打開音效
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        anim.SetTrigger("Opening");
        isOpened = true;
        Invoke("GenItem", delayTime);
    }

    void GenItem()
    {
        // 隨機選擇一個道具
        GameObject itemToSpawn = items[Random.Range(0, items.Length)];
        Instantiate(itemToSpawn, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
        }
    }
}
