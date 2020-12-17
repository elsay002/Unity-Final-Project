using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slerp : MonoBehaviour
{
    Quaternion target;
    // Start is called before the first frame update
    void Start()
    {
     target = Quaternion.Euler(0,0,180);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,target, Time.deltaTime);
    }
}
