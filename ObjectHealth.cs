using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public GameObject destroyEffect;
    private void Start()
    {
        curHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        if(curHealth <= 0)
        {
            GameObject effect = Instantiate(destroyEffect);
            effect.transform.parent = null;
            effect.transform.position = transform.position;
            Destroy(gameObject);

        }
    }
}
