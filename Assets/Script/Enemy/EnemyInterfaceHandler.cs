using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInterfaceHandler : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Button Targeting;
    private EnemyBattleManager _battleManager;
    
    private void Awake()
    {
        _battleManager = GetComponentInParent<EnemyBattleManager>();
    }

    private void OnEnable()
    {
        _battleManager.OnDamaged += OnHealthChange;
        _battleManager.OnEnableTargeting += EnableTargeting;
        _battleManager.OnTargeted += Targeted;
        Targeting.onClick.AddListener(() => OnTargetSelected());
    }

    private void OnDisable()
    {
        _battleManager.OnDamaged -= OnHealthChange;
    }

    private void OnHealthChange()
    {
        var curHp = _battleManager.Health;
        var maxHp = _battleManager.enemyData.maxHealth;
        
        HealthBar.maxValue = maxHp;
        HealthBar.value = curHp;
    }

    private void EnableTargeting() => Targeting.interactable = true;
    private void DisableTargeting() => Targeting.interactable = false;
    private void Targeted() => Targeting.Select();

    private void OnTargetSelected()
    {
        DisableTargeting();
        var bm = BattleManager.Instance;
        var dmg = bm.ActiveCharacter.Attack;
        
        _battleManager.TakingDamage(dmg);
        bm.ActiveCharacter.AttackPerformed();
        bm.ActiveCharacter.EndTurn();
    }
}