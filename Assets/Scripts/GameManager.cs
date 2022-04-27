using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1.0f;
    public InterstitialAdExample adExample;


    private void OnEnable()
    {
        EventManager.showAdOnRestart += ShowAdOnRestart;
        
    }

    private void OnDisable()
    {
        EventManager.showAdOnRestart -= ShowAdOnRestart;
        
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
        adExample.LoadAd();
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            GameOver();
        }
    }

    private void ShowAdOnRestart()
    {
        adExample.ShowAd();
    }
    

    private void GameOver()
    {
        GameObject.Find("CarUI").SetActive(false);
        EventManager.ShowGameOverMenu();
    }
}
