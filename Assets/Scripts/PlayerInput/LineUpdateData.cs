using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUpdateData : MonoBehaviour
{
    [Header("Constants")]
    public float ForceMin;
    public float ForceMax;
    public float ForceRangeMin;
    public float ForceRangeMax;
    public Vector2 ScrollSpeed;

    [Header("Members")]
    public bool HasBeenPlaced;

}
