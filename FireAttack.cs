using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    public HandController handController;
    public void PunchParticles()
    { 
        handController.Attack();
    }
    public void SetCanPunch()
    {
        handController.canAttack = true;
    }
    public void HideHands()
    {

    }
}
