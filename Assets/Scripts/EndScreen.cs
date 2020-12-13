using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public GameObject Credits;
    public TextMeshProUGUI ScoreDisplay,HighScore;

    private void Start()
    {
        StartCoroutine(EndSequence());
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Destroy(GameController.instance.gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator EndSequence()
    {
        StartCoroutine(DisplayPoints());
        yield return new WaitForSeconds(2f);
        while ( Credits.transform.position.y >  -350)
        {
            
            Credits.transform.position -= new Vector3(0,100*Time.deltaTime,0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator DisplayPoints()
    {
        while (true)
        {
            var currPoints =GameController.instance.points;
            ScoreDisplay.text = "Your Earned Points : " + currPoints.ToString();
            yield return new WaitForSeconds(.5f);
            if (currPoints > PlayerPrefs.GetInt("HighScore"))
            {
                HighScore.text = "You Set New HighScore!\n It's " + currPoints +" now!";
            }
            else
            {
                HighScore.text = "Current HighScore is: "+ PlayerPrefs.GetInt("HighScore");
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}