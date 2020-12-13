using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI expectedInput;
    [SerializeField] private TextMeshProUGUI currentPoints;
    [SerializeField] private Image IndicatorImage;
    [SerializeField] private Color[] ColorPallete;
    [SerializeField] private TextMeshProUGUI[] ScoreTexts;
    [SerializeField] private Vector3 PointsDisplayTarget;
    private int currTextIndex;
    private int _points = 0;
    public void DisplayExpectedInput(int expInput)
    {
        expectedInput.text ="Expected Input: "+ expInput.ToString();
    }

    public void AddPoints(int pointsToAdd)
    {
        GameController.instance.points = _points;
        _points += pointsToAdd;
        ScoreTexts[currTextIndex].text = pointsToAdd.ToString();
        ShowScore(currTextIndex);
        if (currTextIndex+1 >= ScoreTexts.Length)
        {
            currTextIndex = 0;
        }
        else
        {
            currTextIndex++;
        }
        currentPoints.text =  _points.ToString();
        
    }

    public void ChangeImageColor(int index)
    {
        IndicatorImage.color = ColorPallete[index];
        IndicatorImage.gameObject.GetComponent<Light2D>().intensity = .3f*2;
    }

    public void ChangeIntensity(float intensity)
    {
        IndicatorImage.gameObject.GetComponent<Light2D>().intensity = intensity*2;
    }

    public void ShowScore(int index)
    {
        ScoreTexts[index].transform.position = PointsDisplayTarget;
        StartCoroutine(ThrowPointsUp(index));
    }

    IEnumerator ThrowPointsUp(int index)
    {
        var timesThrown = 10;
        var currThrownIndex = 0;
        ScoreTexts[index].alpha = 1;
        while (currThrownIndex<timesThrown)
        {
            ScoreTexts[index].transform.position += new Vector3(0,30*Time.deltaTime,0);
            ScoreTexts[index].alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
            currThrownIndex++;
        }
    }
}
