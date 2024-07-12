using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _audioManager;
    private List<EventInstance> eventInstances;
    private EventInstance musicEventInstance;
    private string currentMusicEvent;

    private void Awake()
    {
        if (_audioManager != null)
        {
            Debug.Log("Já existe mais de um AudioManager na cena");
            Destroy(gameObject); // Garantir que apenas um AudioManager exista
            return;
        }
        _audioManager = this;
        eventInstances = new List<EventInstance>();
        DontDestroyOnLoad(gameObject); // Persistir o AudioManager entre as cenas
    }

    private void Start()
    {
        PlayMusicForCurrentScene();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene();
    }

    private void PlayMusicForCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Menu")
        {
            StopCurrentMusic();
            InitializeMusic(FMODEvents._fmodEvents.musicTema);
        }
        else if (currentScene == "Fase")
        {
            StopCurrentMusic();
            InitializeMusic(FMODEvents._fmodEvents.musicGameplay);
        }
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void InitializeMusic(EventReference musicEventReference)
    {
        StopCurrentMusic();

        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
        currentMusicEvent = musicEventReference.Guid.ToString();
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public void StopCurrentMusic()
    {
        if (musicEventInstance.isValid())
        {
            musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicEventInstance.release();
            currentMusicEvent = null;
        }
    }

    public void StopAllAudioEvents()
    {
        foreach (EventInstance instance in eventInstances)
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }
        eventInstances.Clear();
    }
}
