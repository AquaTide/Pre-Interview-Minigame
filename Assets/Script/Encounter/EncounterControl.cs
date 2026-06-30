using System;
using UnityEngine;

public class EncounterControl : MonoBehaviour
{
    public static EncounterControl Instance;
    public Encounter EncounterZone;
    public float EncounterBuildup;
    public float BuildUpRate;

    private EncounterEvent _encounterEvent;
    [SerializeField] private PlayerControl _playerControl;

    private void OnEnable()
    {
        _playerControl = FindFirstObjectByType<PlayerControl>();
    }

    private void Update()
    {
        if(_playerControl.PlayerBody.linearVelocity.magnitude > 0)
            EncounterChance();
    }

    public void EncounterChance()
    {
        EncounterBuildup += Time.deltaTime * BuildUpRate;
        if (EncounterBuildup < 100) return;
        TriggerEncounter();
    }

    public void ResetBuildUp()
    {
        EncounterBuildup = 0;
    }

    private void TriggerEncounter()
    {
        _encounterEvent = gameObject.AddComponent<EncounterEvent>();
        _encounterEvent.encounter = EncounterZone;
        _encounterEvent.OnEncounterEnded += HandleEncounterEnd;
        _encounterEvent.TriggerEncounter(_playerControl);
    }

    private void HandleEncounterEnd()
    {
        ResetBuildUp();
        _encounterEvent.OnEncounterEnded -= HandleEncounterEnd;
        Destroy(_encounterEvent);
    }
}