using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Special : MonoBehaviour
{
    public GameObject specialAttackPrefab; // Prefab da bola de ataque especial
    public Transform firePoint; // Ponto de disparo da bola
    public float maxChargeTime = 1f; // Tempo máximo de carga
    public float maxSize = 2f; // Tamanho máximo da bola ao ser carregada

    public static Special _special;
    private float currentChargeTime;
    public bool isCharging;
    private bool isShoot;
    [SerializeField] private Slider specialSlider;
    [SerializeField] private float carga;

    private void Start()
    {
        isShoot = false;
        _special = this;
    }
    private void Update()
    {
        UpdateSpecialSlider();
            if (isCharging)
            {

                currentChargeTime += Time.deltaTime; //calcula o tempo de carga
                float size = Mathf.Lerp(1f, maxSize, currentChargeTime / maxChargeTime); //aumenta tamanho do psrite
                specialAttackPrefab.transform.localScale = new Vector3(size, size, size);
                specialAttackPrefab.transform.position = firePoint.position;

                if (currentChargeTime >= maxChargeTime) // solta se o tempo de carga chegar no maximo
                {
                    isCharging = false;
                    Fire();
                }
            }     
            
    }

    public void OnChargeAttack(InputAction.CallbackContext context)
    {
        //verifica se tem carga
        if (carga == 100f)
        {
            if (context.started && isShoot == false)
            {
                StartCharging();
            }
            else if (context.canceled)
            {
                Fire();
            }
        }
        else
            Debug.Log("Sem carga");
        
    }

    private void StartCharging()
    {
        
        specialAttackPrefab.SetActive(true);
        specialAttackPrefab.transform.position = firePoint.position;
        currentChargeTime = 0f;
        isCharging = true;
    }

    private void Fire()
    {
        if (specialAttackPrefab == null)
        {
            return;
        }

        isCharging = false;
        Rigidbody2D rb = specialAttackPrefab.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            carga = 0f;
            isShoot = true;
            rb.velocity = transform.up * 10f;
            // Inicia a corrotina para desativar o prefab após 3 segundos
            StartCoroutine(ResetSpecialAttack());
        }

        
    }

    private IEnumerator ResetSpecialAttack()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("pronto");
        // Reseta a posição e desativa o prefab
        specialAttackPrefab.transform.position = firePoint.position;
        specialAttackPrefab.SetActive(false);
        isShoot = false;
        
    }

    //coletavel ------------
    
    public void Charge(float value)
    {
        if (carga < 100)
        {
            //adicionar carga
            carga += value;
            Debug.Log(carga);
        }
        else
            Debug.Log("Canhão carregado");
        
    }

    //barra
    private void UpdateSpecialSlider()
    {
        if (specialSlider != null)
        {
            specialSlider.value = carga/100;
        }
    }
}
