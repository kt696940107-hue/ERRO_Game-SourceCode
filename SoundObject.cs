using UnityEngine;

public class SoundObject : MonoBehaviour
{
    public GameObject playerObject; // 玩家物件
    public GameObject targetObject; // 被觸碰物件
    public AudioClip soundClip;     // 音效
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == playerObject)
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        if (soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }
}
