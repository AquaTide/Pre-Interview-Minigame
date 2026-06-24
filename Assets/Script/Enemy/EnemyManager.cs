using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyData> enemyList;
    public EnemyBattleManager[] spawnedEnemies;
    public int ActiveEnemy = 0;

    private void OnEnable()
    {
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        enemyList = EncounterEvent.Instance.encounter.Enemies;
        SetEnemiesToPosition();
    }

    private void SetEnemiesToPosition()
    {
        var count = enemyList.Count;
        var spotFormation = transform.GetChild(count - 1);
        
        spawnedEnemies = spotFormation.GetComponentsInChildren<EnemyBattleManager>();
        
        var i = 0;
        foreach (var e in spawnedEnemies)
        {
            e.enemyData = enemyList[i];
            i++;
        }
        
        spotFormation.gameObject.SetActive(true);
    }

    public void EnableTargetingOnEnemies()
    {
        foreach (var e in spawnedEnemies)
        {
            e.OnEnableTargeting();
        }

        spawnedEnemies[0].OnTargeted();
    }
}