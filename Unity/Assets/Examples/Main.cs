using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public void SwitchDemoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
