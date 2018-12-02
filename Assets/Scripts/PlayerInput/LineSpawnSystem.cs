using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class LineSpawnSystem : ComponentSystem
{
    struct Components
    {
        public LineSpawnData lineSpawnData;
        public MouseInputData mouseData;
    }

    /// <summary>
    /// Attempt to spawn the line if needed.
    /// </summary>
    protected override void OnUpdate()
    {
        if(!GameManager.Instance.GameActive)
        {
            return;
        }
        
        // Update each component with the line data.
        foreach (var entity in GetEntities<Components>())
        {
            // Handle mouse down to start drawing.
            if (entity.mouseData.MouseButtonDown)
            {
                var lineGo = GameObject.Instantiate(GameManager.Instance.LinePrefab);
                var newLineData = lineGo.GetComponent<LineDrawerData>();
                newLineData.LinePositions.Add(entity.mouseData.MousePositionWorldSpace);

                AudioManager.Instance.PlaySound(AudioManager.Instance.LineDrawingSound);
            }
        }
    }
}
