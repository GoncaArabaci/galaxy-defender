using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    private Vector2 speed;

    public void SetSpeed(Vector2 newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        // Mermiyi hareket ettirme
        transform.Translate(speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        // Ekrandan çýkýnca mermiyi yok et
        Destroy(gameObject);
    }
}

