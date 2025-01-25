using UnityEngine;
using System.Collections;

public class NPCInteraction : MonoBehaviour

{
    public Transform interactableObject; // Belirlenen obje
    public GameObject prefabToSpawn; // Oluþturulacak prefab
    public float interactionRange = 2f; // Eriþim mesafesi
    public float holdTime = 2f; // "E" tuþuna basýlý tutma süresi

    private bool isPlayerNearby = false;
    private float holdTimer = 0f;

    void Update()
    {
        // Oyuncunun mesafesini kontrol et
        if (Vector3.Distance(transform.position, interactableObject.position) <= interactionRange)
        {
            isPlayerNearby = true;
        }
        else
        {
            isPlayerNearby = false;
            holdTimer = 0f; // Mesafeden çýkarsa süreyi sýfýrla
        }

        // "E" tuþuna basýlý tutulduðunda
        if (isPlayerNearby && Input.GetKey(KeyCode.E))
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTime)
            {
                SpawnPrefab();
                holdTimer = 0f; // Timer'ý sýfýrla
            }
        }

        // "E" tuþu býrakýlýrsa süreyi sýfýrla
        if (Input.GetKeyUp(KeyCode.E))
        {
            holdTimer = 0f;
        }
    }

    void SpawnPrefab()
    {
        Vector3 spawnPosition = interactableObject.position + new Vector3(1f, 0f, 0f); // Objeye yakýn bir pozisyon
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        Debug.Log("Prefab oluþturuldu!");

        // Oyuncunun kontrolünün etkilenmemesi için kontrolleri burada sýfýrlýyoruz
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}




