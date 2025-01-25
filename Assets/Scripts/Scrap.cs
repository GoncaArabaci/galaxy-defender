using UnityEngine;

public class Scrap : MonoBehaviour
{
    public float interactionDistance = 0.5f; // Oyuncunun yakýnlýk mesafesi
    private Transform player; // Oyuncunun transform'u

    void Start()
    {
        // Oyuncuyu sahnede "Player" tag'i ile buluyoruz
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player bulunamadý! Oyuncu sahnede 'Player' tag'i ile olmalý.");
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
        // Scrap toplandýðýnda ne olacaðýný buraya yaz
        Debug.Log("Scrap collected by player!");
        Destroy(gameObject); // Scrap objesini yok et
    }

    // Etkileþim mesafesini sahnede göstermek için (isteðe baðlý)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
