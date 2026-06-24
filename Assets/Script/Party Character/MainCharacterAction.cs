using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MainCharacterAction : MonoBehaviour
{
    private CharacterActionInterface _actionInterface;
    private CharBattleHandler _charBattleHandler;
    public UnityEvent AttackAction;
    public UnityEvent DefenseAction;

    public void EndTurn() => _charBattleHandler.EndTurn();
    
    private void Awake()
    {
        _actionInterface = GetComponent<CharacterActionInterface>();
        _charBattleHandler = GetComponentInParent<CharBattleHandler>();
    }

    private void OnEnable()
    {
        _charBattleHandler.OnTakeTurn += OnTakeTurn;
        _charBattleHandler.AttackPerformed += AttackPerformed;
        _charBattleHandler.OnDamaged += OnDamaged;
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
        _charBattleHandler.OnTakeTurn    -= OnTakeTurn;
        _charBattleHandler.OnDamaged     -= OnDamaged;
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
        throw new NotImplementedException();
    }

    private void OnTakeTurn()
    {
        var bm = BattleManager.Instance;
        bm.ActiveCharacter = _charBattleHandler;
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