using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
   // Start is called before the first frame update
    void Start()
    {
        speed = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            transform.Translate(new Vector3(1,0,0) * Time.deltaTime * speed);
        }
    }
    
}
