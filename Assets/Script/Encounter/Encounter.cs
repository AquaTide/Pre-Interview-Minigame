using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Encounter Data", menuName = "Encounter/Add New Encounter")]
public class Encounter : ScriptableObject
{
    public bool isRandomEnemies;
    public List<EnemyData> possibleEnemies;
    [Range(1, 4)] public int minEnemiesCount,maxEnemiesCount;

    public List<EnemyData> Enemies { get; private set; } = new();
    public int EnemiesCount { get; private set; }

    private void RandomEnemiesCount()
    {
        EnemiesCount = Random.Range(minEnemiesCount, maxEnemiesCount);
    }
    
    private void RandomEnemies()
    {
        Enemies.Clear();
        
        for (int i = 0; i < EnemiesCount; i++)
        {
            var rdm = Random.Range(0, possibleEnemies.Count);
            Enemies.Add(possibleEnemies[rdm]);
        }
    }

    public void RandomEncounter()
    {
        RandomEnemiesCount();
        RandomEnemies();
    }

    public void SetEncounter()
    {
        Enemies = possibleEnemies;
        EnemiesCount = Enemies.Count;
    }
}
