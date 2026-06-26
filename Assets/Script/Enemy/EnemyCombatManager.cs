using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyCombatManager : MonoBehaviour, ITurnParticipant, ITakingDamage, IAilment
{
    public EnemyData enemyData;
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public Ailment ActiveAilment { get; private set; }
    public string Name => gameObject.name;
    public int Speed => CalculateSpeedModifier();
    public Action OnAttack;
    public Action OnDeath;
    public Action OnDamaged;
    public Action OnEnableTargeting;
    public Action OnTargeted;

    private EnemyAnimationCallback _animationCallback;

    private void Awake()
    {
        _animationCallback = GetComponentInChildren<EnemyAnimationCallback>();
    }

    private void OnEnable()
    {
        SetBaseStats();
        InstantiateEnemy();
        RegisterParticipant();

    }

    private void OnDisable()
    {
        _animationCallback.DealDamageAction -= DealDamage;
    }

    private void InstantiateEnemy()
    {
        Instantiate(enemyData.prefab, transform.position, Quaternion.identity, transform);
        
        _animationCallback = GetComponentInChildren<EnemyAnimationCallback>();
        _animationCallback.DealDamageAction += DealDamage;
    }

    #region TurnOrder

    public void RegisterParticipant()
    {
        var bm = BattleManager.Instance;
        bm.TurnHandler.RegisterParticipant(this);
    }

    public void RemoveOnDeath()
    {
        if ((ActiveAilment & Ailment.Dead) == 0) return;
        var bm = BattleManager.Instance;
        bm.TurnHandler.RemoveFromTurnOrder(this);
    }

    public void TakeTurn()
    {
        var bm = BattleManager.Instance;
        bm.ActiveEnemy = this;

        if ((ActiveAilment & Ailment.Dead) == 0)
            Attacking();
        else
            EndTurn();
    }

    #endregion

    #region Stats

    private void SetBaseStats()
    {
        Health = enemyData.maxHealth;
        Attack = enemyData.baseAttack;
    }

    private int CalculateSpeedModifier()
    {
        return enemyData.baseSpeed;
    }

    #endregion

    public void TakingDamage(int damage)
    {
        Health -= damage;
        OnDamaged?.Invoke();

        if (Health > 0) return;
        ActiveAilment |= Ailment.Dead;
        DeadOccur();
    }

    private void Attacking()
    {
        OnAttack?.Invoke();
    }

    public void DealDamage()
    {
        var c = BattleManager.Instance.PlayerPartyCombatManager.GetRandomChar();
        c.TakingDamage(Attack);
    }

    public static void EndTurn()
    {
        var bm = BattleManager.Instance;
        bm.TurnHandler.NextTurn();
    }

    #region Ailment

    public void SetActiveAilment(Ailment activeAilment)
    {
        throw new NotImplementedException();
    }

    public void CureAilment(Ailment ailment)
    {
        throw new NotImplementedException();
    }

    public void CureAllAilments()
    {
        throw new NotImplementedException();
    }

    public void PoisonOccur()
    {
        throw new NotImplementedException();
    }

    public void StunOccur()
    {
        throw new NotImplementedException();
    }

    public void ParalyzeOccur()
    {
        throw new NotImplementedException();
    }

    public void SleepOccur()
    {
        throw new NotImplementedException();
    }

    public void DeadOccur()
    {
        var bm = BattleManager.Instance;
        bm.TurnHandler.RemoveFromTurnOrder(this);
        OnDeath?.Invoke();
    }

    #endregion
}