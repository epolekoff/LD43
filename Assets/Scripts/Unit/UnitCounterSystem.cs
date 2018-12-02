using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class UnitCounterSystem : ComponentSystem
{
	struct CounterEntities
    {
        public readonly int Length;
        public ComponentArray<TMPro.TextMeshProUGUI> Text;
        public ComponentArray<UnitCounterData> UnitCounter;
    }

    struct UnitEntities
    {
        public readonly int Length;
        public ComponentArray<WalkingMovementData> Units;
    }

    [Inject]
    CounterEntities m_counters;
    [Inject]
    UnitEntities m_units;

    /// <summary>
    /// Update the text with the unit count.
    /// </summary>
    protected override void OnUpdate()
    {
        for (int i = 0; i < m_counters.Length; i++)
        {
            m_counters.Text[i].text = m_units.Length.ToString();
        }
    }

}
