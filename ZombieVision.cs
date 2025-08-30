using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVision : MonoBehaviour
{
    private AudioSource audioSource;
    private RaycastHit hit;
    public float visionLength = 10f;
    private LayerMask layerMask;
    private void Start()
    {
        audioSource = transform.parent.Find("Audio Source").GetComponent<AudioSource>();
        layerMask = LayerMask.GetMask("Enemy");
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * visionLength, Color.red);
        if(Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, visionLength, ~layerMask))
        {
            
            if(hit.collider.CompareTag("Player") || hit.collider.CompareTag("Building") || hit.collider.CompareTag("Campfire"))
            {
                /*GameObject curTarget = hit.collider.gameObject;*/
                /*transform.parent.GetComponent<ZombieController>().seesPlayer = true;*/
                transform.parent.GetComponent<ZombieController>().curTarget = hit.collider.gameObject;
                /*Vector3 lookAt = new Vector3(curTarget.transform.position.x, transform.position.y, curTarget.transform.position.z);
                transform.LookAt(lookAt);*/
                if (hit.distance < 1)
                {
                    transform.parent.GetComponent<ZombieController>().isTouchingEnemy = true;
                    transform.parent.GetComponent<ZombieController>().visHit = hit;
                }
                else
                {
                    transform.parent.GetComponent<ZombieController>().isTouchingEnemy = false;
                    

                }
            }
            else
            {
                /*transform.parent.GetComponent<ZombieController>().seesPlayer = false;*/   
                transform.parent.GetComponent<ZombieController>().curTarget = null;
            }
        }
        else
        {
            /*transform.parent.GetComponent<ZombieController>().seesPlayer = false;*/
            transform.parent.GetComponent<ZombieController>().curTarget = null;
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Building"))
        {
            audioSource.Play();
            transform.parent.GetComponent<ZombieController>().seesPlayer = true;
            transform.parent.GetComponent<ZombieController>().curTarget = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Building"))
        {
            transform.parent.GetComponent<ZombieController>().seesPlayer = false;
            transform.parent.GetComponent<ZombieController>().curTarget = null;
        }
    }*/
}
