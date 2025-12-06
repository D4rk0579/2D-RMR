using System.Collections;
using Unity.Cinemachine;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f; 
    public Rigidbody2D rb;
    public Weapon weapon;
    public float cooldown = 0.15f;
    public bool autoFire;
    Vector2 moveDirection;
    Vector2 mousePosition; // set variables

    void Start()
    {
        if (KeepData.autoFire == true)
        {
            autoFire = true;
            StartCoroutine(AutoFire());
        }
    }
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        float moveX = Input.GetAxisRaw("Horizontal"); // horizontal and vertical movement variables
        float moveY = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButtonDown(0))
        {
            if (cooldown <= 0) // cd check
            {
                if (autoFire == false) // checks if autofire is false to stop you from stacking shots
                {
                    weapon.Fire(); // shoot the gun
                    cooldown = 0.15f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) // check for autofire hotkey
        {
            if (autoFire == false) // toggle autofire
            {
                autoFire = true;
                KeepData.autoFire = true;
                StartCoroutine(AutoFire()); // start autofire
            }
            else
            {
                autoFire = false;
                KeepData.autoFire = false;
            }
        }
        moveDirection = new Vector2(moveX, moveY).normalized; // move
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // rotate
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        // determine movement speed and actually move the character
        Vector2 aimDirection = mousePosition - rb.position; // aim your gun
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle; // set the gun to rotate with the character smoothly
    }

    IEnumerator AutoFire() // autofire coroutine
    {
        weapon.Fire();
        yield return new WaitForSeconds(0.25f);
        if (autoFire == true)
        {
            StartCoroutine(AutoFire()); // if autoFire is still true, restart, if not then leave it be
        }
    }
}
