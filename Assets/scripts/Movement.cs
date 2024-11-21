using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private SpriteRenderer sr;
    private Animator anim;

    [Header("Movement Settings")]
    public float upForce = 100f;
    public float speed = 1500f;
    public float runSpeed = 2500f;

    [Header("State Flags")]
    public bool isGrounded = false;

    private bool isLeftShift;
    private float moveHorizontal;
    private float moveVertical;
    private bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        isLeftShift = Input.GetKey(KeyCode.LeftShift);
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        HandleSpriteFlip();
        HandleAnimation();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();

        anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
    }

    private void HandleSpriteFlip()
    {
        if (moveHorizontal > 0)
        {
            sr.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            sr.flipX = true;
        }
    }

    private void HandleAnimation()
    {
        bool isMoving = Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0;
        anim.SetBool("isRunning", isMoving);
        anim.SetBool("isJumping", isJumping); // Control jump animation
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        isGrounded = false;
        isJumping = true; // Set jumping state to true
    }

    private void HandleMovement()
    {
        // Get forward direction from the camera, ignoring the vertical component
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Calculate movement direction
        Vector3 movement = (cameraForward * moveVertical + Vector3.right * moveHorizontal).normalized;

        // Apply velocity
        float appliedSpeed = isLeftShift ? runSpeed : speed;
        rb.velocity = new Vector3(movement.x * appliedSpeed * Time.deltaTime, rb.velocity.y, movement.z * appliedSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < 45f)
            {
                isGrounded = true;
                isJumping = false; // Reset jumping state
                anim.SetBool("isGrounded", true);
                break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        anim.SetBool("isGrounded", false);
    }
}