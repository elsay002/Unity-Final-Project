using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //for UI STUFF

public class EnemyCollision : MonoBehaviour
{   
    public Transform target;
    private float speed;
    private Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        healthText = transform.Find("Canvas").Find("Name").GetComponent<Text>();
   }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.position += transform.forward * speed * Time.deltaTime;
        healthText.text = "Evil Robot";
    }


    
}
