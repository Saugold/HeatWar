using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController _gameController;

    public delegate void EnemiesDie(int deads);
    public static event EnemiesDie OnEnemiesDies;

    public int deads;

    void Start()
    {
        _gameController = this;
        deads = 0;
        EnemyLife.OnEnemyDied += CountDead;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reiniciar();
        }
    }

    public void Reiniciar()
    {
        deads = 0;
        Debug.Log("Reiniciado");
    }

    private void CountDead()
    {
        deads++;
        Debug.Log("Deads: " + deads);

        if (OnEnemiesDies != null)
        {
            OnEnemiesDies(deads);
        }
    }
}
