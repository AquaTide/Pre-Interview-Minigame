using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetCameraAsMainOnAdditive : MonoBehaviour
{
    private Camera _currentCamera;

    private void Awake()
    {
        _currentCamera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnload;
    }

    private void OnSceneUnload(Scene arg0)
    {
        _currentCamera.tag = "Untagged";
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        _currentCamera.tag = "MainCamera";
    }
}