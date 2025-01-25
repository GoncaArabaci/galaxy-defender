using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can deðeri
    private int currentHealth;  // Mevcut can deðeri

    public Slider healthSlider; // Can göstergesi için Slider (UI)

    void Start()
    {
        currentHealth = maxHealth; // Oyunun baþýnda can maksimumda
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; // Slider'ýn maksimum deðerini ayarla
            healthSlider.value = currentHealth; // Baþlangýçta mevcut caný göster
        }
    }

    // Hasar alma fonksiyonu
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasar alýndýðýnda mevcut can azalýr

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Slider güncelle
        }

        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Can sýfýra düþerse ölüm gerçekleþir
        }
    }

    // Can yenileme fonksiyonu
    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Maksimum caný aþamaz
        }

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Slider güncelle
        }

        Debug.Log("enemy Healed: " + currentHealth);
    }

    // Ölüm fonksiyonu
    void Die()
    {
        Debug.Log("enemy Died!");
        // Burada karakteri devre dýþý býrakabilir veya ölüm animasyonu oynatabilirsiniz
        gameObject.SetActive(false); // Örneðin, karakteri devre dýþý býrakýr
    }
}
