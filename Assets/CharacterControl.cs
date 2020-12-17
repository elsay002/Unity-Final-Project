using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float speed;
    public float gravity;
    public float jumpHeight;
    private int doubleJump=0;
    public Transform feet;
    public LayerMask grounded;
    private Vector3 direction;
    private Vector3 walkingVelocity;
    private CharacterController controller;
    private Vector3 fallingVelocity;
    // Start is called before the first frame update
    void Start()
    {
        jumpHeight = 3.0f;
        speed = 5.0f;
        gravity = 9.8f;
        fallingVelocity = Vector3.zero;
        direction = Vector3.zero;
        walkingVelocity = Vector3.zero;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = direction.normalized; 
        if(direction != Vector3.zero)   //Keeps facing forward
        {
            transform.forward=direction;
        }
        walkingVelocity = direction * speed;     

        controller.Move(walkingVelocity * Time.deltaTime);   //To move
        
        fallingVelocity.y -= gravity* Time.deltaTime;  //Gravity
        
        bool isGrounded()
        {
            if(Physics.CheckSphere(feet.position, 0.1f,grounded))
            {
                doubleJump = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        if(Input.GetButtonDown("Jump") && (isGrounded() || doubleJump < 2))  //TO Jump
        {
            doubleJump += 1;
            fallingVelocity.y = Mathf.Sqrt(gravity * jumpHeight);
        }
        
        
        controller.Move(fallingVelocity * Time.deltaTime);

    }
}
