using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public TurnHandler TurnHandler = new();
    public EnemyManager EnemyManager;
    public List<CharBattleHandler> Characters;
    public CharBattleHandler ActiveCharacter;
    public EnemyBattleManager ActiveEnemy;
    
    
    private void Awake()
    {
        Instance = this;
        EnemyManager = GetComponentInChildren<EnemyManager>();
    }

    private void OnEnable()
    {
        StartCoroutine(BattleStartDelay());
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private IEnumerator BattleStartDelay()
    {
        var t = 0f;
        while (t < 3)
        {
            t += Time.deltaTime;
            yield return null;
        }
        
        TurnHandler.SortOrder();
        TurnHandler.NextTurn();
    }
}
