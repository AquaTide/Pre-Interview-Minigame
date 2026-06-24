using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterActionInterface : MonoBehaviour
{
    [SerializeField] private Canvas ActionInterface;
    [SerializeField] private Button ActiveOnSelected;

    [SerializeField] private Button AttackButton;
    [SerializeField] private Button DefenseButton;

    public Action AttackSelected;
    public Action DefenseSelected;

    private void Awake()
    {
        AttackButton.onClick.AddListener(() => { AttackSelected?.Invoke(); });
        DefenseButton.onClick.AddListener(() => { DefenseSelected?.Invoke(); });
    }

    public void EnableInterface()
    {
        if(ActionInterface.worldCamera == null)
            ActionInterface.worldCamera = Camera.main;
        ActionInterface.gameObject.SetActive(true);
        ActiveOnSelected.Select();
    }

    public void DisableInterface()
    {
        ActionInterface.gameObject.SetActive(false);
    }
}