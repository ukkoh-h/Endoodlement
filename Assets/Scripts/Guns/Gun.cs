using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public enum GunType
    {
        //Normal,
        SpellBook,
        Slingshot,
        Shredder,
        GobLauncher
    }

    [Header("Action Map")]
    [SerializeField] private InputActionAsset actionMap;

    [Header("Muzzle Flash Settings")]
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private GameObject shredderMuzzleFlashPrefab;
    [SerializeField] private Transform muzzleFlashTransform;
    [SerializeField] private Transform bulletSpawnTransform;

    [Header("Projectile Stettings")]
    [SerializeField] private GameObject DefaultProjectilePrefab;
    //[SerializeField] private Bullet DefaultProjectile;
    [SerializeField] private GameObject projectile1Prefab;
    [SerializeField] private Bullet projectile1;
    [SerializeField] private GameObject projectile2Prefab;
    [SerializeField] private Bullet projectile2;
    [SerializeField] private GameObject projectile3Prefab;
    [SerializeField] private Bullet projectile3;
    [SerializeField] private GameObject projectile4Prefab;
    [SerializeField] private Bullet projectile4;

    /*[Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bulletsound;*/

    [Header("Gun Settings")]
    [SerializeField] private GunType gunType;
    [SerializeField] private GameObject gunBodyPrefab;
    [SerializeField] private int magazineSize = 1;
    [SerializeField] private int ammoCount = 30;
    [SerializeField] private int projectailCount = 1;
    [SerializeField] private float delayBetweenBullets = 0.5f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletAngle = 20f;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private float fireSpread = 0.1f;

    [Header("SpellBook Settings")]
    //[SerializeField] private GunType gunType;
    [SerializeField] private GameObject spellBookBodyPrefab;
    [SerializeField] private Animator spellBookAnimation;
    [SerializeField] private int spellBookDmg = 3;
    [SerializeField] private int spellBookMagazineSize = 1;
    [SerializeField] private int spellBookAmmoCount = 30;
    [SerializeField] private int spellBookProjectailCount = 1;
    [SerializeField] private float spellBookDelayBetweenBullets = 0.5f;
    [SerializeField] private float spellBookBulletSpeed = 10f;
    [SerializeField] private float spellBookBulletAngle = 20f;
    [SerializeField] private float spellBookReloadTime = 1f;
    [SerializeField] private float spellBookFireSpread = 0.1f;

    [Header("SlingShot Settings")]
    [SerializeField] private bool slingShotActive;
    [SerializeField] private GameObject slingShotBodyPrefab;
    [SerializeField] private GameObject slingShotUpgradedBodyPrefab;
    [SerializeField] private int slingShotDmg= 4;
    [SerializeField] private int slingShotMagazineSize = 1;
    [SerializeField] private int slingShotAmmoCount = 30;
    [SerializeField] private int slingShotProjectailCount = 1;
    [SerializeField] private float slingShotDelayBetweenBullets = 0.5f;
    [SerializeField] private float slingShotBulletSpeed = 20f;
    [SerializeField] private float slingShotBulletAngle = 10f;
    [SerializeField] private float slingShotReloadTime = 0.5f;
    [SerializeField] private float slingShotFireSpread = 0.1f;

    [Header("Shredder Settings")]
    [SerializeField] private bool shredderActive;
    [SerializeField] private GameObject shredderBodyPrefab;
    [SerializeField] private GameObject shredderBodyUpGradedPrefab;
    [SerializeField] private int shredderDmg = 2;
    [SerializeField] private int shredderMagazineSize = 30;
    [SerializeField] private int shredderAmmoCount = 120;
    [SerializeField] private int shredderProjectailCount = 1;
    [SerializeField] private float shredderDelayBetweenBullets = 0.5f;
    [SerializeField] private float shredderBulletSpeed = 20f;
    [SerializeField] private float shredderBulletAngle = 10f;
    [SerializeField] private float shredderReloadTime = 2f;
    [SerializeField] private float shredderFireSpread = 0.1f;

    [Header("GoblinLauncher Settings")]
    [SerializeField] private bool gobLauncherActive;
    [SerializeField] private GameObject gobLauncherBodyPrefab;
    [SerializeField] private int gobLauncherDmg = 10;
    [SerializeField] private int gobLauncherMagazineSize = 1;
    [SerializeField] private int gobLauncherAmmoCount = 3;
    [SerializeField] private int gobLauncherProjectailCount = 1;
    [SerializeField] private float gobLauncherDelayBetweenBullets = 0.5f;
    [SerializeField] private float gobLauncherBulletSpeed = 20f;
    [SerializeField] private float gobLauncherBulletAngle = 10f;
    [SerializeField] private float gobLauncherReloadTime = 0.5f;
    [SerializeField] private float gobLauncherFireSpread = 0.1f;

    [Header("Reload Settings")]
    [SerializeField] private bool autoReload = true;

    /*[Header("Shooting Raycast Settings")]
    [SerializeField] private float fireDistance = 100f;
    [SerializeField] private float fireSpread = 0.1f;
    [SerializeField] private LayerMask hitLayers;*/

    private InputAction shootAction;
    private InputAction reloadAction;
    private InputAction switchAction1;
    private InputAction switchAction2;
    private InputAction switchAction3;
    private InputAction switchAction4;
    private InputAction switchScrollAction;

    private int currentBulletsInMagazine;
    private int spellBookBulletsInMagazine;
    private int slingShotBulletsInMagazine;
    private int shredderBulletsInMagazine;
    private int gobLauncherBulletsInMagazine;
    private int currentWeapon;
    private int numberOfWeapons = 1;
    private bool isTryingToShoot;
    private bool canShoot = true;
    private bool isReloading;
    private bool isSwitching;
    private bool reloadCanceled;
    private bool switchCanceled;
    private bool reloadStarted;
    private bool weaponLowered;
    private bool switchListening; 
    private float timeReloaded;
    private float defaultRotationAngle;
    private float currentRotationAngle;
    private float beingReloadedAngle = 55.0f;

    private void Awake()
    {
        InitialiseInputActions();
        currentBulletsInMagazine = magazineSize;
        spellBookBulletsInMagazine = spellBookMagazineSize;
        slingShotBulletsInMagazine = slingShotMagazineSize;
        shredderBulletsInMagazine = shredderMagazineSize;
        gobLauncherBulletsInMagazine = gobLauncherMagazineSize;
        projectile1.SetDmg(spellBookDmg);
        projectile2.SetDmg(slingShotDmg);
        projectile3.SetDmg(shredderDmg);
        //projectile4.SetDmg(gobLauncherDmg);
        if (slingShotActive) numberOfWeapons += 1;
        if (shredderActive) numberOfWeapons += 1;
        if (gobLauncherActive) numberOfWeapons += 1;
    }

    private void Update()
    {
        //UnityEngine.Debug.Log(currentBulletsInMagazine);
        if (isTryingToShoot && canShoot && !isReloading && !isSwitching)
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
        if (isSwitching && weaponLowered && !switchListening)
        {
            StartCoroutine(SwitchListenerSequence());
        }
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
        if (gunType == GunType.SpellBook) return;
        if (!isSwitching) BulletsInMagManager();
        gunType = GunType.SpellBook;
        currentWeapon = 0;
        if (isSwitching) switchCanceled = true;
        else StartCoroutine(GunSwitchSequence());
        reloadCanceled = false;
    }
    private void OnSwitch2Performed(InputAction.CallbackContext context)
    {
        if (!slingShotActive) return;
        if (isReloading) reloadCanceled = true;
        if (gunType == GunType.Slingshot) return;
        if (!isSwitching) BulletsInMagManager();
        gunType = GunType.Slingshot;
        currentWeapon = 1;
        if (isSwitching) switchCanceled = true;
        else StartCoroutine(GunSwitchSequence());
        reloadCanceled = false;
    }
    private void OnSwitch3Performed(InputAction.CallbackContext context)
    {
        if (!shredderActive) return;
        if (isReloading) reloadCanceled = true;
        if (gunType == GunType.Shredder) return;
        if (!isSwitching) BulletsInMagManager();
        gunType = GunType.Shredder;
        currentWeapon = 1;
        if (isSwitching) switchCanceled = true;
        else StartCoroutine(GunSwitchSequence());
        reloadCanceled = false;
    }
    private void OnSwitch4Performed(InputAction.CallbackContext context)
    {
        if (!gobLauncherActive) return;
        if (isReloading) reloadCanceled = true;
        if (gunType == GunType.GobLauncher) return;
        if (!isSwitching) BulletsInMagManager();
        gunType = GunType.GobLauncher;
        currentWeapon = 1;
        if (isSwitching) switchCanceled = true;
        else StartCoroutine(GunSwitchSequence());
        reloadCanceled = false;
    }

    private void OnSwitchScrollPerformed(InputAction.CallbackContext context)
    {
        if (!slingShotActive && !shredderActive && !gobLauncherActive) return;
        if (isReloading) reloadCanceled = true;
        if (!isSwitching) BulletsInMagManager();
        currentWeapon += GetSwitchWeaponInput();
        if(currentWeapon >= numberOfWeapons) currentWeapon = 0;
        if(currentWeapon < 0) currentWeapon = numberOfWeapons -1;

        switch (currentWeapon)
        {
            case 0:
                gunType = GunType.SpellBook;
                break;
            case 1:
                if (slingShotActive) gunType = GunType.Slingshot; 
                else if (shredderActive) gunType = GunType.Shredder;
                else if (gobLauncherActive) gunType = GunType.GobLauncher;
                break;
            case 2:
                if (shredderActive) gunType = GunType.Shredder;
                else if (gobLauncherActive) gunType = GunType.GobLauncher;
                break;
            case 3:
                gunType = GunType.GobLauncher;
                break;
        }
        //UnityEngine.Debug.Log(gunType);
        if (isSwitching) switchCanceled = true;
        else StartCoroutine(GunSwitchSequence());
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
        switchScrollAction = gunActionMap.FindAction("NextWeapon");


        shootAction.performed += OnShootPerformed;
        shootAction.canceled += OnShootCanceled;
        reloadAction.performed += OnReloadPerformed;
        switchAction1.performed += OnSwitch1Performed;
        switchAction2.performed += OnSwitch2Performed;
        switchAction3.performed += OnSwitch3Performed;
        switchAction4.performed += OnSwitch4Performed;
        switchScrollAction.performed += OnSwitchScrollPerformed;

        shootAction.Enable();
        reloadAction.Enable();
        switchAction1.Enable();
        switchAction2.Enable();
        switchAction3.Enable();
        switchAction4.Enable();
        switchScrollAction.Enable();
    }

    private void UnsubscribeFromInputActions()
    {
        shootAction.performed -= OnShootPerformed;
        shootAction.canceled -= OnShootCanceled;
        reloadAction.performed -= OnReloadPerformed;
        switchAction1.performed -= OnSwitch1Performed;
        switchAction2.performed -= OnSwitch2Performed;
        switchAction3.performed -= OnSwitch3Performed;
        switchAction4.performed -= OnSwitch4Performed;
        switchScrollAction.performed -= OnSwitchScrollPerformed;
    }

    private void OnDestroy()
    {
        UnsubscribeFromInputActions();
    }

    private bool CheckIfGunCanShoot()
    {
        if(currentBulletsInMagazine <= 0) 
        {
            TryAutoReload();
            return false;
        }
        if(isReloading || isSwitching) return false;
        return true;
    }

    private Vector3 GetRandomShotDirection()
    {
        Vector3 forward = bulletSpawnTransform.forward * bulletSpeed;
        
        float spreadX = Random.Range(-fireSpread, fireSpread);
        float spreadY = Random.Range(-fireSpread, fireSpread);

        Quaternion spreadRotation = Quaternion.Euler(spreadX - bulletAngle, spreadY, 0f);

        return spreadRotation * forward;
    }

    private void PerformRaycastShot()
    {
        Vector3 direction = GetRandomShotDirection();

        GameObject bullet = Instantiate(DefaultProjectilePrefab, bulletSpawnTransform.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().Setup(direction);
    }

    private IEnumerator ReloadSequence()
    {
        isReloading = true;


        switch (gunType)
        {
            
                
            case GunType.Slingshot:
                AudioManager.Instance.PlaySFX("SlingReload");
                break;
            case GunType.Shredder:
                AudioManager.Instance.PlaySFX("ShredderReload");
                break;

        }

        yield return new WaitForSeconds(reloadTime/4);
        timeReloaded = 0;
        reloadStarted = true;
        currentRotationAngle = transform.localEulerAngles.y;
        yield return new WaitForSeconds(reloadTime/2);
        timeReloaded = 0;
        reloadStarted = false;
        currentRotationAngle = transform.localEulerAngles.y;
        yield return new WaitForSeconds(reloadTime/4);
        if (ammoCount >= magazineSize - currentBulletsInMagazine && !reloadCanceled && gunType != GunType.SpellBook)
        {
            ammoCount -= magazineSize - currentBulletsInMagazine;
            currentBulletsInMagazine = magazineSize;
            AmmoManager();
        } 
        else if (ammoCount < magazineSize - currentBulletsInMagazine && ammoCount > 0 && !reloadCanceled && gunType != GunType.SpellBook)
        {
            currentBulletsInMagazine += ammoCount;
            ammoCount = 0;
            AmmoManager();
        } 
        else if (gunType == GunType.SpellBook)
        {
            currentBulletsInMagazine = magazineSize;
        }

        isReloading = false;
    }
    private void AmmoManager()
    {
        switch (gunType)
        {
            /*case GunType.Normal:

                break;*/
            case GunType.SpellBook:
                spellBookAmmoCount = ammoCount;
                break;
            case GunType.Slingshot:
                slingShotAmmoCount = ammoCount;
                break;
            case GunType.Shredder:
                shredderAmmoCount = ammoCount;
                break;
            case GunType.GobLauncher:
                shredderAmmoCount = ammoCount;
                break;
        }
    }
    private void BulletsInMagManager()
    {
        switch (gunType)
        {
            case GunType.SpellBook:
                spellBookBulletsInMagazine = currentBulletsInMagazine;
                break;
            case GunType.Slingshot:
                slingShotBulletsInMagazine = currentBulletsInMagazine;
                break;
            case GunType.Shredder:
                shredderBulletsInMagazine = currentBulletsInMagazine;
                break;
            case GunType.GobLauncher:
                gobLauncherBulletsInMagazine = currentBulletsInMagazine;
                break;
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
        weaponLowered = true;
    }
    private IEnumerator SwitchListenerSequence()
    {
        switchListening = true;
        yield return new WaitForSeconds(0.5f);
        if(switchCanceled)
        {
            switchCanceled = false;
            switchListening = false;
        } else
        {
            StartCoroutine(EndGunSwitchSequence());
        }
    }
    private IEnumerator EndGunSwitchSequence()
    {
        switch (gunType)
        {
            case GunType.SpellBook:
                SpellBookSwitchRoutine();
                break;
            case GunType.Slingshot:
                SlingShotSwitchRoutine();
                break;
            case GunType.Shredder:
                ShredderSwitchRoutine();
                break;
            case GunType.GobLauncher:
                GobLauncherSwitchRoutine();
                break;
        }
        yield return new WaitForSeconds(reloadTime/4);
        timeReloaded = 0;
        reloadStarted = false;
        currentRotationAngle = transform.localEulerAngles.y;
        yield return new WaitForSeconds(reloadTime/4);
        weaponLowered = false;
        switchListening = false;
        isSwitching = false;
    }
    private void SpellBookSwitchRoutine()
    {
        DefaultProjectilePrefab = projectile1Prefab;
        spellBookBodyPrefab.SetActive(true);
        //gunBodyPrefab.SetActive(false);
        slingShotBodyPrefab.SetActive(false);
        shredderBodyPrefab.SetActive(false);
        //gobLauncherBodyPrefab.SetActive(false);
        magazineSize = spellBookMagazineSize;
        ammoCount = spellBookAmmoCount;
        currentBulletsInMagazine = spellBookBulletsInMagazine;
        projectailCount = spellBookProjectailCount;
        delayBetweenBullets = spellBookDelayBetweenBullets;
        bulletSpeed = spellBookBulletSpeed;
        bulletAngle = spellBookBulletAngle;
        reloadTime = spellBookReloadTime;
        fireSpread = spellBookFireSpread;
    }
    private void SlingShotSwitchRoutine()
    {
        DefaultProjectilePrefab = projectile2Prefab;
        spellBookBodyPrefab.SetActive(false);
        //gunBodyPrefab.SetActive(false);
        slingShotBodyPrefab.SetActive(true);
        shredderBodyPrefab.SetActive(false);
        //gobLauncherBodyPrefab.SetActive(false);
        magazineSize = slingShotMagazineSize;
        ammoCount = slingShotAmmoCount;
        currentBulletsInMagazine = slingShotBulletsInMagazine;
        projectailCount = slingShotProjectailCount;
        delayBetweenBullets = slingShotDelayBetweenBullets;
        bulletSpeed = slingShotBulletSpeed;
        bulletAngle = slingShotBulletAngle;
        reloadTime = slingShotReloadTime;
        fireSpread = slingShotFireSpread;
    }
    private void ShredderSwitchRoutine()
    {
        DefaultProjectilePrefab = projectile3Prefab;
        spellBookBodyPrefab.SetActive(false);
        //gunBodyPrefab.SetActive(false);
        slingShotBodyPrefab.SetActive(false);
        shredderBodyPrefab.SetActive(true);
        //gobLauncherBodyPrefab.SetActive(false);
        magazineSize = shredderMagazineSize;
        ammoCount = shredderAmmoCount;
        currentBulletsInMagazine = shredderBulletsInMagazine;
        projectailCount = shredderProjectailCount;
        delayBetweenBullets = shredderDelayBetweenBullets;
        bulletSpeed = shredderBulletSpeed;
        bulletAngle = shredderBulletAngle;
        reloadTime = shredderReloadTime;
        fireSpread = shredderFireSpread;
    }
    private void GobLauncherSwitchRoutine()
    {
        DefaultProjectilePrefab = projectile3Prefab;
        spellBookBodyPrefab.SetActive(false);
        //gunBodyPrefab.SetActive(false);
        slingShotBodyPrefab.SetActive(false);
        /*shredderBodyPrefab.SetActive(false);
        gobLauncherBodyPrefab.SetActive(true);*/
        magazineSize = gobLauncherMagazineSize;
        ammoCount = gobLauncherAmmoCount;
        currentBulletsInMagazine = gobLauncherBulletsInMagazine;
        projectailCount = gobLauncherProjectailCount;
        delayBetweenBullets = gobLauncherDelayBetweenBullets;
        bulletSpeed = gobLauncherBulletSpeed;
        bulletAngle = gobLauncherBulletAngle;
        reloadTime = gobLauncherReloadTime;
        fireSpread = gobLauncherFireSpread;
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
        if (currentBulletsInMagazine <= 0 && autoReload && ammoCount > 0 && !isReloading && !isSwitching)
        {
            StartCoroutine(ReloadSequence());
        }
    }

    private void PlayMuzzleFlash()
    {
        //UnityEngine.Debug.Log("Shoteffect");
        if(gunType != GunType.SpellBook && gunType != GunType.Shredder)
        {
            GameObject flashInstance = Instantiate(muzzleFlashPrefab, muzzleFlashTransform.position, muzzleFlashTransform.rotation);
            Destroy(flashInstance, Mathf.Min(delayBetweenBullets, 0.5f));
        } else if (gunType == GunType.Shredder)
        {
            GameObject flashInstance = Instantiate(shredderMuzzleFlashPrefab, muzzleFlashTransform.position, muzzleFlashTransform.rotation);
            Destroy(flashInstance, Mathf.Min(delayBetweenBullets, 0.5f));
        } else if (gunType == GunType.SpellBook)
        {
            spellBookAnimation.Play("Arms_Throw");
        }
    }

    private void ShootGun()
    {

        switch (gunType)
        {
            case GunType.SpellBook:
                AudioManager.Instance.PlaySFX("PaperThrow");
                break;
            case GunType.Slingshot:
                AudioManager.Instance.PlaySFX("SlingShoot");
                break;
            case GunType.Shredder:
                AudioManager.Instance.PlaySFX("ShredderShoot");
                break;

        }
        
        if (gunType != GunType.SpellBook)
        {
            currentBulletsInMagazine--;
            TryAutoReload();
            PlayMuzzleFlash();
        }
        for(int i = 0; i < projectailCount; i++)
        {
            PerformRaycastShot();
        }
    }

    private IEnumerator NormalShootRoutine()
    {
        if (CheckIfGunCanShoot())
        {
            ShootGun();
            yield return new WaitForSeconds(delayBetweenBullets);
        }
    }
    private IEnumerator AbnormalShootRoutine()
    {
        if (CheckIfGunCanShoot())
        {
            PlayMuzzleFlash();
            yield return new WaitForSeconds(delayBetweenBullets/5*3);
            ShootGun();
            yield return new WaitForSeconds(delayBetweenBullets/5*2);
            //yield return new WaitForSeconds(delayBetweenBullets);
        }
    }

    private IEnumerator ShootSequence()
    {
        canShoot = false;
        switch (gunType)
        {
            case GunType.SpellBook:
                yield return AbnormalShootRoutine();
                break;
            case GunType.Slingshot:
                yield return NormalShootRoutine();
                break;
            case GunType.Shredder:
                yield return NormalShootRoutine();
                break;

        }
        //yield return NormalShootRoutine();
        canShoot = true;
    }
    public int GetSwitchWeaponInput()
    {
            /*if (CanProcessInput())
            {*/
                var input = switchScrollAction.ReadValue<float>();
                //UnityEngine.Debug.Log(input);

                if (input > 0f)
                    return -1;
                
                if (input < 0f)
                    return 1;
            //}

            return 0;
    }
    public void GetShredderAmmo(int bullets)
    {
        shredderAmmoCount += bullets;
        if (gunType == GunType.Shredder) ammoCount += bullets;
    }
    public void GetSlingShotAmmo(int bullets)
    {
        slingShotAmmoCount += bullets;
        if (gunType == GunType.Slingshot) ammoCount += bullets;
    }
    public void UpgradeSlingShot(int dmg, int projectiles, float reload)
    {
        
    }
    public void GetRandomAmmo()
    {
        float coinFlip = -1;
        if (shredderActive) coinFlip = Random.Range(-1, 1);
        if (coinFlip>=0)
        {
            GetSlingShotAmmo(5);
        } else
        {
            GetShredderAmmo(10);
        }
    }

}