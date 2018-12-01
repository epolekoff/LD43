using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Entities;
using System;

public class InkBarSystem : ComponentSystem
{
    struct InkDataEntities
    {
        public readonly int Length;
        public ComponentArray<InkData> inkData;
    }

    struct InkBarEntities
    {
        public readonly int Length;
        public ComponentArray<InkBarData> inkBarData;
        public ComponentArray<Image> image;
    }

    [Inject]
    InkDataEntities m_inkDataEntities;
    [Inject]
    InkBarEntities m_inkBarEntities;


    protected override void OnUpdate()
    {
        // Calculate the remaining ink.
        float inkRatio = 1f;
        for(int i = 0; i < m_inkDataEntities.Length; i++)
        {
            inkRatio = m_inkDataEntities.inkData[i].RemainingInk / m_inkDataEntities.inkData[i].MaxInk;
            break;
        }

        // Update the fill bar.
        for (int i = 0; i < m_inkBarEntities.Length; i++)
        {
            m_inkBarEntities.image[i].fillAmount = inkRatio;
        }
    }
}
