using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public int buttonWidth;
    public int buttonHeight;
    private int origin_x;
    private int origin_y;

    // Start is called before the first frame update
    void Start()
    {
        buttonHeight = 50;
        buttonWidth = 200;    
        origin_x  = Screen.width/2 - buttonWidth /2;
        origin_y = Screen.height / 2 - buttonHeight *2;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(origin_x,origin_y, buttonWidth, buttonHeight), "Start Game"))
        {
            SceneManager.LoadScene("LevelStart");
        }

        if (GUI.Button(new Rect(origin_x,origin_y + buttonHeight + 20, buttonWidth, buttonHeight), "Quit"))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif    
            
        }
    }
}
