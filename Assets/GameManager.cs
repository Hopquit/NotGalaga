using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TMP_Text gameOverText;
    public float spawnDelay = 3;
    float spawnDelayCooldown;
    public Transform[] aliens;
    public TMP_Text scoreText;
    int score;
    public Transform alienSpawnpoint;

    bool shouldSpawnEnemy = true;
    void Start()
    {
        gameOverText.enabled = false;
        scoreText.text = "Score: " + score;
    }


    void Update()
    {
        if (spawnDelayCooldown <= 0 && shouldSpawnEnemy)
        {
            Spawn();
            spawnDelayCooldown = spawnDelay;
        }
        spawnDelayCooldown -= Time.deltaTime;
    }
    void OnEnable()
    {
        EventBus.StartListening("PlayerDied", OnPlayerDied);
        EventBus.StartListening("AlienDied", OnAlienDied);
    }
    void OnPlayerDied()
    {
        gameOverText.enabled = true;
        shouldSpawnEnemy = false;
    }
    void OnAlienDied()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }
    void OnDisable()
    {
        EventBus.StopListening("PlayerDied", OnPlayerDied);
        EventBus.StopListening("AlienDied", OnAlienDied);
    }
    void Spawn()
    {
        int index = Random.Range(0, aliens.Length);
        Transform alienPrefab = aliens[index];
        Vector3 position = alienSpawnpoint.position;
        Instantiate(alienPrefab, position, Quaternion.identity);
    }
}
