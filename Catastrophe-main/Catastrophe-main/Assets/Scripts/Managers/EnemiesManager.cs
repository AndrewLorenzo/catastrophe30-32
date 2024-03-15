using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemiesManager : MonoBehaviour
{
    [SerializeField] StagePorgress stageProgress;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemyAnimation;
    [SerializeField] Vector2 spawnArea;

    [SerializeField] float spawnTimer;
    GameObject player;

    List<Enemy> bossEnemiesList;
    int totalBossHealth;
    int currentBossHealth;
    [SerializeField] Slider bossHealthBar;
    private void Start()
    {
        //gameObject atau GameObject
        player = GameManager.instance.playerTransform.gameObject;
        bossHealthBar = FindObjectOfType<BossHPBar>(true).GetComponent<Slider>();
    }

    private void Update()
    {
        UpdateBossHealth();
    }

    private void UpdateBossHealth()
    {
        if(bossEnemiesList == null) { return; }
        if(bossEnemiesList.Count == 0) { return; }

        currentBossHealth = 0;

        for (int i=0; i<bossEnemiesList.Count; i++)
        {
            if(bossEnemiesList[i] == null) { continue; }

            currentBossHealth += bossEnemiesList[i].stats.hp;
        }

        bossHealthBar.value = currentBossHealth;
        if(currentBossHealth <= 0) 
        {
            bossHealthBar.gameObject.SetActive(false);
            bossEnemiesList.Clear();
        }
    }

    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)
    {
        Vector3 position = UtilityTools.GenerateRandomPositionSquarePattern(spawnArea);

        position += player.transform.position;

        //spawning main enemy object
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;


        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.SetStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);

        if(isBoss==true)
        {
            SpawnBossEnemy(newEnemyComponent);
        }

        newEnemy.transform.parent = transform;

        // spawning animation object
        GameObject spriteObject = Instantiate(enemyToSpawn.animatedPrefab);
        spriteObject.transform.parent = newEnemy.transform;
        spriteObject.transform.localPosition = Vector3.zero;

    }

    private void SpawnBossEnemy(Enemy newBoss)
    {
        if (bossEnemiesList == null) { bossEnemiesList = new List<Enemy>(); }

        bossEnemiesList.Add(newBoss);

        totalBossHealth += newBoss.stats.hp;

        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = totalBossHealth;
    }

}
