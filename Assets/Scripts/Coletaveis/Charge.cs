using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] private float valorCarga;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Special._special.Charge(valorCarga);
            Destroy(this.gameObject);
        }
    }
}
