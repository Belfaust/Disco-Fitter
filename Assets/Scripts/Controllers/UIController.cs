using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]private Slider BeatShower;

    public void DisplayBeat(float currentTime)
    {
        Debug.Log(currentTime);
        BeatShower.value = currentTime;
    }

    public void SetMaxBeatValue(float maxValue)
    {
        BeatShower.maxValue = maxValue;
    }
    void Update()
    {
        
    }
}
