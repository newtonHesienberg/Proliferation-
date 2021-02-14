using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    CharacterController characterController;
    Animator playerAnim;
    Player player;

    float originalColliderHeight;
    float originalCentreHeight;

    public float reducedColliderHeight;
    public float centreHeight;

    public bool isCrouched = false;

    private void Start()
    {
        player = GetComponent<Player>();
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalColliderHeight = characterController.height;
        originalCentreHeight = characterController.center.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            Crouch();
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            GetUp();

        if(isCrouched)
        {
            player.speed = 2.5f;
        }
    }

    private void Crouch()
    {
        isCrouched = true;

        playerAnim.SetBool("isCrouched", true);
        characterController.height = reducedColliderHeight;
        characterController.center = new Vector3(0, centreHeight, 0);
    }

    private void GetUp()
    {
        isCrouched = false;
        characterController.height = originalColliderHeight;
        characterController.center = new Vector3(0, originalCentreHeight, 0);
        player.speed = 6f;
        playerAnim.SetBool("isCrouched", false);
    }
}
