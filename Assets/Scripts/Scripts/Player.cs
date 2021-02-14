
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
public class Player : MonoBehaviour
{
    public PlayerCrouch playerCrouch;

    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float sprintSpeed = 1f;
    public float timeBetweenFire = 3f;

    public Transform groundCheck;
    public bool isGrounded;
    public float groundDistance;
    public LayerMask groundMask;
    public bool isFired = false;

    public float directionManitude;

    Animator playerAnim;
    UnityEngine.Vector3 velocity;

    float turnSmoothVelocity;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        //JUMP-----------------------------------------------------------------------------
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerAnim.SetTrigger("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //GRAVITY-----------------------------------------------------------
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Fire---------------------------------------------------------------------
        if (Input.GetMouseButton(0))
        {
            isFired = true;
            playerAnim.SetBool("fire", true);
            speed = 2f;
        }
        else
        {
            isFired = false;
            speed = 6f;
            playerAnim.SetBool("fire", false);
        }

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerAnim.SetBool("sprint", true);
            sprintSpeed = 1.5f;
        }
        else
        {
            sprintSpeed = 1f;
            playerAnim.SetBool("sprint", false);
        }

        //Movement
        playerAnim.SetFloat("InputX", horizontal);
        playerAnim.SetFloat("InputY", vertical);

       

        UnityEngine.Vector3 direction = new UnityEngine.Vector3(horizontal, 0f, vertical).normalized;
        if(direction.magnitude >= 0.0f)
        {
            if ((horizontal == 1 || horizontal == -1) && vertical != 0) { speed = 3.5f; }
            if ((horizontal == 1 || horizontal == -1)) { speed = 3.5f; }
            if (direction.magnitude == 0) { speed = 0f; }
            if(playerCrouch.isCrouched && direction.magnitude != 0) { speed = 2.5f; }

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity , turnSmoothTime);
            transform.rotation = UnityEngine.Quaternion.Euler(0f, angle, 0f);

            UnityEngine.Vector3 moveDir = UnityEngine.Quaternion.Euler(0f, targetAngle, 0f) * UnityEngine.Vector3.forward;
            controller.Move(moveDir.normalized * speed * sprintSpeed * Time.deltaTime);
        }

    }

    IEnumerator ResumeMovement()
    {
        yield return new WaitForSeconds(3.17f);
        isFired = false;
        speed = 6f;
    }


    private float GetCorrectAnimation()
    {

        float currentAnimationSpeed = playerAnim.GetFloat("move");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currentAnimationSpeed < 1.5f) { currentAnimationSpeed += Time.deltaTime * 2; }
            else currentAnimationSpeed = 1.5f;
        }
        else if (currentAnimationSpeed < 1)
        {
            currentAnimationSpeed += Time.deltaTime * 2;
        }
        else
        {
            currentAnimationSpeed = 1;
        }
        playerAnim.SetFloat("move", currentAnimationSpeed);

        return currentAnimationSpeed;
    }
}