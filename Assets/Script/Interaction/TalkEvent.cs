using System;
using UnityEngine;
using Naninovel;


[CreateAssetMenu(fileName = "Talk Event", menuName = "Player Interaction/Talk Event")]
public class TalkEvent : ScriptableObject
{
    [ScriptAssetRef]
    public string script;
    public string label;
    public bool AutoEvent;
    public Action OnDialogueEnd;

    private PlayerControl _player;
    public void Talk(PlayerControl player)
    {
        var path = ScriptAssets.GetPath(script);
        _player = player;
        _player.isDoingEvent  = true;
        Dialogue.EnterAndPlay(path).Forget();
    }

    // private void HandleDialogueEnd()
    // {
    //     OnDialogueEnd?.Invoke();
    // }
    //
    // private void HandleDialogueStart()
    // {
    //     _player.DisableControls();
    // }
}
