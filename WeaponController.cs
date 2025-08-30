using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject muzzleFlashPrefab;
    public Transform muzzleFlashPoint;
    public GameObject hitParticles;
    public RotateWithCamera rotateWithCamera;
    public GameObject magazine;
    public GameObject magazineTarget;
    public Item gunItem;

    #region
    [Header("UI Components")]
    public GameObject inventoryGrid;
    private GameObject userInterface;
    private GameObject ammoCounter;
    #endregion

    #region Reloading Sounds
    [Header("Reload Sounds")]
    public AudioSource audioSource;
    public AudioClip magIn;
    public AudioClip magOut;
    public AudioClip boltPull;
    #endregion

    #region Weapon Offsets
    [Header("Weapon Offset")]
    public Vector3 defaultWeaponOffset;
    public Vector3 weaponOffsetWhenAiming;
    #endregion

    
    public Vector3 aimingPositionalValues;
    public Vector3 aimingRotationalValues;

    #region Settings for IK Rig
    [Header("Settings for IK Rig")]
    public Transform leftHandGripPoint;
    public Transform rightHandGripPoint;
    public Transform leftGripPointMaster;
    public Transform rightGripPointMaster;
    public Vector3 leftHandPositionOffset;
    public Vector3 leftHandRotationOffset;
    public Vector3 rightHandPositionOffset;
    public Vector3 rightHandRotationOffset;
    #endregion

    #region Gun Variables and Properties 
    [Header("Gun Variables and Properties")]
    public int magazineId;
    public float transitionSpeed = 10f;
    public float recoilLevel = 0.00001f;
    public float damage = 75f;
    private const int maxAmmo = 30;
    private int curAmmo;
    #endregion

    private bool initialized = false;

    private Quaternion aimingRot;
    private Quaternion startRot;
    private Vector3 startPos;
    private Vector3 startRotation;
    private GameObject crosshair;

    private bool isButtonDown;

    private float lerpedVal;
    private float recoilDuration = 0.5f;

    private float timeElapsed = 0;
    private Vector3 singleShotPos;
    private bool canShoot;

    private RaycastHit hit;
    public GameObject MainCamera;
    public LayerMask layermask;
    
    public void InitializeWeapon()
    {
        inventoryGrid = GameObject.Find("ItemManager").GetComponent<ItemManager>().Inventory;
        curAmmo = gunItem.curAmmo;
        userInterface = GameObject.Find("UserInterface");
        canShoot = true;
        crosshair = GameObject.Find("CrossHair");
        /*MainCamera = Camera.main.gameObject;*/
        isButtonDown = false;
        startPos = transform.localPosition;
        startRotation = transform.localRotation.eulerAngles;
        aimingRot = Quaternion.Euler(aimingRotationalValues);
        startRot = Quaternion.Euler(startRotation);

        // This is horrible practice dont do this. 
        rotateWithCamera = transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<RotateWithCamera>();
        muzzleFlashPoint = transform.Find("MuzzleFlash").transform;
        rotateWithCamera.weaponOffset = defaultWeaponOffset;
        ammoCounter = GameObject.Find("AmmoCounter");
        ammoCounter.SetActive(true);
        initialized = true;
    }
    private void Update()
    {
        if (initialized)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isButtonDown = true;
                /*GetComponent<Animator>().SetBool("Aiming",true);*/
                crosshair.SetActive(false);
                /*transform.position = aimingPositionalValues;*/
                /*transform.SetPositionAndRotation(aimingPositionalValues, aimingRot);*/
                transform.localRotation = aimingRot;
                rotateWithCamera.weaponOffset = weaponOffsetWhenAiming;
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                /*GetComponent<Animator>().SetBool("Aiming", false);*/


                crosshair.SetActive(true);
                transform.localRotation = startRot;
                rotateWithCamera.weaponOffset = defaultWeaponOffset;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
            {
                if (curAmmo == 0)
                {
                    canShoot = false;
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("Shoot");
                    curAmmo--;
                    gunItem.curAmmo = curAmmo;
                    ammoCounter.GetComponent<AmmoCounter>().SetAmmo(curAmmo, maxAmmo);

                }

                /*if(timeElapsed < recoilDuration)
                {
                    float t = timeElapsed / recoilDuration;
                    transform.localPosition = Vector3.Lerp(transform.position, )
                }
                Shoot();*/
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                FindMag();
            }
            /* if (isButtonDown)
             {
                 float t = Time.deltaTime * transitionSpeed;
                 bool reachedPos = Vector3.Distance(transform.position, aimingPositionalValues) < 1;
                 bool reachedRot = Quaternion.Angle(transform.rotation, aimingRot) < 1;
                 if (!reachedPos)
                 {
                     transform.position = Vector3.Lerp(transform.localPosition, aimingPositionalValues, t);
                 }
                 if (!reachedRot)
                 {
                     transform.rotation = Quaternion.Slerp(transform.localRotation, aimingRot, t);
                 }
             }*/
            Debug.DrawRay(MainCamera.transform.position, MainCamera.transform.forward * 300f, Color.green);
        }
    }
    private void FindMag()
    {
        foreach(Transform slot in inventoryGrid.transform.Find("Grid").transform)
        {
            if(slot.childCount > 0)
            {
                if (slot.GetChild(0).GetComponent<DraggableItem>() && slot.GetChild(0).GetComponent<DraggableItem>().item.itemId == magazineId)
                {
                    Destroy(slot.GetChild(0).gameObject);
                    GetComponent<Animator>().SetBool("Reloading", true);
                }
            }
        }
    }
    public void StartReload()
    {
        leftHandGripPoint = magazineTarget.transform;
        
        canShoot = false;
        audioSource.PlayOneShot(magOut);
    }
    public void UpdateCounter()
    {
        audioSource.PlayOneShot(magIn);
        curAmmo = maxAmmo;
        gunItem.curAmmo = curAmmo;
        ammoCounter.GetComponent<AmmoCounter>().SetAmmo(curAmmo, maxAmmo);
    }
    public void BoltPull()
    {
        audioSource.PlayOneShot(boltPull);
    }
    public void FinishReload()
    {
        GetComponent<Animator>().SetBool("Reloading", false);
        canShoot = true;
        leftHandGripPoint = leftGripPointMaster;
    }
    public void Shoot()
    {
        GameObject muzzleflash = Instantiate(muzzleFlashPrefab, muzzleFlashPoint);
        if(Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit, 300f, layermask))
        {
            /*GameObject hitparticles = Instantiate(hitParticles);
            hitparticles.transform.parent = null;
            hitparticles.transform.position = hit.point;*/
            if (hit.transform.gameObject.GetComponent<HealthController>())
            {
                GameObject particles = Instantiate(hit.transform.GetComponent<HealthController>().particles);
                particles.transform.position = hit.point;
                particles.transform.LookAt(transform.position);
                hit.transform.gameObject.GetComponent<HealthController>().TakeDamage(Random.Range(damage - 5, damage + 5), 0);
            }
        }
    }
    private void SetAndPlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void DestroyLight()
    {
        /*Destroy(transform.Find("Light").gameObject);*/
    }
    /*private void Shoot()
    {
        
    }*/
}
