using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    // Walking audio source
    public AudioSource audioSource;
    // Can move?
    public bool CanMove = true;
    // Is moving?
    public bool isMoving = false;
    // Movement speed
    public float movementSpeed;

    // Current orientation reference via game object
    public Transform orientation;

    // Jumping values
    public float jumpForce, jumpCooldown, airMultiplier;

    // Key to jump
    public KeyCode jumpKey = KeyCode.Space;

    // Inventory button
    public KeyCode inventoryKey = KeyCode.I;

    // Inventory canvas object
    public GameObject inventory;

    // Container canvas object
    public GameObject container;

    // Current opened storage
    private GameObject currentStorage;
    // Can jump ?
    bool canJump;

    // Active input values
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    // Player rigidbody for collisions
    Rigidbody rb;

    // Friction to reduce or prevent sliding or slippery gameplay
    public float groundDrag;

    // Manually set height of player
    public float playerHeight;

    // Layer mask for raycasting to check if grounded
    public LayerMask isGround;

    // Bool for grounded
    bool grounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canJump = true;
    }
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // Debug.Log(horizontalInput);
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;
            Jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }
    }
    private void MovePlayer()
    {
        
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (grounded) {
            rb.AddForce(moveDirection.normalized * movementSpeed * 1f, ForceMode.Force);
            if (verticalInput != 0 || horizontalInput != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
           
                }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * 1f * airMultiplier, ForceMode.Force);
            isMoving = false;
        }
    }
    private void Update()
    {
        if (!audioSource.isPlaying && isMoving)
        {
            audioSource.Play();
        }
        else if(audioSource.isPlaying && !isMoving)
        {
            audioSource.Stop();
        }
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
        }
        if (CanMove)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);
            // Debug.Log(grounded);
            PlayerInput();
            SpeedControl();
            if (grounded)
            {
                rb.drag = groundDrag;
            }
            else
            {
                rb.drag = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);   
    }
    private void resetJump()
    {
        canJump = true;
    }
    public void ToggleInventory()
    {
        CanMove = !CanMove;
        if(currentStorage != null)
        {

            currentStorage.GetComponent<StorageBox>().CloseStorage();
            currentStorage = null;
            GetComponent<Interact>().isOpen = false;
        }
        container.SetActive(false);
        inventory.SetActive(!CanMove);

    }
    public void OpenContainer(GameObject gameObject)
    {
        /* When you close with E nothing happens */
        CanMove = !CanMove;
        Debug.Log("open container");
        currentStorage = gameObject;
        inventory.SetActive(!CanMove);
        container.SetActive(true);
    /* Get all items stored in container */
    /* Save all items when its closed */
    }

    public void LoadData(GameData data)
    {
        transform.position = data.playerPos;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPos = this.transform.position;
    }
}
