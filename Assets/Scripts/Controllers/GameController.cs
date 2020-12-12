using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField]private float BPM;
    [SerializeField] private UIController _uiController;
    private float timer;
    private float currentTime;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        timer = 1 / (BPM / 60);
        _uiController.SetMaxBeatValue(timer);
        StartCoroutine(BeatTimer());
    }

    public void InputCheck(int input)
    {
        
    }
    private IEnumerator BeatTimer()
    {
        while (true)
        {
            StartCoroutine(BeatShower());
            yield return new WaitForSeconds(timer);
        }
    }

    private IEnumerator BeatShower()
    {
         currentTime = timer;
        while(currentTime>0)
        {
            currentTime -= Time.deltaTime;
            _uiController.DisplayBeat(currentTime);
            yield return new WaitForSeconds(0);
        }
    }
}
