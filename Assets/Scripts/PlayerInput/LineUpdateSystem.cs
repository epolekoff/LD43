using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class LineUpdateSystem : ComponentSystem
{
    public struct ForceReceivers
    {
        public readonly int Length;
        public ComponentArray<Rigidbody> rigidbody;
    }

    public struct LineComponents
    {
        public readonly int Length;
        public ComponentArray<LineUpdateData> lineUpdateData;
        public ComponentArray<LineDrawerData> lineDrawerData; // TODO: Ideally, I would pull LinePositions out into a new object.
        public ComponentArray<LineRenderer> lineRenderer;
    }

    [Inject]
    ForceReceivers m_Receivers;
    [Inject]
    LineComponents m_Lines;

    private struct ForceEffect
    {
        public Rigidbody rigidbody;
        public Vector3 force;
    }

    protected override void OnUpdate()
    {
        Dictionary<int, ForceEffect> forceEffectsByReceiverIndex = new Dictionary<int, ForceEffect>();
        float deltaTime = Time.deltaTime;

        // Iterate the lines and gather all objects in range.
        for (int l = 0; l < m_Lines.Length; l++)
        {
            // Scroll the UVs of the line.
            m_Lines.lineRenderer[l].material.mainTextureOffset += m_Lines.lineUpdateData[l].ScrollSpeed * deltaTime;

            // Iterate all of the receivers.
            for (int r = 0; r < m_Receivers.Length; r++)
            {
                // Don't apply force to an object twice.
                if(forceEffectsByReceiverIndex.ContainsKey(r))
                {
                    continue;
                }

                // Check if this receiver is in range.
                foreach(Vector3 point in m_Lines.lineDrawerData[l].LinePositions)
                {
                    // Add the force and break if in range.
                    Vector3 lineToReceiver = m_Receivers.rigidbody[r].transform.position - point;
                    if (lineToReceiver.magnitude < m_Lines.lineUpdateData[l].ForceRangeMax)
                    {
                        float rangeRatio = (lineToReceiver.magnitude - m_Lines.lineUpdateData[l].ForceRangeMin) / (m_Lines.lineUpdateData[l].ForceRangeMax - m_Lines.lineUpdateData[l].ForceRangeMin);
                        float force = ((1-rangeRatio) * (m_Lines.lineUpdateData[l].ForceMax - m_Lines.lineUpdateData[l].ForceMin) + m_Lines.lineUpdateData[l].ForceMin);
                        forceEffectsByReceiverIndex.Add(r, new ForceEffect()
                        {
                            rigidbody = m_Receivers.rigidbody[r],
                            force = lineToReceiver.normalized * force
                        });
                        break;
                    }
                }
            }
        }

        // Iterate all touched objects and apply a force to them.
        foreach(var index in forceEffectsByReceiverIndex.Keys)
        {
            ForceEffect forceEffect = forceEffectsByReceiverIndex[index];
            forceEffect.rigidbody.AddForce(forceEffect.force);
        }
    }
}
