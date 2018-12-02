using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class KillZoneUpdateSystem : ComponentSystem
{

    public struct Entities
    {
        public readonly int Length;
        public GameObjectArray gameObjects;
        public ComponentArray<KillZoneData> killZone;
    }

    [Inject]
    public Entities m_entities;

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;

        for(int i = 0; i < m_entities.Length; i++)
        {

            // Count down.
            m_entities.killZone[i].Timer -= deltaTime;

            // Update the displayed text.
            if(m_entities.killZone[i].Timer > 0)
            {
                m_entities.killZone[i].CountdownText.text = Mathf.CeilToInt(m_entities.killZone[i].Timer).ToString();
            }
            else
            {
                m_entities.killZone[i].CountdownText.text = "";
            }

            // Handle the killing by turning on the collider.
            if(m_entities.killZone[i].Timer <= 0 && !m_entities.killZone[i].HasExploded)
            {
                m_entities.killZone[i].HasExploded = true;

                foreach (var collider in m_entities.killZone[i].Collider)
                {
                    collider.enabled = true;
                }
                GameObject.Destroy(m_entities.gameObjects[i], 0.1f);

                // Play a sound
                AudioManager.Instance.PlayExplosionSound();
            }
        }
    }
}
