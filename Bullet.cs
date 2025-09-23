using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifespan = 3f; // set lifespan so it doesn't go on forever
    public float timer; // lifespan timer
    void Start()
    {
        timer = lifespan;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // destroy on collision
    }
    private void FixedUpdate()
    {
        timer -= Time.deltaTime; // make timer tick down 1 second regardless of framerate (that's what Time.deltaTime does)
        if (timer <= 0)
        {
            Destroy(gameObject); // destroy bullet after timer expires
        }
    }
}
