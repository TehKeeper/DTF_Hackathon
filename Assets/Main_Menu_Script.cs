﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _exitApp()
    {
        Application.Quit();
    }

    public void _launchGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
