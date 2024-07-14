using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveController : MonoBehaviour
{
    [SerializeField] private GameObject wave1;
    [SerializeField] private GameObject wave2;
    [SerializeField] private GameObject wave21;
    [SerializeField] private GameObject wave22;
    [SerializeField] private GameObject wave3;
    [SerializeField] private GameObject wave31;
    [SerializeField] private GameObject wave32;
    [SerializeField] private GameObject wave33;
    [SerializeField] private GameObject wave4;
    [SerializeField] private GameObject wave41;
    [SerializeField] private GameObject wave42;
    [SerializeField] private GameObject wave43;
    [SerializeField] private GameObject wave44;
    [SerializeField] private GameObject wave45;
    [SerializeField] private GameObject wave46;
    [SerializeField] private GameObject wave47;

    [SerializeField] private GameObject coletavel;
    [SerializeField] private GameObject coletavelPrefab;

    [SerializeField] private GameObject waveUI;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI deadDebug;
    [SerializeField] private GameObject dicaText;

    [SerializeField] private Vector2 spawnAreaMin;
    [SerializeField] private Vector2 spawnAreaMax;

    private GameObject currentWaveInstance;
    [SerializeField] private Camera mainCamera;

    void Start()
    {
        GameController.OnEnemiesDies += ChecarWave;
        StartCoroutine(IniciarWaves());
        
    }
    void OnDestroy()
    {
        GameController.OnEnemiesDies -= ChecarWave;
    }
    void OnDrawGizmosSelected()
    {
        // Desenha um wireframe na área de spawn para fins de debug
        Gizmos.color = Color.green;
        Vector3 minPoint = new Vector3(spawnAreaMin.x, spawnAreaMin.y, 0);
        Vector3 maxPoint = new Vector3(spawnAreaMax.x, spawnAreaMax.y, 0);
        Gizmos.DrawLine(minPoint, new Vector3(minPoint.x, maxPoint.y, 0));
        Gizmos.DrawLine(minPoint, new Vector3(maxPoint.x, minPoint.y, 0));
        Gizmos.DrawLine(maxPoint, new Vector3(maxPoint.x, minPoint.y, 0));
        Gizmos.DrawLine(maxPoint, new Vector3(minPoint.x, maxPoint.y, 0));
    }

    private void ChecarWave(int deads)
    {
        deadDebug.text = deads.ToString();

        if (deads == 4 && wave2 != null)
        {
            coletavel = Instantiate(coletavelPrefab, this.transform.position, Quaternion.identity);
            coletavel.SetActive(true);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Debug.Log("Wave 2");
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave2, spawnPosition, Quaternion.identity);
            StartCoroutine(MostrarWaveUI("Wave 2"));
            dicaText.SetActive(true);
        }
        else if (deads == 12 && wave21 != null)
        {
            coletavel.SetActive(true);
            dicaText.SetActive(false);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave21, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 2.1");
        }
        else if (deads == 14 && wave22 != null)
        {
            coletavel.SetActive(true);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave22, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 2.2");
        }
        else if (deads == 20 && wave3 != null)
        {
            coletavel.SetActive(true);
            StartCoroutine(MostrarWaveUI("Wave 3"));
            dicaText.SetActive(false);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave3, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 3");
        }
        else if (deads == 25 && wave31 != null)
        {
            coletavel.SetActive(true);
            dicaText.SetActive(false);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave31, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 3.1");
        }
        else if (deads == 26 && wave32 != null)
        {
            coletavel.SetActive(true);
            dicaText.SetActive(false);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave32, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 3.2");
        }
        else if (deads == 30 && wave33 != null)
        {
            coletavel.SetActive(true);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave33, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 3.3");
        }
        else if (deads == 33 && wave4 != null)
        {
            coletavel.SetActive(true);
            StartCoroutine(MostrarWaveUI("Wave 4"));
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave4, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 4");
        }
        else if (deads == 39 && wave41 != null)
        {
            coletavel.SetActive(true);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave41, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 4.1");
        }
        else if (deads == 42 && wave42 != null)
        {
            coletavel.SetActive(true);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave42, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 4.2");
        }
        else if (deads == 44 && wave44 != null)
        {
            coletavel.SetActive(true);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave44, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 4.4");
        }
        else if (deads == 49 && wave45 != null)
        {
            coletavel.SetActive(true);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave45, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 4.5");
        }
        else if (deads == 54 && wave46 != null)
        {
            coletavel.SetActive(true);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave46, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 4.6");
        }
        else if (deads == 57 && wave47 != null)
        {
            coletavel.SetActive(true);
            if (currentWaveInstance != null)
            {
                Destroy(currentWaveInstance);
                currentWaveInstance = null;
            }
            Vector2 spawnPosition = GetRandomSpawnPosition();
            currentWaveInstance = Instantiate(wave47, spawnPosition, Quaternion.identity);
            Debug.Log("Wave 4.7");
        }
    }


    private IEnumerator IniciarWaves()
    {

        //coletavel.SetActive(false);
        waveText.text = "Wave 1";
        yield return new WaitForSeconds(2f);
        StartCoroutine(MostrarWaveUI("Wave 1"));
        Vector2 spawnPosition = GetRandomSpawnPosition();
        currentWaveInstance = Instantiate(wave1, spawnPosition, Quaternion.identity);
    }

    private IEnumerator MostrarWaveUI(string text)
    {
        waveText.text = text;
        waveUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        waveUI.SetActive(false);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition;
        do
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector2(randomX, randomY);
        } while (IsInsideCameraView(spawnPosition));

        return spawnPosition;
    }

    private bool IsInsideCameraView(Vector2 position)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(new Vector3(position.x, position.y, 0));
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    private void AtivarColetavelEDestroyarInstanciaAtual()
    {
        coletavel.SetActive(true);
        if (currentWaveInstance != null)
        {
            Destroy(currentWaveInstance);
            currentWaveInstance = null;
        }
    }

    public void ReiniciarWaveController()
    {
        if (currentWaveInstance != null)
        {
            Destroy(currentWaveInstance);
            currentWaveInstance = null;
        }
        coletavel.SetActive(false);
        waveUI.SetActive(false);
        dicaText.SetActive(false);
    }
}
