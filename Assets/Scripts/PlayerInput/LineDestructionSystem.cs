using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class LineDestructionSystem : ComponentSystem
{
    public struct Entities
    {
        public readonly int Length;
        public GameObjectArray gameObjects;
        public ComponentArray<TimedDestructionData> destructionData;
        public ComponentArray<LineDrawerData> lineDrawerData;
        public ComponentArray<LineRenderer> lineRenderer;
    }

    public struct InkEntities
    {
        public readonly int Length;
        public ComponentArray<InkData> inkData;
    }

    [Inject]
    private Entities entities;
    [Inject]
    private InkEntities inkEntities;

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
                bool shouldDestroy = ShrinkLine(entities.lineDrawerData[i], entities.lineRenderer[i], inkEntities, deltaTime);
                if (shouldDestroy)
                {
                    toDestroy.Add(entities.gameObjects[i]);
                }
            }
        }

        // Destroy them.
        foreach(var go in toDestroy)
        {
            GameObject.Destroy(go);
        }
    }

    private bool ShrinkLine(LineDrawerData lineData, LineRenderer lineRenderer, InkEntities inkEntities, float deltaTime)
    {
        bool shouldDestroy = false;
        float inkToRefund = 0;
        float amountToShrink = lineData.DestructionDistancePerSecond * deltaTime;
        if (lineData.LinePositions.Count >= 2)
        {
            Vector3 firstToSecondVector = lineData.LinePositions[1] - lineData.LinePositions[0];
            if (firstToSecondVector.magnitude > amountToShrink)
            {
                lineData.LinePositions[0] = lineData.LinePositions[0] + firstToSecondVector.normalized * amountToShrink;
                inkToRefund = amountToShrink * lineData.InkRefundedPerDistance;
            }
            else
            {
                lineData.LinePositions.RemoveAt(0);
                inkToRefund = firstToSecondVector.magnitude * lineData.InkRefundedPerDistance;
                Debug.Log(inkToRefund);
            }
        }
        else
        {
            shouldDestroy = true;
        }

        // Refund the ink.
        InkSystem.SetRefundedInk(inkEntities.inkData[0], inkToRefund);

        // Update the line renderer.
        lineRenderer.positionCount = lineData.LinePositions.Count;
        lineRenderer.SetPositions(lineData.LinePositions.ToArray());

        return shouldDestroy;
    }
}
