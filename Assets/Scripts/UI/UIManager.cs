using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void LifeUpdate(int life);
    public static event LifeUpdate OnLifeUpdated;
    [SerializeField] private TextMeshProUGUI vida;
    [SerializeField] private GameObject reiniciar;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject pause;
    private bool isPaused;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button reiniciarButton;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private GameObject levelLoaderObject;
    //[SerializeField] private game
    void Start()
    {
        isPaused = false;
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
        GameController._gameController.deads = 0;
        AudioManager._audioManager.StopAllAudioEvents();
        AudioManager._audioManager.PlayOneShot(FMODEvents._fmodEvents.gameOver, this.transform.position);
        EventSystem.current.SetSelectedGameObject(reiniciarButton.gameObject);
        reiniciar.SetActive(true);
        Time.timeScale = 0; // Pausa o jogo
    }

    private void UpdateLife(int life)
    {
        vida.text = life.ToString();
    }

    public void Comecar()
    {
        Time.timeScale = 1; // Reinicia o tempo do jogo
        SceneManager.LoadScene("Fase");
    }
    public void ComecarDoMenu()
    {
        StartCoroutine(RotinaComecarDoMenu());
    }
    public IEnumerator RotinaComecarDoMenu()
    {
        levelLoaderObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1; // Reinicia o tempo do jogo
        SceneManager.LoadScene("Fase");
    }
    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Creditos()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void Voltar()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void Sair()
    {
        
        Application.Quit();
    }


    public void Pause()
    {
        if (isPaused)
        {
            isPaused = false;
            pause.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            EventSystem.current.SetSelectedGameObject(pauseButton.gameObject);
            pause.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void SetMenu(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Pause();
        }
    }

    
}
