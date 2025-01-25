using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // Farklý mermi prefab'larý
    public Transform firePoint; // Merminin çýkýþ noktasý
    public int maxBullets = 10; // Maksimum mermi sayýsý
    public float bulletSpeed = 20f; // Merminin hýzý

    private int currentBullets;
    private int selectedBulletIndex = 0; // Seçili mermi türü
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale; // Karakterin orijinal ölçeði

    void Start()
    {
        currentBullets = maxBullets; // Baþlangýçta maksimum mermi sayýsý
        spriteRenderer = GetComponent<SpriteRenderer>(); // Karakterin SpriteRenderer bileþeni
        originalScale = transform.localScale; // Orijinal ölçeði sakla
    }

    void Update()
    {
        // Sol týklama ile ateþ et
        if (Input.GetMouseButtonDown(0) && currentBullets > 0)
        {
            Shoot();
        }

        // Mermi türünü deðiþtirme (örneðin, Q tuþuyla)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleBulletType();
        }

        // FirePoint'i Mouse'un baktýðý yöne çevir
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Z eksenini sýfýrla (2D için)
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Karakteri mouse yönüne göre flip yap
        if (mousePosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Karakteri sola çevir
            firePoint.right = -direction; // FirePoint yönünü ters çevir
        }
        else
        {
            transform.localScale = originalScale; // Karakteri saða çevir
            firePoint.right = direction; // FirePoint yönünü doðru ayarla
        }
    }

    void Shoot()
    {
        // Seçili mermi prefab'ýný oluþtur
        GameObject bullet = Instantiate(bulletPrefabs[selectedBulletIndex], firePoint.position, firePoint.rotation);
        BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
        if (bulletMovement != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Z eksenini sýfýrla (2D için)
            Vector2 direction = (mousePosition - transform.position).normalized;

            if (transform.localScale.x < 0) // Karakter sola dönükse yönü ters çevir
            {
                direction.x = -direction.x;
            }

            bulletMovement.SetSpeed(direction * bulletSpeed); // Mermiyi mouse'un yönüne doðru hareket ettir
        }

        currentBullets--; // Mermi sayýsýný azalt
    }

    void CycleBulletType()
    {
        selectedBulletIndex = (selectedBulletIndex + 1) % bulletPrefabs.Length; // Mermi türünü sýrayla deðiþtir
    }

    public void Reload(int amount)
    {
        currentBullets = Mathf.Clamp(currentBullets + amount, 0, maxBullets); // Mermiyi yenile
    }
}











