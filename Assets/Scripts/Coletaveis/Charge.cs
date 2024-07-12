using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] private float valorCarga;
    private Collider2D col;

    private void Start()
    {
        this.col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager._audioManager.PlayOneShot(FMODEvents._fmodEvents.sfxCharge, this.transform.position);
            Special._special.Charge(valorCarga);
            Destroy(this.gameObject);
        }
    }
}
