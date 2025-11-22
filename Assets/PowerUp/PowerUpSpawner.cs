using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Grid Components")]
    public float distance_box = 2.5f;   // Jarak antar column
    public float spawnHeight = 6.5f;    // Tinggi spawn

    [Header("Spawn Settings")]
    // Jeda waktu spawn
    public float spawnInterval = 3f;    
    private float timer = 0;

    [System.Serializable]
    public class PowerUpData
    {
        public string name;
        public GameObject prefab;
        [Range(0, 100)]
        public float spawnChance = 50f;
        public float fallSpeed = 4f;
    }

    [Header("List Power Ups")]
    public PowerUpData[] powerUpList;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            TrySpawnPowerUp();
            timer = 0;
        }
    }

    void TrySpawnPowerUp()
    {
        // Pilih Random satu Power Up dari list
        if (powerUpList.Length == 0) return;

        int randomIndex = Random.Range(0, powerUpList.Length);
        PowerUpData selectedPowerUp = powerUpList[randomIndex];

        if (selectedPowerUp.prefab == null)
        {
            return;
        }

        float roll = Random.Range(0f, 100f);

        if (roll <= selectedPowerUp.spawnChance)
        {
            SpawnObject(selectedPowerUp);
        }
    }

    void SpawnObject(PowerUpData data)
    {
        // Posisi Jalur
        int laneIndex = Random.Range(0, 3);

        float xPos = (laneIndex - 1) * distance_box;

        Vector3 spawnPos = new Vector3(xPos, spawnHeight, -1);

        // Munculkan Barang
        GameObject obj = Instantiate(data.prefab, spawnPos, Quaternion.identity);

        // Kecepatan jatuh
        PowerUp powerUpScript = obj.GetComponent<PowerUp>();
        if (powerUpScript != null)
        {
            powerUpScript.speed = data.fallSpeed;
        }
    }
}