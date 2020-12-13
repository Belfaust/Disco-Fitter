using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public TextMeshProUGUI ScoreDisplay;
    
    private MenuMenager newMAnager;
    private void Awake()
    {
        newMAnager = FindObjectOfType<MenuMenager>();
        if (GameController.instance.completedLevel)
        {
            ScoreDisplay.text = "Score: " +GameController.instance.points + "!";
        }
        Destroy(GameController.instance);
    }
}
