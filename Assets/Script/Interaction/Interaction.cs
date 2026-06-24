using System;
using UnityEngine;
using MyBox;

public enum InteractionType
{
    Talk,
    ReceiveItem
}
public class Interaction : MonoBehaviour
{
    public InteractionType interactionType;

    [ConditionalField(nameof(interactionType), false, InteractionType.ReceiveItem)]
    public ReceiveItem receiveItemAction;

    [ConditionalField(nameof(interactionType), false, InteractionType.Talk)]
    public TalkEvent talkEvent;
    
    public void InteractWithObject(PlayerControl player)
    {
        switch (interactionType)
        {
            case InteractionType.Talk:
                talkEvent.Talk(player);
                break;
            case InteractionType.ReceiveItem:
                throw new NotImplementedException("Receive item not implemented");
                break;
        }
    }
}

