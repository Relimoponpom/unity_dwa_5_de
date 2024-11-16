using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    Rigidbody rb;
    SpriteRenderer sr;
    Animator anim;

    public float upForce = 100;
    public float speed = 1500;
    public float runSpeed = 2500;
    public bool isGrounded = false;

    bool isLeftShift;
    float moveHorizontal;
    float moveVertical; // For forward/backward movement

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        isLeftShift = Input.GetKey(KeyCode.LeftShift);
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical"); // W/S or Up/Down for forward/backward

        // Handle sprite flipping based on horizontal movement
        if (moveHorizontal > 0)
        {
            sr.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            sr.flipX = true;
        }

        // Handle running animation
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        // Jump functionality
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
            isGrounded = false;
            anim.SetTrigger("Jump"); // Trigger jump animation
        }
    }

    private void FixedUpdate()
    {
        // Get forward direction from the camera
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // Ignore vertical component
        cameraForward.Normalize();

        // Calculate movement direction
        Vector3 movement = (cameraForward * moveVertical + Vector3.right * moveHorizontal).normalized;

        // Apply velocity
        float appliedSpeed = isLeftShift ? runSpeed : speed;
        rb.velocity = new Vector3(movement.x * appliedSpeed * Time.deltaTime, rb.velocity.y, movement.z * appliedSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ensure the character is grounded when colliding with the ground
        isGrounded = true;
        anim.SetBool("isGrounded", true);
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        anim.SetBool("isGrounded", false);
    }
}
