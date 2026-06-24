using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Obtain Item", menuName = "Player Interaction/Obtain Item")]
public class ReceiveItem : ScriptableObject
{
    public ItemReceived[] itemReceived;
}

[Serializable]
public class ItemReceived
{
    public Item item;
    public int amount;
}