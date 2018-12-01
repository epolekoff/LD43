using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct UnitSpawnData : IComponentData
{
    [Header("Bounds")]
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;

    [Header("Count")]
    public int NumToSpawn;
}

public class UnitSpawnDataComponent : ComponentDataWrapper<UnitSpawnData> { }
