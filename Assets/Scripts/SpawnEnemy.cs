using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{
   
    void Start()
    {
        
    }
   
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            
            SceneManager.LoadScene("demoScene_free");
            Destroy(this.gameObject);
        }
    }
    
}
