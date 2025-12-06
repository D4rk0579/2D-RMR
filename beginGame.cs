using UnityEngine;
using UnityEngine.SceneManagement;
public class beginGame : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("ZoneOne"); // load level 1
    }

    public void goToShop()
    {
        SceneManager.LoadScene("Shop"); // load the shop
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("Main Menu"); // load main menu
    }
}
