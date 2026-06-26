using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInterfaceHandler : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Button Targeting;
    private EnemyCombatManager _combatManager;

    private void Awake()
    {
        _combatManager = GetComponentInParent<EnemyCombatManager>();
    }

    private void OnEnable()
    {
        _combatManager.OnDamaged += OnHealthChange;
        _combatManager.OnEnableTargeting += EnableTargeting;
        _combatManager.OnTargeted += Targeted;
        Targeting.onClick.AddListener(() => OnTargetSelected());
    }

    private void OnDisable()
    {
        _combatManager.OnDamaged -= OnHealthChange;
    }

    private void Start()
    {
        InitializeHealthBar();
    }

    private void OnHealthChange()
    {
        UpdateHealthBar();
    }

    private void InitializeHealthBar()
    {
        var maxHp = _combatManager.enemyData.maxHealth;
        HealthBar.maxValue = maxHp;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        var curHp = _combatManager.Health;
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

        _combatManager.TakingDamage(dmg);
        bm.ActiveCharacter.AttackPerformed();
        bm.ActiveCharacter.EndTurn();
    }
}