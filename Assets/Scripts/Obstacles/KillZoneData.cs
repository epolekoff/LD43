using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillZoneData : MonoBehaviour
{
    [Header("Constants")]
    public float TimerMax;
    public List<Collider> Collider;
    public TMPro.TextMeshProUGUI CountdownText;


    [Header("Members")]
    public float Timer;
}
