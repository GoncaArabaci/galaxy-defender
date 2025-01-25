using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // Mermi hýzý
    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Hedefe doðru hareket et
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // Hedefe hasar ver
        Destroy(gameObject);
    }
}
