using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Add New Quest")]
public class Quest : ScriptableObject
{
    public string questId;
    public string questShortName;
    public string questName;
    public string questDescription;
    public QuestObjective[] questObjectives;
}

[Serializable]
public class QuestObjective
{
    public int targetCount;
    public int currentCount;
    public QuestType questType;
    public string description;
    public bool isComplete;
}

public enum QuestType
{
    KillQuest,
    Travel
}
