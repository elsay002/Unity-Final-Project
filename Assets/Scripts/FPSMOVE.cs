using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMOVE : MonoBehaviour
{

    public float speed;
    private Vector3 direction;
    private Rigidbody rbody;
    private float rotationSpeed;
    private float rotationX;
    private float rotationY;

    public float jumpHeight;
    private int doubleJump=0;
    public Transform feet;
    public LayerMask grounded;

    public GameObject BulletObj;
    public Transform BulletSpawn;
    private AudioSource gunfire;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
        rotationSpeed = 1f;
        rotationX = 0;
        rotationY = 10f;
        rbody = GetComponent<Rigidbody>();

        jumpHeight = 5.0f;
        doubleJump=0;
        gunfire = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = direction.normalized;
        if(direction.x != 0)
        {
            rbody.MovePosition(rbody.position + transform.right * direction.x * speed * Time.deltaTime);
        }

        if(direction.z != 0)
        {
            rbody.MovePosition(rbody.position + transform.forward * direction.z * speed * Time.deltaTime);
        }
        
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") *rotationSpeed;
        transform.localEulerAngles = new Vector3(-rotationY, rotationX,0);

        bool isGrounded()//jumping
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

        if(Input.GetButtonDown("Jump") && (isGrounded() || doubleJump <2))//jumping
        {
            doubleJump += 1;
            rbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        }


        if(Input.GetButtonDown("Fire1"))
        {
            gunfire.Play();
            Fire();
        }
    }

    void Fire()
    {
      var Bullet = (GameObject)Instantiate(BulletObj, BulletSpawn.position, BulletSpawn.rotation); 
      Bullet.GetComponent<Rigidbody>().velocity = Bullet.transform.forward * 12; 
      Destroy(Bullet,2.0f);  
    }
}
