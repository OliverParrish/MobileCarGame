using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
