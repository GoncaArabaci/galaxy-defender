using UnityEngine;

public class NPCFallow : MonoBehaviour
{
    private Transform player; // Oyuncunun Transform bileþeni
    public float speed = 5f; // Takip hýzý
    public float minimumDistanceToPlayer = 1.5f; // Oyuncuyla NPC arasýndaki minimum mesafe
    public float minimumDistanceToOtherNPCs = 1f; // Diðer NPC'lerle olan minimum mesafe
    public float avoidSpeed = 3f; // Diðer NPC'lerden uzaklaþma hýzý

    void Start()
    {
        // Player objesini sahnede otomatik olarak bul
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player objesi bulunamadý! Sahnedeki Player objesine 'Player' etiketi ekleyin.");
        }
    }

    void Update()
    {
        // Eðer player atanmýþsa takip et
        if (player != null)
        {
            // Oyuncu ile NPC arasýndaki mesafeyi hesapla
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            // Eðer mesafe minimum mesafeden büyükse hareket et
            if (distanceToPlayer > minimumDistanceToPlayer)
            {
                // Diðer NPC'lere olan mesafeyi kontrol et ve hareket et
                if (IsTooCloseToOtherNPCs(out Vector3 avoidDirection))
                {
                    // Eðer baþka bir NPC'ye çok yakýnsa uzaklaþ
                    transform.position += avoidDirection * avoidSpeed * Time.deltaTime;
                }
                else
                {
                    // Oyuncuya doðru hareket et
                    Vector3 directionToPlayer = (player.position - transform.position).normalized;
                    transform.position += directionToPlayer * speed * Time.deltaTime;
                }
            }
        }
    }

    // Diðer NPC'lere çok yakýn olup olmadýðýný kontrol eder
    bool IsTooCloseToOtherNPCs(out Vector3 avoidDirection)
    {
        avoidDirection = Vector3.zero; // Uzaklaþma yönü
        GameObject[] allNPCs = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject npc in allNPCs)
        {
            if (npc != this.gameObject) // Kendini kontrol etme
            {
                float distanceToNPC = Vector3.Distance(npc.transform.position, transform.position);

                if (distanceToNPC < minimumDistanceToOtherNPCs)
                {
                    // Uzaklaþma yönünü belirle
                    avoidDirection += (transform.position - npc.transform.position).normalized;
                }
            }
        }

        // Eðer uzaklaþma yönü belirlenmiþse, normalize et
        if (avoidDirection != Vector3.zero)
        {
            avoidDirection = avoidDirection.normalized;
            return true;
        }

        return false;
    }
}
