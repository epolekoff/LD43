using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MouseInputData : MonoBehaviour
{
    public Vector3 MousePositionWorldSpace;
    public bool MouseInValidPosition;

    public bool MouseButtonDown;
    public bool MouseButtonUp;
    public bool MouseButtonHeld;
}
