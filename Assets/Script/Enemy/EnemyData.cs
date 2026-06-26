using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Add Enemy")]
public class EnemyData : ScriptableObject
{
    public string displayName;
    public int maxHealth;
    public int baseSpeed;
    public int baseAttack;
    public GameObject prefab;
}
