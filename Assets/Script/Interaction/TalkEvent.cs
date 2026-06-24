using Naninovel;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Talk Event", menuName = "Player Interaction/Talk Event")]
public class TalkEvent : ScriptableObject
{
    [ScriptAssetRef]
    public string script;
    public string label;
    public UnityEvent onDialogueEnter;
    public UnityEvent onDialogueEnd;

    private PlayerControl _player;
    public void Talk(PlayerControl player)
    {
        Dialogue.OnEntered += HandleDialogueStart;
        Dialogue.OnExited += HandleDialogueEnd;
        
        var path = ScriptAssets.GetPath(script);
        _player = player;
        Dialogue.EnterAndPlay(path).Forget();
    }

    private void HandleDialogueEnd()
    {
        _player.EnableControl();
        
        onDialogueEnd?.Invoke();
    }

    private void HandleDialogueStart()
    {
        _player.DisableControls();
        
        onDialogueEnter?.Invoke();
    }
}
