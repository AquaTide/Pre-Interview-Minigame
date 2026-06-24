using UnityEngine;
using UnityEngine.SceneManagement;

public class MMButtonEvent : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("BeginnerVillage");
    }
}