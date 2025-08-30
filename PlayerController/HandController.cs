using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // Get active item if any
    // call its use function
    // if no use function, default to a punch function 
    public GameObject particlePrefab;
    private AudioController audioController;
    public KeyCode attackButton = KeyCode.Mouse0; // Button to trigger
    public GameObject Arm;
    public GameObject itemSlot;
    public bool canAttack = true;
    [SerializeField] private Camera camera;
    private PlayerMovement playerMovement;
    public Animator animator;
    private RuntimeAnimatorController defaultAnimator;
    public float attackRange = 100.0f;
    // public animation
    RaycastHit hit;

    public float handDmg = 10f;
    public int itemDamageAccess = 0;
    private void Start()
    {
        audioController = transform.Find("Audio Source").GetComponent<AudioController>();
        playerMovement = GetComponent<PlayerMovement>();
        defaultAnimator = animator.runtimeAnimatorController;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(attackButton) && canAttack && playerMovement.CanMove)
        {
            canAttack = false;
            animator.SetTrigger("Use");
            audioController.playSound("punch");
        }
        Debug.DrawRay(camera.transform.position, camera.transform.forward * attackRange, Color.red);
    }
    // Method called from animation event
    public void Attack()
    {
        // WAIT FOR ANIMATION TO FINISH ADD ABILITY TO HOLD BUTTON  
        // Get melee tools damage and trigger the animation 
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, attackRange))
        {
            if (hit.transform.gameObject.GetComponent<HealthController>())
            {
                GameObject particles = Instantiate(hit.transform.GetComponent<HealthController>().particles);
                particles.transform.position = hit.point;
                particles.transform.LookAt(transform.position);
                hit.transform.gameObject.GetComponent<HealthController>().TakeDamage(Random.Range(handDmg - 5, handDmg + 5), itemDamageAccess);
            }
            /*if (hit.collider.CompareTag("Enemy"))
            {
                *//*hit.transform.GetComponent<ZombieController>().TakeDamage(Random.Range(handDmg - 5, handDmg + 5));*//*
            }*/
        }
    }
    public void changeItems(RuntimeAnimatorController itemAnimator)
    {
        animator.runtimeAnimatorController = itemAnimator;
        /*Arm.SetActive(true);*/
        // Get items function 
    }
    public void emptyHand()
    {
        /*Arm.SetActive(false);*/
        animator.runtimeAnimatorController = defaultAnimator;
    }
}
