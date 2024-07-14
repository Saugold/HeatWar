using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class UISound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnHover()
    {
        AudioManager._audioManager.PlayOneShot(FMODEvents._fmodEvents.sfxSelector, this.transform.position);
    }

    public void OnClick()
    {
        AudioManager._audioManager.PlayOneShot(FMODEvents._fmodEvents.sfxConfirm, this.transform.position);
    }

    public void Back()
    {
        AudioManager._audioManager.PlayOneShot(FMODEvents._fmodEvents.sfxBack, this.transform.position);
    }

    public void Começar()
    {
        AudioManager._audioManager.StopAllAudioEvents();
        AudioManager._audioManager.PlayOneShot(FMODEvents._fmodEvents.sfxStart, this.transform.position);
    }
    public void VoltarMenu()
    {
        AudioManager._audioManager.PlayOneShot(FMODEvents._fmodEvents.sfxBack, this.transform.position);
    }
}
