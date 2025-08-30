using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    public float walkSpeed = 10;
    public float runSpeed;
    public float rotationSpeed = 1;
    private float rotValue;

    public bool isAlive;
    private bool isWalking;
    private bool isTurning;
    private Rigidbody rb;
    
    void Start()
    {
        isAlive = true;
        isWalking = false;
        isTurning = false;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WalkInDir());
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        { 
            moveMob();
            SpeedControl(walkSpeed);
        }
        if (isTurning)
        {
            rotateMob();
        }
    }
    IEnumerator WalkInDir()
    {
        while (isAlive)
        {
            isWalking = false;
            yield return new WaitForSeconds(Random.Range(1, 5));
            //rotValue = Random.Range(-2, 2);
            rotValue = rotationSpeed;
            isTurning = true;
            yield return new WaitForSeconds(1.5f);
            isTurning = false;
            isWalking = true;
            moveMob();
            yield return new WaitForSeconds(Random.Range(1, 3));
            //GetComponent<Rigidbody>().velocity = transform.forward * walkSpeed;
            //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * walkSpeed * Time.deltaTime, ForceMode.Force);
        }
    }
    private void moveMob()
    {
        Vector3 moveDirection = transform.forward * walkSpeed * Time.deltaTime;
        rb.AddForce(moveDirection.normalized, ForceMode.Force);
        
    }
    private void rotateMob()
    {
        transform.Rotate(new Vector3(transform.rotation.x, (transform.rotation.y + rotValue) * Time.deltaTime, transform.rotation.z));
    }
    private void SpeedControl(float speed)
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > speed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * speed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
}
