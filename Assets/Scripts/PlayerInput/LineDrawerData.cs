using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class LineDrawerData : MonoBehaviour
{
    [Header("Constants")]
    public float MaxAllowedAngle;
    public float MinAnchorDistance;

    [Header("Members")]
    public Vector3 PreviousAnchoredLinePosition;
    public List<Vector3> LinePositions;
}
