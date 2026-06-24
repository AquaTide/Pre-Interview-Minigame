using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Village State", menuName = "Village/Add New Village State")]
public class Village : ScriptableObject
{
    public string villageName;
    public string villageDescription;
    public Sprite villageIcon;
    [Tooltip("Village state depend on quest cleared")]
    public Quest[] villageStatesAfterQuestClear;
}