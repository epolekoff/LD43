using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine.Jobs;
using System;

public class UnitSpawnSystem : JobComponentSystem {

    struct Components
    {
        public UnitSpawnData spawnData;
    }

    [Inject]
    private Components components;

    struct SpawnUnitJob : IJobParallelFor
    {
        public void Execute(int index)
        {
            
        }
    }

    
}
