using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI distanceTravelled;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        distanceTravelled.text = "Distance: " + score.ToString() + "m";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GameObject.Find("CarUI").SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
