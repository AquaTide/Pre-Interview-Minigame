using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Add NPC/Add New NPC")]
public class NonPlayableCharacter : ScriptableObject
{
    public string npcId;
    public string npcName;
    public Sprite npcSprite;
    public Animator animator;
}