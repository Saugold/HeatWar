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
    [SerializeField] private GameObject explosionPrefab;

    private void Start()
    {
        isShoot = false;
        _special = this;
    }
    private void Update()
    {
        Debug.Log(carga);
        UpdateSpecialSlider();
        if (isCharging)
        {
            currentChargeTime += Time.deltaTime; // Calcula o tempo de carga
            float size = Mathf.Lerp(1f, maxSize, currentChargeTime / maxChargeTime); // Aumenta o tamanho do prefab
            specialAttackPrefab.transform.localScale = new Vector3(size, size, size);
            specialAttackPrefab.transform.position = firePoint.position;

            if (currentChargeTime >= maxChargeTime) // Solta se o tempo de carga chegar no máximo
            {
                isCharging = false;
                Fire();
            }
        }
    }

    public void OnChargeAttack(InputAction.CallbackContext context)
    {
        // Verifica se tem carga
        if (carga >= 100)
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
        {
            Debug.Log("Sem carga");
        }
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
        if (rb != null && carga >= 100)
        {
            isShoot = true;
            rb.velocity = transform.up * 10f;
            carga = 0;
            UpdateSpecialSlider(); // Atualiza a barra de progresso imediatamente após o disparo
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

    // Coletável
    public void Charge(float value)
    {
        if (carga < 100)
        {
            carga += value;
            Debug.Log(carga);
            UpdateSpecialSlider(); // Atualiza a barra de progresso imediatamente após a carga
        }
        else
        {
            Debug.Log("Canhão carregado");
        }
    }

    // Barra
    private void UpdateSpecialSlider()
    {
        if (specialSlider != null)
        {
            specialSlider.value = carga / 100f;
        }
    }

    
}
