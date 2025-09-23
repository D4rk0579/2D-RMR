using UnityEngine;
using UnityEngine.SceneManagement;
public class beginGame : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene"); //load the main scene, it's called samplescene because I never bothered to name it
    }
}
