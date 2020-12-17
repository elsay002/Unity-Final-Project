using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnGUI()
    {
        if(GUI.Button(new Rect(20,20,200,100), "Menu"))
        {
            Application.LoadLevel(0);
        }
    }
}
