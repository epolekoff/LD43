using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMovementData : MonoBehaviour
{
    [Header("Constants")]
    public float MinWalkSpeed = 1f;
    public float MaxWalkSpeed = 3f;

    public float MinAngle = 0f;
    public float MaxAngle = 360;

    public float MinWalkTime = 1f;
    public float MaxWalkTime = 3f;

    public float MinRestTime = 1f;
    public float MaxRestTime = 3f;


    [Header("Runtime Variables")]
    public bool Walking = false;
    public float CurrentTimer = 0f;
    public float CurrentWalkSpeed = 0f;
    public Vector3 CurrentDirection = Vector3.zero;
    public bool CanWalk = true;
}
