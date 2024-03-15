using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemiesManager enemiesManager;
    StageTime stageTime;

    PlayerWinManager playerWinManager;

    int eventIndexer;
    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    private void Start()
    {
        playerWinManager = FindObjectOfType<PlayerWinManager>();
    }

    private void Update()
    {
        if (eventIndexer >= stageData.stageEvents.Count)
        {
            return;
        }


        if (stageTime.time > stageData.stageEvents[eventIndexer].time)
        {
            switch (stageData.stageEvents[eventIndexer].eventType)
            {
                case StageEventType.SpawnEnemy:
                    SpawnEnemy(false);
                    break;

                case StageEventType.SpawnObject:
                    SpawnObject();
                    break;

                case StageEventType.WinStage:
                    WinStage();
                    break;
                case StageEventType.SpawnEnemyBoss:
                    SpawnEnemyBoss();
                    break;
            }
            Debug.Log(stageData.stageEvents[eventIndexer].message);
            eventIndexer++;
        }
    }

    private void SpawnEnemyBoss()
    {
        SpawnEnemy(true);
    }
    private void WinStage()
    {
        playerWinManager.Win();
    }

    private void SpawnEnemy(bool bossEnemy)
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].Count; i++)
            {
                enemiesManager.SpawnEnemy(stageData.stageEvents[eventIndexer].enemyToSpawn, bossEnemy);
            }
    }


    private void SpawnObject()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].Count; i++)
            {
                Vector3 positionToSpawn = GameManager.instance.playerTransform.position;
                positionToSpawn += UtilityTools.GenerateRandomPositionSquarePattern(new Vector2(5f, 5f));
                SpawnManager.instance.SpawnObject(positionToSpawn, stageData.stageEvents[eventIndexer].objectToSpawn);
            }
        
    }

}
