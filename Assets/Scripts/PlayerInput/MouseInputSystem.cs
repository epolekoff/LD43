using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class MouseInputSystem : ComponentSystem
{

    /// <summary>
    /// Components.
    /// </summary>
    struct Components
    {
        public MouseInputData mouseData;
    }

    /// <summary>
    /// Update.
    /// </summary>
    protected override void OnUpdate()
    {
        // Raycast from the mouse to the map to get the mouse position in world space.
        Vector3 worldSpacePosition = Vector3.negativeInfinity;
        bool mouseInValidPosition = false;
        Ray cameraRay = GameManager.Instance.GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(cameraRay, out hitInfo, 30f, LayerMask.GetMask("GameMap")))
        {
            worldSpacePosition = hitInfo.point + new Vector3(0, 1, 0);
            mouseInValidPosition = true;
        }

        // Get the click state.
        bool down = Input.GetMouseButtonDown(0);
        bool up = Input.GetMouseButtonUp(0);
        bool held = Input.GetMouseButton(0);

        Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.red, 1f);

        // Update all of the entities.
        foreach (var entity in GetEntities<Components>())
        {
            entity.mouseData.MousePositionWorldSpace = worldSpacePosition;
            entity.mouseData.MouseInValidPosition = mouseInValidPosition;

            entity.mouseData.MouseButtonDown = down;
            entity.mouseData.MouseButtonUp = up;
            entity.mouseData.MouseButtonHeld = held;

        }
    }

}
