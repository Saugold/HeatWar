using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animatorTrans;
    public GameObject player;
    [SerializeField] private MonoBehaviour playerControlScript;
    public void Transition(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    public IEnumerator LoadScene(string sceneName)
    {
        animatorTrans.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }
    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Fase")
        {
            StartCoroutine(CarregarFase());
            animatorTrans.SetTrigger("Start");
        }
       
    }

    public IEnumerator CarregarFase()
    {
        if (player != null)
        {
            
            player.GetComponent<SpriteRenderer>().enabled = false;

            
            playerControlScript.enabled = false;
        }

        
        yield return new WaitForSeconds(2f);

        if (player != null)
        {
            
            player.GetComponent<SpriteRenderer>().enabled = true;

            
            playerControlScript.enabled = true;
        }


    }
}
