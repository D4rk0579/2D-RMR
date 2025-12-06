using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("ZoneOne"); //load level 1
    }
    public void goToMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); //load the main menu
    }
    
}
