using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class TimedDestructionSystem : ComponentSystem
{
    public struct Entities
    {
        public readonly int Length;
        public GameObjectArray gameObjects;
        public ComponentArray<TimedDestructionData> destructionData;
    }
    
    [Inject]
    private Entities entities;

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;
        var toDestroy = new List<GameObject>();

        // Determine which ones to destroy.
        for (var i = 0; i < entities.Length; ++i)
        {
            // Only count down if we want the timer started.
            if(!entities.destructionData[i].StartTimer)
            {
                continue;
            }

            entities.destructionData[i].Timer -= deltaTime;

            if(entities.destructionData[i].Timer <= 0)
            {
                toDestroy.Add(entities.gameObjects[i]);
            }
        }

        // Destroy them.
        foreach(var go in toDestroy)
        {
            GameObject.Destroy(go);
        }
    }
}
