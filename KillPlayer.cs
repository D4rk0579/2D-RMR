using System.Security.Cryptography;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player; // this is you!
    public Transform respawnPoint;
    public ScoreText logic; // ui
    public Upgrades upgrades;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("scoreCanvas").GetComponent<ScoreText>(); // get the score text ui
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) // find the player object
        {
            logic.resetScore(); // reset your score on death
        }
    }
}
