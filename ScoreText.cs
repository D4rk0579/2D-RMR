using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public Text waveText;
    public Text scoreUntilNextText;
    public int playerScore;
    public static int highScore;
    public int wave = 1;
    public Health health;
    public Upgrades upgrades;
    public float waveRequirement = 3; //all of these set basic variables
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
        scoreText.text = "Score: 0"; // set the ui text up
        highScoreText.text = "High Score: " + highScore;
        waveText.text = "Wave: 1";
        scoreUntilNextText.text = $"Score until next area: {health.scoreToNext}";
    }

    [ContextMenu("Increase Score")] // allows me to increase the score straight from the editor, really nice for debugging
    public void addScore()
    {
        playerScore += 1; // add 1 to score when function is called
        scoreText.text = "Score: " + playerScore.ToString(); // update the text as follows
    }
    public void resetScore()
    {
        if (highScore < playerScore) // set high score text on death
        {
            highScore = playerScore; // increase high score when player exceeds it
            highScoreText.text = "High Score: " + playerScore.ToString(); // state high score in ui
        }
        Upgrades.money += playerScore;
        if (Health.level == 2)
        {
            Upgrades.money += 25; // account for the 25 score you get to transition from level 1 to 2
        }
        waveRequirement = 3;
        wave = 1;
        health.maxHealth = 100;
        health.scoreToNext = 25;
        Health.level = 1;
        playerScore = 0; // reset score
        scoreText.text = "Score: " + playerScore.ToString();
        KeepData.highScoreValue = highScore;
        SceneManager.LoadScene("DeathScreen");
    }
    public void addWave()
    {
        if (playerScore >= waveRequirement) // increase waves
        {
            wave += 1; // increase wave
            waveText.text = "Wave: " + wave.ToString();
            waveRequirement += Mathf.Floor(((waveRequirement * 3) / 4) + 3); // increase requirement to reach next wave
            Debug.Log("Required for next wave: " + waveRequirement);
            health.DifficultyIncrease(); // increase health
        }
    }

    public void changeScoreUntilNextText()
    {
        scoreUntilNextText.text = "Score until next area: " + (health.scoreToNext - playerScore); // tell you how far you are from next zone
    }
}
