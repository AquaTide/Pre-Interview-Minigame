using System;
using UnityEngine;
using MyBox;
using UnityEngine.Events;
using Naninovel;


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
    [SerializeField] private bool _haveBeenTriggered;
    private PlayerControl _player;

    public UnityEvent OnInteractionStart;
    public UnityEvent OnInteractionEnd;

    public void InteractWithObject(PlayerControl player)
    {
        if (_haveBeenTriggered && !CanBeRepeated) return;
        if (player.isDoingEvent) return;
        _player = player;
        switch (interactionType)
        {
            case InteractionType.Talk:
                Dialogue.OnEntered += HandleDialogueStart;
                Dialogue.OnExited += HandleDialogueEnd;
                talkEvent.Talk(player);
                break;
            case InteractionType.ReceiveItem:
                throw new NotImplementedException("Receive item not implemented");
                break;
        }

        _haveBeenTriggered = true;
    }

    private void HandleDialogueStart()
    {
        _player.DisableControls();
        OnInteractionStart?.Invoke();
    }

    private void HandleDialogueEnd()
    {
        OnInteractionEnd?.Invoke();
        Dialogue.OnEntered -= HandleDialogueStart;
        Dialogue.OnExited -= HandleDialogueEnd;
    }
}