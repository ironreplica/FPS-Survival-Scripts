using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnAnimationTrigger : MonoBehaviour
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
