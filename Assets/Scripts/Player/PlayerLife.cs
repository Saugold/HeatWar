using System.Collections;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDied;

    public delegate void PlayerDamage(int life);
    public static event PlayerDamage OnPlayerDamage;

    [SerializeField] private int maxLife;
    [SerializeField] private int life;
    [SerializeField] private float invulnerabilityDuration = 1f; // Duração da invulnerabilidade em segundos
    [SerializeField] private float blinkInterval = 0.1f; // Intervalo entre as piscadas
    [SerializeField] private Sprite defaultSprite; // Sprite padrão
    [SerializeField] private Sprite blinkSprite; // Sprite de piscar

    private SpriteRenderer spriteRenderer;
    private bool isInvulnerable = false;

    private void Start()
    {
        life = maxLife;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (collision.gameObject.CompareTag("Damage") && !isInvulnerable)
        {
            life--;
            AudioManager._audioManager.PlayOneShot(FMODEvents._fmodEvents.sfxDamage, this.transform.position);
            TomarDano();
            Debug.Log(life);
            StartCoroutine(BlinkAndInvulnerability());
        }
    }

    private void TomarDano()
    {
        if (OnPlayerDamage != null)
        {
            OnPlayerDamage(life);
        }
    }

    private IEnumerator BlinkAndInvulnerability()
    {
        isInvulnerable = true;
        float elapsedTime = 0f;

        while (elapsedTime < invulnerabilityDuration)
        {
            spriteRenderer.sprite = spriteRenderer.sprite == defaultSprite ? blinkSprite : defaultSprite;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        spriteRenderer.sprite = defaultSprite; // Certifique-se de que o sprite está de volta ao padrão no final
        isInvulnerable = false;
    }
}
