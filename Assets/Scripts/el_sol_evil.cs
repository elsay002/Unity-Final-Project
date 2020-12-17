using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class el_sol_evil : MonoBehaviour
{
    public Transform target;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.position,transform.position) < 5)
        {
            //transform.LookAt(target);
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
}
