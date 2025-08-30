using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsColliding : MonoBehaviour
{
    public bool isColliding;
    public LayerMask layerMask;
    private void Start()
    {
        isColliding = false;
    }
    private void FixedUpdate()
    {
        isColliding = collisions();
    }
    private bool collisions()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, layerMask);
        for(var i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].gameObject.name != gameObject.name)
            {
                return true;
            }
        }
        return false;
    }
}
