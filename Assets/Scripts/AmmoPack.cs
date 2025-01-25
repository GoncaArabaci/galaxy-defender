using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public float interactionDistance = 0.5f; // Oyuncunun yakýnlýk mesafesi
    private Transform player; // Oyuncunun transform'u
    private PlayerShooting playerShooting;

    void Start()
    {
        // Oyuncuyu sahnede "Player" tag'i ile buluyoruz
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject == null)
        {
            Debug.LogError("Player bulunamadý! Oyuncu sahnede 'Player' tag'i ile olmalý.");
            return;
        }

        player = playerObject.transform;
        playerShooting = playerObject.GetComponent<PlayerShooting>();

        if (playerShooting == null)
        {
            Debug.LogError("PlayerShooting scripti bulunamadý! Player objesine eklenmiþ olmalý.");
        }
    }

    void Update()
    {
        // Oyuncu belli bir mesafeye gelmiþse ve "E" tuþuna basýlmýþsa
        if (player != null && Vector2.Distance(transform.position, player.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collect();
            }
        }
    }

    public void Collect()
    {
        if (playerShooting != null)
        {
            Debug.Log("AmmoPack collected by player!");
            playerShooting.currentBullets += 10; // Mermiyi artýr
            Destroy(gameObject); // AmmoPack objesini yok et
        }
    }
}
