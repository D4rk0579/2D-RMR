using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public Text waveText;
    public int playerScore;
    public int highScore;
    public int wave = 1;
    public Health health;
    public int waveRequirement = 3; //all of these set basic variables
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "Score: 0"; // set the ui text up
        highScoreText.text = "High Score: 0";
        waveText.text = "Wave: 1";
        health = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        playerScore = 0; // reset score
        scoreText.text = "Score: " + playerScore.ToString(); 
    }
    public void addWave()
    {
        if (playerScore >= waveRequirement) // increase waves
        {
            wave += 1; // increase wave
            waveText.text = "Wave: " + wave.ToString();
            waveRequirement *= 2; // increase requirement to reach next wave
            health.DifficultyIncrease(); // increase health
        }
    }
}
