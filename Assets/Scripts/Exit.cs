using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public Button exitButton;
    public void ExitGame()
    {
        Application.Quit();
    }
    private void Start()
    {
        exitButton.onClick.AddListener(ExitGame);
    }
}
