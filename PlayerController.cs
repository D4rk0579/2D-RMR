using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f; 
    public Rigidbody2D rb;
    public Weapon weapon;
    Vector2 moveDirection;
    Vector2 mousePosition; // set variables

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // horizontal and vertical movement variables
        float moveY = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButtonDown(0))
        {
            weapon.Fire(); // shoot the gun
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
}
