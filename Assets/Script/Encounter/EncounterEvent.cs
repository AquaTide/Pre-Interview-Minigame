using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EncounterEvent : MonoBehaviour
{
    public static EncounterEvent Instance;

    public Encounter encounter;
    public Action OnEncounterEnded;
    public UnityEvent ScriptedEvent;

    private ColliderMonitor _monitor;
    private GameObject _player;
    private bool prepToDestroy;

    private void Awake()
    {
        _monitor = GetComponentInChildren<ColliderMonitor>();
    }

    private void OnEnable()
    {
        if (_monitor != null)
        {
            _monitor.onTriggerEnter += TriggerEncounter;
            GenerateEncounterEnemies();
        }
    }

    private void OnDisable()
    {
        if (_monitor != null)
            _monitor.onTriggerEnter -= TriggerEncounter;
    }

    public void GenerateEncounterEnemies()
    {
        if (encounter.isRandomEnemies)
            encounter.RandomEncounter();
        else
            encounter.SetEncounter();
    }

    private void TriggerEncounter()
    {
        Instance = this;
        SceneManager.LoadScene("BtlBeginnerDungeon", LoadSceneMode.Additive);
    }

    private void OnSceneUnloaded(Scene sc)
    {
        if (sc.name != "BtlBeginnerDungeon") return;
        _player.SetActive(true);
        Instance = null;
        OnEncounterEnded?.Invoke();
        ScriptedEvent?.Invoke();

        if (prepToDestroy)
            gameObject.SetActive(false);
    }

    private void TriggerEncounter(Collider2D other)
    {
        if (prepToDestroy) return;
        if (!other.transform.parent.CompareTag("Player")) return;

        _player = other.gameObject;
        _player.SetActive(false);
        prepToDestroy = true;
        _monitor.onTriggerEnter -= TriggerEncounter;
        TriggerEncounter();
    }

    public void TriggerEncounter(PlayerControl other)
    {
        _player = other.gameObject;
        _player.SetActive(false);
        TriggerEncounter();
    }

    public void EncounterEnded()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.UnloadSceneAsync("BtlBeginnerDungeon");
    }
}