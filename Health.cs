using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float maxHealth = 100;
    public float currentHealth;
    public GameObject GameObject;
    public Vector3 spawnPosition = new Vector3(-6f, 2f);
    public EnemyChase EnemyChase;
    public Upgrades upgrades;
    public ScoreText logic;
    private Rigidbody2D rb;
    public int scoreToNext;
    public static int level = 1;
    public GameObject square;
    public GameObject triangle; // variable definitions

    void Start()
    {
        if (Upgrades.damage < 10)
        {
            Upgrades.damage = 10; // set upgraded stats defaults, avoids bugs in case you haven't upgraded
        }
        if (level == 1)
        {
            scoreToNext = 25;
        } else if (level == 2)
        {
            scoreToNext = 50; 
        } else if (level == 3)
        {
            scoreToNext = 10000; // setting score values
        }
        Vector3 spawnPosition = new Vector3(-6f, 2f, 0f); // exact coordinates to respawn enemies
        currentHealth = maxHealth; // set health
        healthBar.UpdateHealthBar(currentHealth, maxHealth); // set hp bar ui
        logic = GameObject.FindGameObjectWithTag("scoreCanvas").GetComponent<ScoreText>(); // make hp bar show
        EnemyChase EnemyChase = GetComponent<EnemyChase>();
        rb = GetComponent<Rigidbody2D>(); // grab rigidbody
    }

    public void DifficultyIncrease()
    {
        maxHealth += 30; // increase max hp
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log("Max Health: " + maxHealth.ToString()); // log the max hp to the console, for debugging purposes
    }

    [SerializeField] FloatingHealthBar healthBar;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet")) // check for collision with a bullet
        {
            Debug.Log("Collision successful"); // tell me if the bullet successfully goes through hitting the enemy
            currentHealth = currentHealth - Upgrades.damage; // take damage
            healthBar.UpdateHealthBar(currentHealth, maxHealth); // update hp bar
            // Debug.Log(currentHealth); // log current hp to the console, for debugging purposes
            if (currentHealth <= 0) // death
            {
                if (logic.playerScore < scoreToNext) // check if score is enough to advance
                {
                    if (level == 1)
                    {
                        square.transform.position = spawnPosition; // easier to move it and reset hp than destroy and remake
                        rb.constraints = RigidbodyConstraints2D.FreezeAll;
                        rb.constraints = RigidbodyConstraints2D.None;
                        Instantiate(square, spawnPosition, Quaternion.identity); // make a new enemy alongside the one that just died
                        currentHealth += maxHealth; // reset hp
                        healthBar.UpdateHealthBar(currentHealth, maxHealth);
                        logic.addScore(); // add a kill to the score ui
                        logic.addWave(); // add a wave to the ui
                        logic.changeScoreUntilNextText();
                    } else if (level == 2)
                    {
                        triangle.transform.position = spawnPosition; // easier to move it and reset hp than destroy and remake
                        rb.constraints = RigidbodyConstraints2D.FreezeAll;
                        rb.constraints = RigidbodyConstraints2D.None;
                        Instantiate(triangle, spawnPosition, Quaternion.identity); // make a new enemy alongside the one that just died
                        currentHealth += maxHealth; // reset hp
                        healthBar.UpdateHealthBar(currentHealth, maxHealth);
                        logic.addScore(); // add a kill to the score ui
                        logic.addWave(); // add a wave to the ui
                        logic.changeScoreUntilNextText(); // update score needed for next level ui
                    }
                    else
                    {
                        square.transform.position = spawnPosition; // easier to move it and reset hp than destroy and remake
                        rb.constraints = RigidbodyConstraints2D.FreezeAll;
                        rb.constraints = RigidbodyConstraints2D.None;
                        Instantiate(square, spawnPosition, Quaternion.identity); // make a new enemy alongside the one that just died
                        currentHealth += maxHealth; // reset hp
                        healthBar.UpdateHealthBar(currentHealth, maxHealth);
                        logic.addScore(); // add a kill to the score ui
                        logic.addWave(); // add a wave to the ui
                        logic.changeScoreUntilNextText();
                    }
                }
                else if (logic.playerScore >= scoreToNext && level == 1)
                {
                    SceneManager.LoadScene("ZoneTwo");
                    level++;
                    logic.scoreUntilNextText.text = $"Score until next area: {scoreToNext}"; // start level 2
                }
                else if (logic.playerScore >= scoreToNext && level == 2)
                {
                    SceneManager.LoadScene("ZoneThree");
                    level++;
                    logic.scoreUntilNextText.text = $"Score until next area: {scoreToNext}"; // start level 3
                }
            }
        }
    }
}
