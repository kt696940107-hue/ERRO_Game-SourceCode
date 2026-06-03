using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInSign)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                string randomText = signTexts[Random.Range(0, signTexts.Length)];
                dialogBoxText.text = randomText;
                dialogBox.SetActive(true);

                PlaySoundEffect();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = false;
            if (dialogBox != null)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    void PlaySoundEffect()
    {
        if (soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }
}
