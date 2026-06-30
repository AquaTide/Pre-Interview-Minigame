using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerChangeScene : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
