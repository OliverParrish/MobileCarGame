using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI distanceTravelled;
    private bool hasBeenPlayed;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject background;
    private void OnEnable()
    {
        EventManager.showGameOverMenu += Setup;
        EventManager.onContinue += Continue;
    }

    private void OnDisable()
    {
        EventManager.showGameOverMenu -= Setup;
        EventManager.onContinue -= Continue;
    }
    public void Setup()
    {
        background.SetActive(true);
        distanceTravelled.text = "Distance: " + FindObjectOfType<CarController>().totalDistance.ToString() + "m";
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

    public void Continue()
    {
        background.SetActive(false);
        EventManager.FuelPickup();
        continueButton.SetActive(false);
        GameObject.Find("CarUI").SetActive(true);

    }
    
}
