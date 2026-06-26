using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public TurnHandler TurnHandler = new();
    public EnemyManager EnemyManager;
    public PlayerCombatManager PlayerPartyCombatManager;
    public CharCombatHandler ActiveCharacter;
    public EnemyCombatManager ActiveEnemy;
    public bool CheckBattleValidation => EnemyManager.ValidateBattle();

    private void Awake()
    {
        Instance = this;
        EnemyManager = GetComponentInChildren<EnemyManager>();
        PlayerPartyCombatManager = GetComponentInChildren<PlayerCombatManager>();
    }

    private void OnEnable()
    {
        StartCoroutine(BattleStartDelay());
        TurnHandler.StartRound();
    }

    private void OnDisable()
    {
        Instance = null;
    }
    
    public static void BattleEnd()
    {
        EncounterEvent.Instance.EncounterEnded();
    }
    
    private IEnumerator BattleStartDelay()
    {
        var t = 0f;
        while (t < 3)
        {
            t += Time.deltaTime;
            yield return null;
        }

        TurnHandler.NextRound();
    }
}