using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class LineDrawerSystem : ComponentSystem
{
    struct Components
    {
        public LineDrawerData lineDrawerData;
        public LineUpdateData lineUpdateData;
        public LineRenderer lineRenderer;
        public TimedDestructionData timedDestructionData;
        public MouseInputData mouseData;
    }

    struct InkComponents
    {
        public readonly int Length;
        public ComponentArray<InkData> inkData;
    }

    [Inject]
    InkComponents m_inkComponents;

    /// <summary>
    /// Update.
    /// </summary>
    protected override void OnUpdate ()
    {
        // Update each component with the line data.
		foreach(var entity in GetEntities<Components>())
        {
            if(entity.lineUpdateData.HasBeenPlaced)
            {
                continue;
            }

            // Handle releasing the mouse to stop drawing.
            if(entity.mouseData.MouseButtonUp || !entity.mouseData.MouseInValidPosition || m_inkComponents.inkData[0].RemainingInk <= 0)
            {
                PlaceLine(entity);
            }

            if(entity.mouseData.MouseInValidPosition && m_inkComponents.inkData[0].RemainingInk > 0 && !entity.lineUpdateData.HasBeenPlaced)
            {
                // Determine if a new anchor point is needed.
                Vector3 mousePosition = entity.mouseData.MousePositionWorldSpace;
                Vector3 oldToAnchorVector = entity.lineDrawerData.PreviousAnchoredLinePosition - entity.lineDrawerData.LinePositions[entity.lineDrawerData.LinePositions.Count - 1];
                Vector3 anchorToCurrentVector = mousePosition - entity.lineDrawerData.PreviousAnchoredLinePosition;
                float oldAngle = Mathf.Atan2(oldToAnchorVector.z, oldToAnchorVector.x);
                float currentAngle = Mathf.Atan2(anchorToCurrentVector.z, anchorToCurrentVector.x);
                float deltaAngle = Mathf.Abs(oldAngle - currentAngle) * Mathf.Rad2Deg;
                float currentDistance = anchorToCurrentVector.magnitude;
                if (deltaAngle > entity.lineDrawerData.MaxAllowedAngle && currentDistance > entity.lineDrawerData.MinAnchorDistance)
                {
                    // Add a new point.
                    entity.lineDrawerData.LinePositions.Add(mousePosition);
                    entity.lineDrawerData.PreviousAnchoredLinePosition = mousePosition;

                    // Lock in the spent ink.
                    if(entity.lineDrawerData.LinePositions.Count > 2)
                    {
                        InkSystem.SetUsedInk(m_inkComponents.inkData[0], entity.lineDrawerData.InkUsedPerDistance * currentDistance);
                        if(m_inkComponents.inkData[0].RemainingInk <= 0)
                        {
                            PlaceLine(entity);
                        }
                    }
                }
                else
                {
                    // Update the last position in the list to match the mouse position.
                    entity.lineDrawerData.LinePositions[entity.lineDrawerData.LinePositions.Count - 1] = mousePosition;

                    // Show some spent ink, but don't lock it in.
                }
            }


            // Update the line renderer with the new positions.
            entity.lineRenderer.positionCount = entity.lineDrawerData.LinePositions.Count;
            entity.lineRenderer.SetPositions(entity.lineDrawerData.LinePositions.ToArray());
        }
	}

    /// <summary>
    /// Stop drawing the line.
    /// </summary>
    private void PlaceLine(Components entity)
    {
        entity.lineUpdateData.HasBeenPlaced = true;
        entity.timedDestructionData.StartTimer = true;
    }
}
