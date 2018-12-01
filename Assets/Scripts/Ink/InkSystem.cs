using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class InkSystem : ComponentSystem
{
    public struct InkComponents
    {
        public readonly int Length;
        public ComponentArray<InkData> inkData;
    }

    [Inject]
    InkComponents inkComponents;

    /// <summary>
    /// Update the ink on the users.
    /// </summary>
    protected override void OnUpdate()
    {
        
    }

    /// <summary>
    /// Figure out how much ink is left.
    /// </summary>
    public static float GetRemainingInk(InkData inkData)
    {
        float remainingInk = 0;
        remainingInk += inkData.RemainingInk;
        return remainingInk;
    }

    /// <summary>
    /// Lose ink
    /// </summary>
    public static void SetUsedInk(InkData inkData, float usedInk)
    {
        inkData.RemainingInk -= usedInk;
        if(inkData.RemainingInk < 0)
        {
            inkData.RemainingInk = 0;
        }
    }

    /// <summary>
    /// Gain ink
    /// </summary>
    public static void SetRefundedInk(InkData inkData, float newInk)
    {
        inkData.RemainingInk += newInk;
        if (inkData.RemainingInk > inkData.MaxInk)
        {
            inkData.RemainingInk = inkData.MaxInk;
        }
    }
}
