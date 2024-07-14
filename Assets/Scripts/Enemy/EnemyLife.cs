using FMODUnity;
using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float maxLife;
    private float currentLife;
    private bool isDead = false; // Variável para verificar se o inimigo já morreu

    public delegate void EnemyDeath();
    public static event EnemyDeath OnEnemyDied;
    [SerializeField] public EventReference sfxDanoEnemy;
    [SerializeField] public EventReference sfxMorrerEnemy;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        currentLife = maxLife;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (currentLife <= 0 && !isDead)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        // Dispara o evento de morte do inimigo
        if (OnEnemyDied != null)
        {
            OnEnemyDied();

        }
        isDead = true; // Marca o inimigo como morto para evitar chamadas múltiplas
        AudioManager._audioManager.PlayOneShot(sfxMorrerEnemy, transform.position);
        Debug.Log("Morreu");

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shoot"))
        {
            TomarDano(1, collision);
        }
        else if (collision.CompareTag("SpecialShoot"))
        {
            TomarDano(4, collision);
        }
    }

    private void TomarDano(int dano, Collider2D collision)
    {
        AudioManager._audioManager.PlayOneShot(sfxDanoEnemy, transform.position);
        currentLife -= dano;
        Debug.Log(currentLife);

        // Aplica uma força de recuo ao inimigo
        Vector2 recoilDirection = transform.position - collision.transform.position;
        rb.AddForce(recoilDirection.normalized * 5f, ForceMode2D.Impulse);

        // Inicia a corrotina para fazer o inimigo piscar em vermelho
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        // Salva a cor original do inimigo
        Color originalColor = spriteRenderer.color;

        // Muda a cor para vermelho
        spriteRenderer.color = Color.red;

        // Aguarda um curto período de tempo
        yield return new WaitForSeconds(0.1f);

        // Restaura a cor original
        spriteRenderer.color = originalColor;
    }
}
