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
    public BossController boss;
    public Transform alienSpawnpoint;
    public float spawnBossDelay = 20;
    float spawnBossDelayCooldown;
    public Transform bossSpawnpoint;

    bool shouldSpawnEnemy = true;
    void Start()
    {
        gameOverText.enabled = false;
        scoreText.text = "Score: " + score;
        spawnDelayCooldown = spawnDelay;
        spawnBossDelayCooldown = spawnBossDelay;
    }


    void Update()
    {
        if (spawnDelayCooldown <= 0 && shouldSpawnEnemy)
        {
            Spawn();
            spawnDelayCooldown = spawnDelay;
        }
        spawnDelayCooldown -= Time.deltaTime;
         if (spawnBossDelayCooldown <= 0 && shouldSpawnEnemy)
        {
            SpawnBoss();
            spawnBossDelayCooldown = spawnBossDelay;
        }
        spawnBossDelayCooldown -= Time.deltaTime;
    }
    void OnEnable()
    {
        EventBus.StartListening("PlayerDied", OnPlayerDied);
        EventBus.StartListening("AlienDied", OnAlienDied);
        EventBus.StartListening("BossDied", OnBossDied);
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
    void OnBossDied()
    {
        score += 50;
        scoreText.text = "Score: " + score;
    }
    void OnDisable()
    {
        EventBus.StopListening("PlayerDied", OnPlayerDied);
        EventBus.StopListening("AlienDied", OnAlienDied);
        EventBus.StopListening("BossDied", OnBossDied);
    }
    void Spawn()
    {
        int index = Random.Range(0, aliens.Length);
        Transform alienPrefab = aliens[index];
        Vector3 position = alienSpawnpoint.position;
        Instantiate(alienPrefab, position, Quaternion.identity);
    }
    void SpawnBoss()
    {
        BossController controller = Instantiate<BossController>(boss);
        controller.SetBossDestination(bossSpawnpoint);
    }
}
