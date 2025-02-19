using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weaponRoot;
    public Transform weaponMuzzle;
    public ProjectileBase projectilePrefab;
    public GameObject muzzleFlashPrefab;
    public float delayBetweenShots = 0.5f;

    private float _lastShotTime = Mathf.NegativeInfinity;
    private AudioSource _audioSource;

    public Vector3 muzzleWorldVelocity {  get; private set; }
    public bool isWeaponActive { get; private set; }
    public GameObject owner { get; set; }
    public GameObject sourcePrefab {  get; set; }

    private void Awake()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
    }

    public void ShowWeapon(bool show)
    {
        weaponRoot.SetActive(show);
        isWeaponActive = show;
    }

    public bool HandleShootInputs(bool inputHeld)
    {
        if (inputHeld)
        {
            return TryShoot();
        }
        return false;
    }

    private bool TryShoot()
    {
        if(_lastShotTime + delayBetweenShots < Time.time)
        {
            HandleShoot();
            //print("shot");
            return true;
        }
        return false;
    }

    private void HandleShoot()
    {
        if(projectilePrefab != null)
        {
            Vector3 shotDirection = weaponMuzzle.forward;
            ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponMuzzle.position, weaponMuzzle.rotation);

            newProjectile.Shoot(this);
        }

        // 开火特效
        if(muzzleFlashPrefab != null)
        {
            GameObject muzzleFlashInstance = Instantiate(muzzleFlashPrefab, weaponMuzzle.position, weaponMuzzle.rotation, weaponMuzzle.transform);

            Destroy(muzzleFlashInstance, 2);
        }

        // 开火音效
        if(_audioSource != null)
        {
            _audioSource.Play();
        }
        _lastShotTime = Time.time;
    }
}
