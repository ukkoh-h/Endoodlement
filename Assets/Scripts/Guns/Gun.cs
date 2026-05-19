using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public enum GunType
    {
        Normal,
        SpellBook,
        Slingshot,
        Shredder,
        GobLauncher
    }

    [Header("Action Map")]
    [SerializeField] private InputActionAsset actionMap;

    [Header("Muzzle Flash Settings")]
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private Transform muzzleFlashTransform;
    [SerializeField] private Transform bulletSpawnTransform;

    [Header("Projectile Stettings")]
    [SerializeField] private GameObject DefaultProjectilePrefab;
    [SerializeField] private GameObject projectile1Prefab;
    [SerializeField] private GameObject projectile2Prefab;
    [SerializeField] private GameObject projectile3Prefab;
    [SerializeField] private GameObject projectile4Prefab;

    /*[Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bulletsound;*/

    [Header("Gun Settings")]
    [SerializeField] private GunType gunType;
    [SerializeField] private GameObject gunBodyPrefab;
    [SerializeField] private int magazineSize = 1;
    [SerializeField] private int ammoCount = 30;
    [SerializeField] private float delayBetweenBullets = 0.5f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletAngle = 20f;
    [SerializeField] private float reloadTime = 5f;

    [Header("SpellBook Settings")]
    //[SerializeField] private GunType gunType;
    [SerializeField] private GameObject spellBookBodyPrefab;
    [SerializeField] private int spellBookMagazineSize = 1;
    [SerializeField] private int spellBookAmmoCount = 30;
    [SerializeField] private float spellBookDelayBetweenBullets = 0.5f;
    [SerializeField] private float spellBookBulletSpeed = 10f;
    [SerializeField] private float spellBookBulletAngle = 20f;
    [SerializeField] private float spellBookReloadTime = 1f;

    [Header("SlingShot Settings")]
    //[SerializeField] private GunType gunType;
    [SerializeField] private GameObject slingShotBodyPrefab;
    [SerializeField] private int slingShotMagazineSize = 1;
    [SerializeField] private int slingShotAmmoCount = 30;
    [SerializeField] private float slingShotkDelayBetweenBullets = 0.5f;
    [SerializeField] private float slingShotBulletSpeed = 20f;
    [SerializeField] private float slingShotBulletAngle = 10f;
    [SerializeField] private float slingShotReloadTime = 0.5f;

    [Header("Reload Settings")]
    [SerializeField] private bool autoReload = true;

    [Header("Shooting Raycast Settings")]
    [SerializeField] private float fireDistance = 100f;
    [SerializeField] private float fireSpread = 0.1f;
    [SerializeField] private LayerMask hitLayers;

    private List<int> gunList = new();

    private InputAction shootAction;
    private InputAction reloadAction;
    private InputAction switchAction1;
    private InputAction switchAction2;
    private InputAction switchAction3;
    private InputAction switchAction4;
    private InputAction switchScrollAction;

    private int currentBulletsInMagazine;
    private bool isTryingToShoot;
    private bool canShoot = true;
    private bool isReloading;
    private bool isSwitching;
    private bool reloadCanceled;
    private bool reloadStarted;
    private float timeReloaded;
    private float defaultRotationAngle;
    private float currentRotationAngle;
    private float beingReloadedAngle = 30.0f;

    private void Awake()
    {
        InitialiseInputActions();
        currentBulletsInMagazine = magazineSize;
    }

    private void Update()
    {
        if (isTryingToShoot && canShoot && !isReloading)
        {
            StartCoroutine(ShootSequence());
        }
        if (timeReloaded < 1 && isReloading)
        {
            timeReloaded += Time.deltaTime * 7 * (reloadTime/(reloadTime*reloadTime));
        }
        if (timeReloaded < 1 && isSwitching)
        {
            timeReloaded += Time.deltaTime * 7 * (reloadTime/(reloadTime*reloadTime));
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.y, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (reloadStarted ? beingReloadedAngle : 0), timeReloaded), transform.localEulerAngles.z);
    }
    public bool CanProcessInput()
    {
        return Cursor.lockState == CursorLockMode.Locked;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("Trying to shoot");
        isTryingToShoot = true;
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        isTryingToShoot = false;
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        AttemptReload();
    }
    private void OnSwitch1Performed(InputAction.CallbackContext context)
    {
        if (isReloading) reloadCanceled = true;
        gunType = GunType.SpellBook;
        StartCoroutine(GunSwitchSequence());
        reloadCanceled = false;
    }
        private void OnSwitch2Performed(InputAction.CallbackContext context)
    {
        if (isReloading) reloadCanceled = true;
        gunType = GunType.Slingshot;
        StartCoroutine(GunSwitchSequence());
        reloadCanceled = false;
    }

    private void InitialiseInputActions()
    {
        InputActionMap gunActionMap = actionMap.FindActionMap("Gun");
        shootAction = gunActionMap.FindAction("Shoot");
        reloadAction = gunActionMap.FindAction("Reload");
        switchAction1 = gunActionMap.FindAction("Switch1");
        switchAction2 = gunActionMap.FindAction("Switch2");
        switchAction3 = gunActionMap.FindAction("Switch3");
        switchAction4 = gunActionMap.FindAction("Switch4");


        shootAction.performed += OnShootPerformed;
        shootAction.canceled += OnShootCanceled;
        reloadAction.performed += OnReloadPerformed;
        switchAction1.performed += OnSwitch1Performed;
        switchAction2.performed += OnSwitch2Performed;

        shootAction.Enable();
        reloadAction.Enable();
    }

    private void UnsubscribeFromInputActions()
    {
        shootAction.performed -= OnShootPerformed;
        shootAction.canceled -= OnShootCanceled;
        reloadAction.performed -= OnReloadPerformed;
        switchAction1.performed -= OnSwitch1Performed;
        switchAction2.performed -= OnSwitch2Performed;
    }

    private void OnDestroy()
    {
        UnsubscribeFromInputActions();
    }

    private bool CheckIfGunCanShoot()
    {
        if(currentBulletsInMagazine <= 0) return false;
        if(isReloading) return false;
        return true;
    }

    private Vector3 GetRandomShotDirection()
    {
        Vector3 forward = muzzleFlashTransform.forward * bulletSpeed;
        
        float spreadX = Random.Range(-fireSpread, fireSpread);
        float spreadY = Random.Range(-fireSpread, fireSpread);

        Quaternion spreadRotation = Quaternion.Euler(spreadX - bulletAngle, spreadY, 0f);

        return spreadRotation * forward;
    }

    private void PerformRaycastShot()
    {
        Vector3 direction = GetRandomShotDirection();

        RaycastHit hit;
        bool hitSomething = Physics.Raycast(bulletSpawnTransform.position, direction, out hit, fireDistance,  hitLayers);

        Color lineColor = hitSomething ? Color.red : Color.blue;
        //Debug.DrawRay(bulletSpawnTransform.position, direction * fireDistance, lineColor, 1f);

        GameObject bullet = Instantiate(DefaultProjectilePrefab, bulletSpawnTransform.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().Setup(direction);

        if (hitSomething)
        {
            //Hit logick
        }
    }

    private IEnumerator ReloadSequence()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime/4);
        timeReloaded = 0;
        reloadStarted = true;
        currentRotationAngle = transform.localEulerAngles.y;
        yield return new WaitForSeconds(reloadTime/2);
        timeReloaded = 0;
        reloadStarted = false;
        currentRotationAngle = transform.localEulerAngles.y;
        yield return new WaitForSeconds(reloadTime/4);
        if (ammoCount >= magazineSize - currentBulletsInMagazine && !reloadCanceled)
        {
            ammoCount -= magazineSize - currentBulletsInMagazine;
            currentBulletsInMagazine = magazineSize;
            AmmoManager();
        } else if (ammoCount < magazineSize - currentBulletsInMagazine && ammoCount > 0 && !reloadCanceled)
        {
            currentBulletsInMagazine += ammoCount;
            ammoCount = 0;
            AmmoManager();
        }
        isReloading = false;
    }
    private void AmmoManager()
    {
        switch (gunType)
        {
            case GunType.Normal:

                break;
            case GunType.SpellBook:
                spellBookAmmoCount = ammoCount;
                break;
            case GunType.Slingshot:
                slingShotAmmoCount = ammoCount;
                break;
            /*case GunType.Shredder:
                shredderAmmoCount = ammoCount;
                break;*/
        }
    }
    private IEnumerator GunSwitchSequence()
    {
        isSwitching = true;
        yield return new WaitForSeconds(reloadTime/4);
        timeReloaded = 0;
        reloadStarted = true;
        currentRotationAngle = transform.localEulerAngles.y;
        yield return new WaitForSeconds(reloadTime/4);
        switch (gunType)
        {
            case GunType.SpellBook:
                SpellBookSwitchRoutine();
                break;
            case GunType.Slingshot:
                SlingShotSwitchRoutine();
                break;
        }
        yield return new WaitForSeconds(reloadTime/4);
        timeReloaded = 0;
        reloadStarted = false;
        currentRotationAngle = transform.localEulerAngles.y;
        yield return new WaitForSeconds(reloadTime/4);
        isSwitching = false;
    }
    private void SpellBookSwitchRoutine()
    {
        DefaultProjectilePrefab = projectile1Prefab;
        //gunBodyPrefab = spellBookBodyPrefab;
        magazineSize = spellBookMagazineSize;
        ammoCount = spellBookAmmoCount;
        delayBetweenBullets = spellBookDelayBetweenBullets;
        bulletSpeed = spellBookBulletSpeed;
        bulletAngle = spellBookBulletAngle;
        reloadTime = spellBookReloadTime;
    }
        private void SlingShotSwitchRoutine()
    {
        DefaultProjectilePrefab = projectile2Prefab;
        //gunBodyPrefab = slingShotBodyPrefab;
        magazineSize = slingShotMagazineSize;
        ammoCount = slingShotAmmoCount;
        delayBetweenBullets = slingShotkDelayBetweenBullets;
        bulletSpeed = slingShotBulletSpeed;
        bulletAngle = slingShotBulletAngle;
        reloadTime = slingShotReloadTime;
    }
    private void AttemptReload()
    {
        if (isReloading || isSwitching || currentBulletsInMagazine >= magazineSize)
        {
            return;
        }
        StartCoroutine(ReloadSequence());
    }

    private void TryAutoReload()
    {
        if (currentBulletsInMagazine <= 0 && autoReload && !isReloading && !isSwitching)
        {
            StartCoroutine(ReloadSequence());
        }
    }

    private void PlayMuzzleFlash()
    {
        GameObject flashInstance = Instantiate(muzzleFlashPrefab, muzzleFlashTransform);
        Destroy(flashInstance, Mathf.Min(delayBetweenBullets, 0.03f));
    }

    private void ShootGun()
    {
        //audioSource.PlayOneShot(bulletsound);
        PlayMuzzleFlash();
        currentBulletsInMagazine--;

        TryAutoReload();

        PerformRaycastShot();
    }

    private IEnumerator NormalShootRoutine()
    {
        if (CheckIfGunCanShoot())
        {
            ShootGun();
            yield return new WaitForSeconds(delayBetweenBullets);
        }
    }

    private IEnumerator ShootSequence()
    {
        canShoot = false;
        switch (gunType)
        {
            case GunType.Normal:
                yield return NormalShootRoutine();
                break;
        }
        canShoot = true;
    }
    public int GetSwitchWeaponInput()
    {
            if (CanProcessInput())
            {
                var input = switchScrollAction.ReadValue<float>();

                if (input > 0f)
                    return -1;
                
                if (input < 0f)
                    return 1;
            }

            return 0;
    }

}