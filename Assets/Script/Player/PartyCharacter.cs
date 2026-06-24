using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Controllable/New Party Character")]
public class PartyCharacter : ScriptableObject
{
    public string characterName;
    public int baseHealth;
    public int baseAttack;
    public int baseSpeed;
    public GameObject characterObject;
}