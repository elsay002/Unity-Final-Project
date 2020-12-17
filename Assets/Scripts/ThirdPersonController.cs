using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;

    public float gravity;
    public float jumpHeight;
    private int doubleJump=0;
    public Transform feet;
    public LayerMask grounded;
    private Vector3 fallingVelocity;

    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        jumpHeight = 3.0f;
        gravity = 9.8f;
        fallingVelocity = Vector3.zero;        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x , direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        fallingVelocity.y -= gravity* Time.deltaTime;

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

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            
            PlayerPrefs.SetString("lastScene",SceneManager.GetActiveScene().name);
            
        }
    }

    
}
