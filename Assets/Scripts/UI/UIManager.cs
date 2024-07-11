using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void LifeUpdate(int life);
    public static event LifeUpdate OnLifeUpdated;
    [SerializeField] private TextMeshProUGUI vida;
    [SerializeField] private GameObject reiniciar;

    void Start()
    {
        PlayerLife.OnPlayerDied += Reiniciar;
        PlayerLife.OnPlayerDamage += UpdateLife;
    }

    void OnDestroy()
    {
        PlayerLife.OnPlayerDied -= Reiniciar;
        PlayerLife.OnPlayerDamage -= UpdateLife;
    }

    private void Reiniciar()
    {
        reiniciar.SetActive(true);
        Time.timeScale = 0; // Pausa o jogo
    }

    private void UpdateLife(int life)
    {
        vida.text = life.ToString();
    }

    public void Começar()
    {
        Time.timeScale = 1; // Reinicia o tempo do jogo
        SceneManager.LoadScene("Fase");
    }
}
