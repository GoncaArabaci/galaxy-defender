using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; // Düþman caný
    public float speed = 2f; // Düþman hareket hýzý
    public int damage = 10; // Kulenin alacaðý hasar miktarý
    public float attackRate = 1f; // Saniyede bir kez saldýrý
    private float attackCountdown = 0f; // Saldýrý için zamanlayýcý

    private bool isAttacking = false; // Düþman kuleye ulaþtý mý?

    private Transform tower; // Kule referansý

    void Start()
    {
        // Kulenin referansýný bul ve hedef olarak ayarla
        tower = GameObject.FindGameObjectWithTag("Tower").transform;
    }

    void Update()
    {
        if (!isAttacking)
        {
            MoveTowardsTower(); // Eðer kuleye ulaþmadýysa hareket et
        }
        else
        {
            AttackTower(); // Eðer ulaþtýysa kuleye saldýr
        }
    }

    // Düþmanýn kuleye doðru hareket etmesini saðlar
    void MoveTowardsTower()
    {
        if (tower == null) return;

        // Kuleye doðru hareket et
        Vector2 direction = (tower.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // Düþmanýn kuleye saldýrmasýný saðlar
    void AttackTower()
    {
        if (attackCountdown <= 0f)
        {
            // Kulenin canýný azalt
            Tower towerScript = tower.GetComponent<Tower>();
            if (towerScript != null)
            {
                towerScript.TakeDamage(damage); // Kulenin canýný azalt
            }

            attackCountdown = 1f / attackRate; // Yeni saldýrý için zamanlayýcýyý sýfýrla
        }

        attackCountdown -= Time.deltaTime; // Zamanlayýcýyý güncelle
    }

    // Kuleye ulaþtýðýnda çalýþýr
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            isAttacking = true; // Kuleye ulaþýldý
        }
    }

    // Kuleden ayrýldýðýnda çalýþýr (opsiyonel, gerekirse)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            isAttacking = false; // Kuleden uzaklaþýrsa tekrar hareket edebilir
        }
    }

    // Düþmanýn hasar almasýný saðlar
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    // Düþman öldüðünde çalýþýr
    void Die()
    {
        Destroy(gameObject); // Düþmaný sahneden kaldýr
    }
}
