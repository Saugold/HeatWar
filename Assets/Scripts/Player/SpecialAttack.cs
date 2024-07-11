using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    [SerializeField]private GameObject explosionPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")){
            Vector3 position = collision.transform.position;
            Instantiate(explosionPrefab, position, Quaternion.identity);
            this.gameObject.SetActive(false);
            Debug.Log("Explodiu");
        }
    }
}
