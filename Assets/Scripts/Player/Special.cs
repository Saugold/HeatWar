using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

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

    private FMOD.Studio.EventInstance instance;

    
    [SerializeField] private EventReference fmodEvent;

    [SerializeField]
    [Range(0f, 1f)]
    private float valorVFX;

    // Parâmetro de carga do ataque
    private float chargeState = 0f;

    private void Start()
    {
        _special = this;
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        
    }

    private void FixedUpdate()
    {
        instance.setParameterByName("tiro carregado", valorVFX);
        UpdateSpecialSlider();

        // Atualiza o parâmetro ChargeState com base no estado de carregamento
        if (isCharging)
        {

            currentChargeTime += Time.deltaTime; // Calcula o tempo de carga
            float size = Mathf.Lerp(1f, maxSize, currentChargeTime / maxChargeTime); // Aumenta o tamanho do prefab
            specialAttackPrefab.transform.localScale = new Vector3(size, size, size);
            specialAttackPrefab.transform.position = firePoint.position;

            // Atualiza o parâmetro ChargeState gradualmente até 0.99
            chargeState = Mathf.Clamp01(currentChargeTime / maxChargeTime * 0.99f);

            
        }
            

        // Define o valor do parâmetro no FMOD
        //specialEventInstance.setParameterByName("ChargeState", chargeState);
    }

    public void OnChargeAttack(InputAction.CallbackContext context)
    {
        // Verifica se tem carga suficiente para iniciar o ataque
        if (carga >= 100)
        {
            if (context.started && !isShoot)
            {
                instance.start();
                valorVFX = 0;
                StartCharging();
            }
            else if (context.canceled)
            {
                valorVFX = 1;
                Fire();
                
                //specialEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
        else
        {
            Debug.Log("Sem carga suficiente para disparar o ataque especial.");
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
            // Define o parâmetro ChargeState como 1
            chargeState = 1f;
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
        // Reseta a posição e desativa o prefab
        valorVFX = 0;
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        specialAttackPrefab.SetActive(false);
        isShoot = false;
    }

    // Método para atualizar o valor da carga
    public void Charge(float value)
    {
        if (carga < 100)
        {
            carga += value;
            UpdateSpecialSlider(); // Atualiza a barra de progresso imediatamente após a carga
        }
        else
        {
            Debug.Log("Carga máxima atingida.");
        }
    }

    // Método para atualizar o slider visual da carga
    private void UpdateSpecialSlider()
    {
        if (specialSlider != null)
        {
            specialSlider.value = carga / 100f;
        }
    }
    private void OnDestroy()
    {
        if (instance.isValid())
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
        }
    }
}
