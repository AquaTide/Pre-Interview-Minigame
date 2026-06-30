using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Unity.VectorGraphics.Scene;

public class TransportPlayer : MonoBehaviour
{
    public string GoToScene;

    public void MoveToScene()
    {
        SceneManager.LoadScene(GoToScene);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            MoveToScene();
    }
}