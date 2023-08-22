using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    public static Action isPlayerDead; // Action to stop enemies co-routine that would stop enemies from respawning
    public static Action PlayDeathSound; // Action to stop enemies co-routine that would stop enemies from respawning
}
