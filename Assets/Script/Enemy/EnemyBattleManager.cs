using System;
using UnityEngine;

public class EnemyBattleManager : MonoBehaviour, ITurnParticipant, ITakingDamage, IAilment
{
    public EnemyData enemyData;
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public string Name => enemyData.displayName;
    public int Speed => CalculateSpeedModifier();
    public Action OnDamaged;
    public Action OnEnableTargeting;
    public Action OnTargeted;

    private void OnEnable()
    {
        SetBaseStats();
        InstantiateEnemy();
        AddToTurnOrder();
        AddToTargetingList();
    }

    private void InstantiateEnemy()
    {
        Instantiate(enemyData.prefab, transform.position, Quaternion.identity, transform);
    }

    private void AddToTurnOrder()
    {
        var bm = BattleManager.Instance;
        bm.TurnHandler.AddEnemies(this);
    }

    private void AddToTargetingList()
    {
        var bm = BattleManager.Instance;
    }

    private void SetBaseStats()
    {
        Health = enemyData.maxHealth;
        Attack = enemyData.baseAttack;
    }

    private int CalculateSpeedModifier()
    {
        return enemyData.baseSpeed;
    }

    public void TakeTurn()
    {
        var bm = BattleManager.Instance;
        bm.ActiveEnemy = this;
        
        Debug.Log($"{gameObject.name}: Take turn");
    }

    public void TakingDamage(int damage)
    {
        Health -= damage;
        OnDamaged?.Invoke();
    }

    public Ailment ActiveAilment { get; }

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
        throw new NotImplementedException();
    }
}