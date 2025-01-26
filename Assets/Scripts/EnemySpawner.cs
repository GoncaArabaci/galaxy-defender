using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Spawn edilecek d��man prefab�
    public Transform spawnPoint; // D��man�n spawn olaca�� pozisyon
    public int numberOfEnemies = 5; // Spawn edilecek d��man say�s�
    public float spawnDelay = 1f; // Her bir d��man spawn� aras�ndaki s�re

    private int spawnedCount = 0; // Spawn edilen d��man say�s�

    void Start()
    {
        // Spawn i�lemine ba�la
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnedCount < numberOfEnemies)
        {
            SpawnEnemy();
            spawnedCount++;
            yield return new WaitForSeconds(spawnDelay); // Delay bekle
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
