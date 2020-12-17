using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate2 : MonoBehaviour
{
    public GameObject obj;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Instantiate(obj,GameObject.Find("Cube (1)").transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,-1));
        transform.Translate(new Vector3(-2,0,0) * Time.deltaTime, Space.World);
        
    }
}
