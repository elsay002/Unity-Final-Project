using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCSpawner : MonoBehaviour
{   
    public GameObject playerobj;
    public Transform playerstart;
    GameObject player;

    
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(playerobj, playerstart);
    }

    
}
