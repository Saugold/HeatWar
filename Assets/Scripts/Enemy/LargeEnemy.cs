using System.Collections;
using UnityEngine;

public class LargeEnemy : MonoBehaviour, IEnemy
{
    private Collider2D enemyCol;
    private Rigidbody2D enemyRb;
    private bool canAttack;
    private bool isMovingTowardPlayer = true; // Flag para controlar se está se movendo em direção ao jogador
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDistance;
    private Transform playerTransform;
    Vector2 direction;

    [field: Header("TiroConfig")]
    [SerializeField] private GameObject projectilePrefab; // Prefab do projetil
    [SerializeField] private Transform firePoint; // Ponto de origem do disparo
    [SerializeField] private float fireRate = 2f; // Taxa de disparo em segundos
    [SerializeField] private float projectileSpeed = 10f; // Velocidade do projetil

    private bool canShoot = true;
    private float timeSincePlayerInSight = 0f;
    private float timeToStartFleeing = 5f; // Tempo em segundos para começar a fugir
    private float fleeDuration = 5f; // Duração da fuga em segundos
    private float timeSinceLastFlee = 0f; // Tempo decorrido desde a última fuga

    void Start()
    {
        enemyCol = GetComponent<BoxCollider2D>();
        enemyRb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Atribui o playerTransform aqui
    }

    void Update()
    {
        if (playerTransform == null) return; // Garante que playerTransform não seja nulo

        if (isMovingTowardPlayer)
        {
            timeSincePlayerInSight += Time.deltaTime;
            if (timeSincePlayerInSight >= timeToStartFleeing)
            {
                isMovingTowardPlayer = false;
                timeSinceLastFlee = 0f; // Reinicia o contador de tempo desde a última fuga
            }
        }
        else
        {
            timeSinceLastFlee += Time.deltaTime;
            if (timeSinceLastFlee >= fleeDuration)
            {
                moveSpeed = 3f;
                isMovingTowardPlayer = true; // Volta a se mover em direção ao jogador
                timeSincePlayerInSight = 0f; // Reinicia o contador de tempo desde que o jogador foi avistado
            }
            else
            {
                SairTela(); // Método para mover na direção oposta do jogador
            }
        }

        Comportamento();
    }

    public void Atacar()
    {
        StartCoroutine(ShootRoutine());
    }

    public void Comportamento()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= attackDistance)
            {
                enemyRb.velocity = Vector2.zero;
                canAttack = true;
                if (canAttack)
                {
                    Atacar();
                }
            }
            else
            {
                // Move o inimigo em direção ao jogador
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                enemyRb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
            }

            // Calcula a direção do jogador em relação ao inimigo
            Vector2 playerdirection = (playerTransform.position - transform.position).normalized;

            // Rotaciona o sprite para olhar para o jogador
            float angle = Mathf.Atan2(playerdirection.y, playerdirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shoot"))
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Debug.Log("Morreu");
        Destroy(this.gameObject);
    }

    IEnumerator ShootRoutine()
    {
        if (canAttack)
        {
            if (canShoot)
            {
                Atirar(); // Dispara o projetil
                canShoot = false; // Impede novos disparos até o próximo intervalo
                yield return new WaitForSeconds(1f / fireRate); // Espera o intervalo de disparo
                canShoot = true; // Permite novos disparos após o intervalo
            }
            yield return null;
        }
    }

    void Atirar()
    {
        Vector2 playerdirection = (playerTransform.position - transform.position).normalized;

        // Calcula o ângulo de rotação do projetil
        float angle = Mathf.Atan2(playerdirection.y, playerdirection.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rb1 = projectile.GetComponent<Rigidbody2D>();
        if (rb1 != null)
        {
            rb1.velocity = playerdirection * projectileSpeed;
        }
    }

    void SairTela()
    {
        moveSpeed = 10f;
        // Move na direção oposta ao jogador
        Vector2 directionAwayFromPlayer = -(playerTransform.position - transform.position).normalized;
        enemyRb.MovePosition((Vector2)transform.position + (directionAwayFromPlayer * moveSpeed * Time.deltaTime));
    }
}
