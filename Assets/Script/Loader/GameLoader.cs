using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [Flags]
    private enum LoadState
    {
        Localization = 1 << 0,
        Asset        = 1 << 1
    }
    
    private readonly LoadState _expectedState = LoadState.Localization;
    private LoadState _curState;

    private void Start()
    {   
        LoadLocalization();
        StartCoroutine(OnLoaderComplete());
    }

    private IEnumerator OnLoaderComplete()
    {
        yield return new WaitUntil(() => (_curState & _expectedState) == _expectedState);
        SceneManager.LoadScene("Main Menu");
    }

    private void LoadLocalization()
    {
        _curState = LoadState.Localization;
    }
}