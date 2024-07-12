using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player SFX")]
    //Som tocado quando o player caminha
    [field: SerializeField] public EventReference sfxTiro;
    [field: SerializeField] public EventReference sfxEspecial;
    [field: SerializeField] public EventReference sfxDamage;
    [field: SerializeField] public EventReference sfxDie;

    [field: Header("Collectors SFX")]
    [field: SerializeField] public EventReference sfxCharge;

    [field: Header("SFX Small Enemy")]
    [field: SerializeField] public EventReference sfxTiroSmall;
    [field: SerializeField] public EventReference sfxDanoSmall;
    [field: SerializeField] public EventReference sfxDeadSmall;

    [field: Header("SFX Large Enemy")]
    [field: SerializeField] public EventReference sfxTiroLarge;
    [field: SerializeField] public EventReference sfxDanoLarge;
    [field: SerializeField] public EventReference sfxDeadLarge;

    [field: Header("SFX Large Enemy")]
    [field: SerializeField] public EventReference sfxTiroBig;
    [field: SerializeField] public EventReference sfxDanoBig;
    [field: SerializeField] public EventReference sfxDeadBig;

    [field: Header("Soundtrack")]
    //Toca musica da cena
    [field: SerializeField] public EventReference musicGameplay;
    //toca musica do menu
    [field: SerializeField] public EventReference musicTema;
    [field: SerializeField] public EventReference gameOver;

    //sons do menu
    [field: Header("Menu")]
    [field: SerializeField] public EventReference sfxSelector;
    [field: SerializeField] public EventReference sfxConfirm;
    [field: SerializeField] public EventReference sfxBack;
    [field: SerializeField] public EventReference sfxStart;

    public static FMODEvents _fmodEvents;

    private void Awake()
    {
        if (_fmodEvents != null)
        {
            Debug.Log("Já existe mais de um fmodeEvents na cena");
        }
        else
            _fmodEvents = this;

    }
}
