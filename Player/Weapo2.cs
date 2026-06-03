using System.Collections;
using UnityEngine;

public class Weapo2 : MonoBehaviour
{
    public Transform firePoint2; // 第二個發射點
    public BulletSettings[] bulletSettings;
    private int _currentBulletIndex = 0;
    private int shotsFired = 0;
    private float nextFireTime = 0f;
    private Animator animator;
    private AudioSource shootAudio;
    public AudioClip shootSoundClip;

    public int currentBulletIndex
    {
        get { return _currentBulletIndex; }
        set { _currentBulletIndex = value; }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        shootAudio = gameObject.AddComponent<AudioSource>();
        shootAudio.clip = shootSoundClip;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchBulletType();
        }

        if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.DownArrow) && Time.time >= nextFireTime && shotsFired < bulletSettings[_currentBulletIndex].totalShots)
        {
            // 先播放動畫和音效，再啟動協程發射子彈
            PlayShootAnimation();
            PlayShootSound();
            StartCoroutine(ShootBullets());
        }
    }

    IEnumerator ShootBullets()
    {
        // 檢查子彈數量是否足夠
        if (shotsFired + bulletSettings[_currentBulletIndex].bulletsPerShot > bulletSettings[_currentBulletIndex].totalShots)
        {
            Debug.Log("Not enough bullets!");
            yield break; // 中斷協程
        }

        for (int i = 0; i < bulletSettings[_currentBulletIndex].bulletsPerShot; i++)
        {
            GameObject bullet = Instantiate(bulletSettings[_currentBulletIndex].bulletPrefab, firePoint2.position, firePoint2.rotation);
            yield return new WaitForSeconds(1f / bulletSettings[_currentBulletIndex].fireRate);
        }

        shotsFired += bulletSettings[_currentBulletIndex].bulletsPerShot;

        if (shotsFired >= bulletSettings[_currentBulletIndex].totalShots)
        {
            SwitchBulletType();
        }

        nextFireTime = Time.time + 1f / bulletSettings[_currentBulletIndex].fireRate;
    }

    void PlayShootAnimation()
    {
        animator.SetTrigger("ShootDown");
    }

    void PlayShootSound()
    {
        if (shootAudio != null && shootSoundClip != null)
        {
            shootAudio.Play();
        }
    }

    void SwitchBulletType()
    {
        _currentBulletIndex = (_currentBulletIndex + 1) % bulletSettings.Length;
        shotsFired = 0;
    }
}
