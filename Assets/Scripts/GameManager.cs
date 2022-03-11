using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1.0f;
    public GameOverMenu gameOverMenu;

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("GameOver", restartDelay);
        }
    }

    

    private void GameOver()
    {
        gameOverMenu.Setup((int)FindObjectOfType<CarController>().totalDistance);
        GameObject.Find("CarUI").SetActive(false);
    }
}
