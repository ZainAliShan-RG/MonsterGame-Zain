using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void OnEnable()
    {
        EventController.PlayDeathSound += FireDeathSound;
    }

    private void OnDisable()
    {
        EventController.PlayDeathSound -= FireDeathSound;
    }

    private void FireDeathSound()
    {
        audioSource.Play();
    }
}
