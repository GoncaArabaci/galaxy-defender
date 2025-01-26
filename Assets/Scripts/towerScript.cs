using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    public float range = 5f; // Kule menzili
    public float fireRate = 1f; // Atýþ hýzý
    public GameObject bulletPrefab; // Mermi prefab'i
    public Transform firePoint; // Merminin çýkýþ noktasý

    public int health = 100; // Kulenin toplam caný

    private Transform target; // Hedef düþman
    private float fireCountdown = 0f;

    void Update()
    {
        UpdateTarget();

        if (target == null)
            return;

        // Ateþ etme iþlemleri
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Kulenin canýný azalt
        Debug.Log("Tower Health: " + health);

        if (health <= 0)
        {
            Die(); // Can sýfýrsa kule yok olur
        }
    }

    void Die()
    {
        Debug.Log("Tower destroyed!");
        Destroy(gameObject); // Kulenin yok edilmesi
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");

    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }

    // Çarpýþma Algýlama
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC")) // Eðer çarpan obje NPC ise
        {
            health += 10; // Kulenin canýný artýr
            Debug.Log("Tower health increased: " + health);
            Destroy(collision.gameObject); // Çarpan NPC'yi yok et
        }
    }
}
