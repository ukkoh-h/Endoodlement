using System.Collections;
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

    [Header("Projectile Stettings")]
    [SerializeField] private GameObject projectile1Prefab;
    [SerializeField] private GameObject projectile2Prefab;
    [SerializeField] private GameObject projectile3Prefab;
    [SerializeField] private GameObject projectile4Prefab;

    /*[Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bulletsound;*/

    [Header("Gun Settings")]
    [SerializeField] private GunType gunType;
    [SerializeField] private int magazineSize = 1;
    [SerializeField] private int ammoCount = 30;
    [SerializeField] private float delayBetweenBullets = 0.5f;
    [SerializeField] private float bulletSpeed = 5f;

    [Header("Reload Settings")]
    [SerializeField] private bool autoReload = true;
    [SerializeField] private float reloadTime = 1f;

    [Header("Shooting Raycast Settings")]
    [SerializeField] private float fireDistance = 100f;
    [SerializeField] private float fireSpread = 0.1f;
    [SerializeField] private LayerMask hitLayers;

    private InputAction shootAction;
    private InputAction reloadAction;

    private int currentBulletsInMagazine;
    private bool isTryingToShoot;
    private bool canShoot = true;
    private bool isReloading;

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
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Trying to shoot");
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

    private void InitialiseInputActions()
    {
        InputActionMap gunActionMap = actionMap.FindActionMap("Gun");
        shootAction = gunActionMap.FindAction("Shoot");
        reloadAction = gunActionMap.FindAction("Reload");

        shootAction.performed += OnShootPerformed;
        shootAction.canceled += OnShootCanceled;
        reloadAction.performed += OnReloadPerformed;

        shootAction.Enable();
        reloadAction.Enable();
    }

    private void UnsubscribeFromInputActions()
    {
        shootAction.performed -= OnShootPerformed;
        shootAction.canceled -= OnShootCanceled;
        reloadAction.performed -= OnReloadPerformed;
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

        Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY , 100f);

        return spreadRotation * forward;
    }

    private void PerformRaycastShot()
    {
        Vector3 direction = GetRandomShotDirection();

        RaycastHit hit;
        bool hitSomething = Physics.Raycast(muzzleFlashTransform.position, direction, out hit, fireDistance,  hitLayers);

        Color lineColor = hitSomething ? Color.red : Color.blue;
        Debug.DrawRay(muzzleFlashTransform.position, direction * fireDistance, lineColor, 1f);

        GameObject bullet = Instantiate(projectile1Prefab, muzzleFlashTransform.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().Setup(direction);

        if (hitSomething)
        {
            //Hit logick
        }
    }

    private IEnumerator ReloadSequence()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentBulletsInMagazine = magazineSize;
        isReloading = false;
    }
    private void AttemptReload()
    {
        if (isReloading || currentBulletsInMagazine >= magazineSize)
        {
            return;
        }
        StartCoroutine(ReloadSequence());
    }

    private void TryAutoReload()
    {
        if (currentBulletsInMagazine <= 0 && autoReload && !isReloading)
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
}
