using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterEvent : MonoBehaviour
{
    public static EncounterEvent Instance;

    public Encounter encounter;
    
    private ColliderMonitor _monitor;
    private Collider2D _playerCollider;
    private bool prepToDestroy;
    
    private void Awake()
    {
        _monitor = GetComponentInChildren<ColliderMonitor>();
    }

    private void OnEnable()
    {
        _monitor.onTriggerEnter += TriggerEncounter;
        GenerateEncounterEnemies();
    }

    private void OnDisable()
    {
        _monitor.onTriggerEnter -= TriggerEncounter;
    }

    public void GenerateEncounterEnemies()
    {
        if(encounter.isRandomEnemies)
            encounter.RandomEncounter();
        else
            encounter.SetEncounter();
    }

    public void TriggerEncounter()
    {
        Instance = this;
        SceneManager.LoadScene("BtlBeginnerDungeon", LoadSceneMode.Additive);
    }

    private void OnSceneUnloaded(Scene sc)
    {
        if (sc.name != "BtlBeginnerDungeon") return;
        _playerCollider.transform.parent.gameObject.SetActive(true);
        Instance = null;
        Destroy(gameObject);
    }

    private void TriggerEncounter(Collider2D other)
    {
        if(prepToDestroy) return;
        if(!other.transform.parent.CompareTag("Player")) return;
        _playerCollider = other;
        _playerCollider.transform.parent.gameObject.SetActive(false);
        prepToDestroy = true;
        _monitor.onTriggerEnter -= TriggerEncounter;
        TriggerEncounter();
    }

    public void EncounterEnded()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.UnloadSceneAsync("BtlBeginnerDungeon");
    }
}
