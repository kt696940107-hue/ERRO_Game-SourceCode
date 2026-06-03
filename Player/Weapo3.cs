using System.Collections;
using UnityEngine;

public class Weapo3 : MonoBehaviour
{
    public Transform firePoint3; // 第二個發射點
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

        if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.UpArrow) && Time.time >= nextFireTime && shotsFired < bulletSettings[_currentBulletIndex].totalShots)
        {
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
            GameObject bullet = Instantiate(bulletSettings[_currentBulletIndex].bulletPrefab, firePoint3.position, firePoint3.rotation);
            yield return new WaitForSeconds(1f / bulletSettings[_currentBulletIndex].fireRate);
        }

        shotsFired += bulletSettings[_currentBulletIndex].bulletsPerShot;

        if (shotsFired >= bulletSettings[_currentBulletIndex].totalShots)
        {
            SwitchBulletType();
        }

        nextFireTime = Time.time + 1f / bulletSettings[_currentBulletIndex].fireRate;
        PlayShootAnimation();
        PlayShootSound();
    }

    void PlayShootAnimation()
    {
        animator.SetTrigger("ShootUp");
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
