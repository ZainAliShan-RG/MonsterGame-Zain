using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuSound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Button Sound Here!");
        audioSource.PlayOneShot(audioClip);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        /*Debug.Log("Button Sound Here!");
        audioSource.PlayOneShot(audioClip);*/
    }
    public void CheckPlayBtnClick()
    {
        Debug.Log("Script hit!");
    }

}
