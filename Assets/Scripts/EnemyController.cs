using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //for UI STUFF

public class EnemyController : MonoBehaviour
{   
    public Transform target;
    private float speed;
    private float health;
    private Text healthText;
    private Image healthbar;
    private float maxHealth;
    public AudioClip audio;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        health  =100.0f;
        healthText = transform.Find("Canvas").Find("Name").GetComponent<Text>();
        maxHealth = 100.0f;
        healthbar = transform.Find("Canvas").Find("Health2").GetComponent<Image>();

        GameObject hero = GameObject.FindGameObjectWithTag("Player");
   }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.position,transform.position) < 5)
        {
            transform.LookAt(target);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    
        healthText.text = "Evil Robot Cube";
        healthbar.fillAmount = health / maxHealth;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            health -= 10.0f;
            if(health <=0)
            {
                AudioSource.PlayClipAtPoint(audio, GameObject.Find("Robot").transform.position, 1);
                Destroy(this);
                Destroy(gameObject);
            }
            Destroy(col.gameObject);
        }
    }
}
