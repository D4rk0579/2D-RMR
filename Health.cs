using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static float maxHealth = 100;
    public float currentHealth;
    public GameObject GameObject;
    public Transform enemyRespawn;
    public ScoreText logic; // variable definitions

    void Start()
    {
        currentHealth = maxHealth; // set health
        healthBar.UpdateHealthBar(currentHealth, maxHealth); // set hp bar ui
        logic = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ScoreText>(); // make hp bar show
    }

    public void DifficultyIncrease()
    {
        maxHealth += 20; // increase max hp
        Debug.Log("Max Health: " + maxHealth.ToString()); // log the max hp to the console, for debugging purposes
    }

    [SerializeField] FloatingHealthBar healthBar;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet")) // check for collision with a bullet
        {
            currentHealth = currentHealth - 10; // take damage
            healthBar.UpdateHealthBar(currentHealth, maxHealth); // update hp bar
            Debug.Log(currentHealth); // log current hp to the console, for debugging purposes
            if (currentHealth <= 0) // death
            {
                GameObject.transform.position = enemyRespawn.position; // easier to move it and reset hp than destroy and remake
                currentHealth += maxHealth; // reset hp
                logic.addScore(); // add a kill to the score ui
                logic.addWave(); // add a wave to the ui
                Instantiate(GameObject, enemyRespawn); // make a new enemy alongside the one that just died
            }
        }
    }
}
