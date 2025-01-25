using UnityEngine;

public class NPCFallow : MonoBehaviour
{
    public Transform Player; // Takip edilecek oyuncu
    public float followSpeed = 5f; // Takip hýzýný ayarla
    public float followDistance = 2f; // Oyuncudan uzaklýðý koruma mesafesi

    private Vector3 offset; // Takipçi objenin baþlangýçtaki ofseti

    void Start()
    {
        // Oyuncuya olan baþlangýçtaki mesafeyi sakla
        offset = transform.position - Player.position;
    }

    void Update()
    {
        // Hedef pozisyon, oyuncunun pozisyonu ve ofset ile hesaplanýr
        Vector3 targetPosition = Player.position + offset;

        // Eðer oyuncu ile takipçi arasýnda mesafe fazla ise hareket et
        if (Vector3.Distance(transform.position, Player.position) > followDistance)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
