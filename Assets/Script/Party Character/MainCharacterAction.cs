using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MainCharacterAction : MonoBehaviour
{
    private CharacterActionInterface _actionInterface;
    private CharCombatHandler _charCombatHandler;
    public UnityEvent AttackAction;
    public UnityEvent DefenseAction;

    public void EndTurn() => _charCombatHandler.EndTurn();
    
    private void Awake()
    {
        _actionInterface = GetComponent<CharacterActionInterface>();
        _charCombatHandler = GetComponentInParent<CharCombatHandler>();
    }

    private void OnEnable()
    {
        _charCombatHandler.OnTakeTurn += OnTakeTurn;
        _charCombatHandler.AttackPerformed += AttackPerformed;
        _charCombatHandler.OnTakingDamage += OnDamaged;
        // _charBattleHandler.OnPoisonOccur += OnPoisonOccur;
        // _charBattleHandler.OnStunOccur   += OnStunOccur;
        // _charBattleHandler.OnParalyzeOccur += OnParalyzeOccur;
        // _charBattleHandler.OnSleepOccur  += OnSleepOccur;
        // _charBattleHandler.OnDeadOccur   += OnDeadOccur;
        
        
        _actionInterface.AttackSelected += EnableTargetingOnEnemies;
        _actionInterface.DefenseSelected += OnDefense;
    }

    private void AttackPerformed()
    {
        AttackAction?.Invoke();
    }

    private void OnDisable()
    {
        _charCombatHandler.OnTakeTurn    -= OnTakeTurn;
        _charCombatHandler.OnTakingDamage     -= OnDamaged;
        // _charBattleHandler.OnPoisonOccur -= OnPoisonOccur;
        // _charBattleHandler.OnStunOccur   -= OnStunOccur;
        // _charBattleHandler.OnParalyzeOccur -= OnParalyzeOccur;
        // _charBattleHandler.OnSleepOccur  -= OnSleepOccur;
        // _charBattleHandler.OnDeadOccur   -= OnDeadOccur;

        _actionInterface.AttackSelected -= EnableTargetingOnEnemies;
        _actionInterface.DefenseSelected -= OnDefense;
    }

    private void OnDamaged()
    {
        
    }

    private void OnTakeTurn()
    {
        var bm = BattleManager.Instance;
        bm.ActiveCharacter = _charCombatHandler;
        _actionInterface.EnableInterface();
    }

    private void EnableTargetingOnEnemies()
    {
        _actionInterface.DisableInterface();
        var bm = BattleManager.Instance;
        bm.EnemyManager.EnableTargetingOnEnemies();
    }

    private void OnDefense()
    {
        DefenseAction?.Invoke();
        Debug.Log("Defense performed");
    }
}