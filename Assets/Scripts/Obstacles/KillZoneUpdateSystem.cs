using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class KillZoneUpdateSystem : ComponentSystem
{
    struct Entities
    {
        public KillZoneData killZone;
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;

        foreach(var entity in GetEntities<Entities>())
        {
            // Count down.
            entity.killZone.Timer -= deltaTime;

            // Update the displayed text.
            entity.killZone.CountdownText.text = Mathf.CeilToInt(entity.killZone.Timer).ToString();

            // Handle the killing by turning on the collider.
            if(entity.killZone.Timer <= 0)
            {
                entity.killZone.Collider.enabled = true;
            }
        }
    }
}
