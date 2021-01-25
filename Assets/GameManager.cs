using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;
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
    public Transform hitSound;
    bool shouldSpawnEnemy = true;
    public LootTableScriptableObject loot;
    void Start()
    {
        gameOverText.SetActive(false);
        scoreText.text = "Score: " + score;
        spawnDelayCooldown = spawnDelay;
        spawnBossDelayCooldown = spawnBossDelay;
        hitSound.GetComponent<AudioSource>().volume *= PlayerPrefs.GetFloat("volume", 0.5f);
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
        EventBus.StartListening("PlayerHit", OnPlayerHit);
        var volume = PlayerPrefs.GetFloat("volume", 0.5f);
        PlayerPrefs.SetFloat("volume", volume);
    }
    void OnPlayerDied(Transform data)
    {
        gameOverText.SetActive(true);
        shouldSpawnEnemy = false;
    }
    void OnPlayerHit(Transform data)
    {
        Instantiate (hitSound);
    }
    void OnAlienDied(Transform data)
    {
        score += 1;
        scoreText.text = "Score: " + score;
        var drop = loot.RandomDrop();
        if (drop != null)
        {
            Instantiate (drop, data.position, Quaternion.identity);
        }
    }
    void OnBossDied(Transform data)
    {
        score += 50;
        scoreText.text = "Score: " + score;
    }
    void OnDisable()
    {
        EventBus.StopListening("PlayerDied", OnPlayerDied);
        EventBus.StopListening("AlienDied", OnAlienDied);
        EventBus.StopListening("BossDied", OnBossDied);
        EventBus.StopListening("PlayerHit", OnPlayerHit);
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
    public void ChangeScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    } 
}
