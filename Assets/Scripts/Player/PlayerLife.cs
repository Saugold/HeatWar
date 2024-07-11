using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDied;

    public delegate void PlayerDamage(int life);
    public static event PlayerDamage OnPlayerDamage;

    [SerializeField] private int maxLife;
    [SerializeField] private int life;

    private void Start()
    {
        life = maxLife;
    }

    private void Update()
    {
        if (life <= 0)
        {
            Debug.Log("Morreu");
            if (OnPlayerDied != null)
            {
                OnPlayerDied();
            }
            Destroy(gameObject); // Destroi o objeto do jogador ao morrer
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            life--;
            TomarDano();
            Debug.Log(life);
        }
    }

    private void TomarDano()
    {
        if (OnPlayerDamage != null)
        {
            OnPlayerDamage(life);
        }
    }
}
