using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]private Slider BeatShower;
    [SerializeField] private TextMeshProUGUI expectedInput;
    [SerializeField] private TextMeshProUGUI currentPoints;
    [SerializeField] private Image IndicatorImage;
    [SerializeField] private Color[] ColorPallete;
    private int _points = 0;
    public void DisplayBeat(float currentTime)
    {
        BeatShower.value = currentTime;
    }

    public void SetMaxBeatValue(float maxValue)
    {
        BeatShower.maxValue = maxValue;
    }

    public void DisplayExpectedInput(int expInput)
    {
        expectedInput.text ="Expected Input: "+ expInput.ToString();
    }

    public void AddPoints(int pointsToAdd)
    {
        GameController.instance.points = _points;
        _points += pointsToAdd;
        currentPoints.text =  _points.ToString();
    }

    public void ChangeImageColor(int index)
    {
        IndicatorImage.color = ColorPallete[index];
        IndicatorImage.gameObject.GetComponent<Light2D>().intensity = .3f;
    }

    public void ChangeIntensity(float intensity)
    {
        IndicatorImage.gameObject.GetComponent<Light2D>().intensity = intensity;
    }
}
