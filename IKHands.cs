using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class IKHands : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint leftHand;
    [SerializeField] private TwoBoneIKConstraint rightHand;
    [SerializeField] private GameObject leftGrip;
    [SerializeField] private GameObject rightGrip;
    [SerializeField] private RigBuilder rigBuilder;
    [SerializeField] public WeaponController currentWeapon;

    private Vector3 leftOriginalPos;
    private Quaternion leftOriginalRotation;
    private Vector3 rightOriginalPos;
    private Quaternion rightOriginalRotation;

    private Vector3 leftHandCurrentPosition;
    private Quaternion leftHandCurrentRotation;
    private Vector3 rightHandCurrentPosition;
    private Quaternion rightHandCurrentRotation;
    private float transitionSpeed = 10f;
    private void Start()
    {
       /* rightHand.data.target = weapon.transform.Find("Right-Target").transform;
        leftHand.data.target = weapon.transform.Find("Left-Target").transform;
        rigBuilder.Build();*/
        /*rightHand.data.target.rotation = weapon.transform.Find("Right-Target").transform.rotation;
        leftHand.data.target.rotation = weapon.transform.Find("Left-Target").transform.rotation;*/
        leftOriginalPos = leftHand.transform.position;
        leftOriginalRotation = leftHand.transform.rotation;
        rightOriginalPos = rightHand.transform.position;
        rightOriginalRotation = rightHand.transform.rotation;
    }
    private void LateUpdate()
    {
        if(currentWeapon != null)
        {
            UpdateHandPosition(leftHand, currentWeapon.leftHandGripPoint, currentWeapon.leftHandPositionOffset, currentWeapon.leftHandRotationOffset);
            UpdateHandPosition(rightHand, currentWeapon.rightHandGripPoint, currentWeapon.rightHandPositionOffset, currentWeapon.rightHandRotationOffset);
        }
        /*rightHand.data.target.position = weapon.transform.Find("Right-Target").transform.position;
        leftHand.data.target.position = weapon.transform.Find("Left-Target").transform.position;
        rightHand.data.target.rotation = weapon.transform.Find("Right-Target").transform.rotation;
        leftHand.data.target.rotation = weapon.transform.Find("Left-Target").transform.rotation;*/
    }
    private void UpdateHandPosition(TwoBoneIKConstraint hand, Transform gripPoint, Vector3 posOffset, Vector3 rotOffset)
    {
        Vector3 targetPosition = gripPoint.TransformPoint(posOffset);
        Quaternion targetRotation = gripPoint.rotation * Quaternion.Euler(rotOffset);

        // Smoothly moving to new positions and rotations
        hand.data.target.position = Vector3.Lerp(hand.data.target.position, targetPosition, Time.deltaTime * transitionSpeed);
        hand.data.target.rotation = Quaternion.Slerp(hand.data.target.rotation, targetRotation, Time.deltaTime * transitionSpeed);

        hand.weight = 1f;
    }

    private void OnValidate()
    {
        if(rigBuilder != null && currentWeapon != null)
        {
            rigBuilder.Build();
        }
    }
    public void ResetWeight()
    {
        leftHand.weight = 0f;
        rightHand.weight = 0f;
    }
    public void IsHoldingWeapon(bool isHoldingWeapon)
    {
        if (isHoldingWeapon)
        {
            /*leftHand.transform.position = weapon.transform.parent.Find("Left-Target").transform.position;
            leftHand.transform.rotation = weapon.transform.parent.Find("Left-Target").transform.rotation;

            rightHand.transform.position = weapon.transform.parent.Find("Right-Target").transform.position;
            rightHand.transform.rotation = weapon.transform.parent.Find("Right-Target").transform.rotation;*/
            // Left Hand
            Vector3 leftHandPosition = currentWeapon.leftHandGripPoint.TransformPoint(currentWeapon.leftHandPositionOffset);
            Quaternion leftHandRotation = currentWeapon.leftHandGripPoint.rotation * Quaternion.Euler(currentWeapon.leftHandRotationOffset);

            leftHand.transform.position = leftHandPosition;
            leftHand.transform.rotation = leftHandRotation;

            // Right Hand
            Vector3 rightHandPosition = currentWeapon.rightHandGripPoint.TransformPoint(currentWeapon.rightHandPositionOffset);
            Quaternion rightHandRotation = currentWeapon.rightHandGripPoint.rotation * Quaternion.Euler(currentWeapon.rightHandRotationOffset);

            rightHand.transform.position = rightHandPosition;
            rightHand.transform.rotation = rightHandRotation;

            // Calculate target positions and rotations (as in step 2)

            // Smoothly interpolate to new positions and rotations
            leftHandCurrentPosition = Vector3.Lerp(leftHandCurrentPosition, leftHandPosition, Time.deltaTime * transitionSpeed);
            leftHandCurrentRotation = Quaternion.Slerp(leftHandCurrentRotation, leftHandRotation, Time.deltaTime * transitionSpeed);

            rightHandCurrentPosition = Vector3.Lerp(rightHandCurrentPosition, rightHandPosition, Time.deltaTime * transitionSpeed);
            rightHandCurrentRotation = Quaternion.Slerp(rightHandCurrentRotation, rightHandRotation, Time.deltaTime * transitionSpeed);

            // Apply to IK targets
            leftHand.transform.SetPositionAndRotation(leftHandCurrentPosition, leftHandCurrentRotation);
            rightHand.transform.SetPositionAndRotation(rightHandCurrentPosition, rightHandCurrentRotation);
        }
        else
        {
            leftHand.transform.position = leftOriginalPos;
            leftHand.transform.rotation = leftOriginalRotation;

            rightHand.transform.position = rightOriginalPos;
            rightHand.transform.rotation = rightOriginalRotation;

        }
    }
}
