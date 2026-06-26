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
    public bool AutoEvent => talkEvent.AutoEvent;

    [ConditionalField(nameof(interactionType), false, InteractionType.ReceiveItem)]
    public ReceiveItem receiveItemAction;

    [ConditionalField(nameof(interactionType), false, InteractionType.Talk)]
    public TalkEvent talkEvent;

    public bool CanBeRepeated;
    [SerializeField] [ReadOnly] private bool _haveBeenTriggered;

    public void InteractWithObject(PlayerControl player)
    {
        if (_haveBeenTriggered && !CanBeRepeated) return;
        switch (interactionType)
        {
            case InteractionType.Talk:
                talkEvent.Talk(player);
                break;
            case InteractionType.ReceiveItem:
                throw new NotImplementedException("Receive item not implemented");
                break;
        }

        _haveBeenTriggered = true;
    }
}