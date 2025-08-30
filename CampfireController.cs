using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireController : MonoBehaviour
{
    private bool inRadius;
    private PlayerHealth playerHealth;
    public float healAmount = 5f;
    private Coroutine curCoroutine;
    private void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRadius = true;
            curCoroutine = StartCoroutine(HealPlayer());

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRadius = false;
            StopCoroutine(curCoroutine);
            curCoroutine = null;
        }
    }
    private IEnumerator HealPlayer()
    {
        while(inRadius)
        {
            yield return new WaitForSeconds(1f);
            if(playerHealth.getHealth() + healAmount <= playerHealth.getMaxHealth() )
            {
                Debug.Log("Heal");
                playerHealth.AddHealth(healAmount);
            }
        }
        
    }
}
