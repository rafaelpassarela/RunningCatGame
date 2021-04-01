using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// stoped at 7:12
public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;
    public float runSpeed = 8f;
    public float jumpHeigth = 2f;

    private float gravity = -50f;
    private float horizontalInput;
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isPaused = true;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // check for pause menu
        if (Input.GetButtonDown("Submit"))
        {
            isPaused = !isPaused;
        }

        // if paused does nothing
        if (isPaused)
        {
            return;
        }

        horizontalInput = 1f;
        // face foward
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayer, QueryTriggerInteraction.Ignore);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        } else
        {
            // add gravity
            velocity.y += gravity * Time.deltaTime;
        }

        // move foward
        characterController.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeigth * -2 * gravity);
        }

        // vertical movement
        characterController.Move(velocity * Time.deltaTime);
    }
}
