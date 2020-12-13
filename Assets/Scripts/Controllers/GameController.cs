using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int pointsToAdd = 0,points;
    [SerializeField] private Animator[] Tools;
    [SerializeField] private Animator consumer,arm;
    [SerializeField]private float BPM;
    [SerializeField] private UIController _uiController;
    [SerializeField]private int expectedInput=1,currentInput=1;
    [SerializeField]private bool[] armPieces = new bool[4];
    [SerializeField] private Vector3 armResetPos;
    private bool pressedCorrectly = true,expectingInput = true,breakTime= false,playingAnim = false,flyingMoney = false; 
    private int _beatCount = 0;
    private float timer,currentTime;

    public void InputCheck(int input)
    {
        if (expectingInput == true)
        {
            Tools[input-1].SetTrigger("Activate");
            if (input == expectedInput&&currentInput == expectedInput)
            {
                if (currentTime < (timer * .2f))
                {
                    expectingInput = false;
                    pointsToAdd = 50;
                }
                else if (currentTime < (timer * .7f))
                {
                    expectingInput = false;
                    pointsToAdd = 40;
                }
            }
            else if(input != expectedInput&&currentInput == expectedInput)
            {
                if (currentTime < (timer * .7f))
                {
                    pointsToAdd = 30;
                } 
            }
            else if(input == expectedInput&&currentInput != expectedInput)
            {
                if (currentTime >timer - (timer * .2f))
                {
                    pointsToAdd = 50;
                }
            }
        }
    }
    public void MoneyCheck()
    {
        if (flyingMoney)
        {
            if (currentTime < (timer * .2f))
            {
                _uiController.AddPoints(40);
            }
            else if (currentTime < (timer * .7f))
            {
                _uiController.AddPoints(10);
            }
            flyingMoney = false;
        }
    }
    bool ArmCheck()
    {
        int armPiecesPlaced = 0;
        for (int i = 0; i < armPieces.Length; i++)
        {
            if (armPieces[i] == true)
            {
                armPiecesPlaced++;
            }
        }
        if (armPiecesPlaced == armPieces.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
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
        _uiController.DisplayExpectedInput(expectedInput);
        _uiController.AddPoints(0);
        timer = 1 / (BPM / 60);
        _uiController.SetMaxBeatValue(timer);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            _uiController.ChangeImageColor(currentInput - 1);
            StartCoroutine(BeatTimer());
            yield break;
        }
    }
    private void GameCheck()
    {
        if(currentTime<timer -(timer/10)&&expectedInput != currentInput)
        {
            if (flyingMoney)
            {
                _uiController.AddPoints(10);
                flyingMoney = false;
            }
            if (!breakTime)
            {
                armPieces[expectedInput - 1] = true;
                expectedInput = currentInput; 
                _uiController.DisplayExpectedInput(expectedInput);
                expectingInput = true;
            }
            else
            {
                if (!playingAnim)
                {
                    StartCoroutine(ConsumerAnim());
                    playingAnim = true;
                }
                _uiController.DisplayExpectedInput(0);
                expectedInput = currentInput;
            }
            _uiController.AddPoints(pointsToAdd);
            pointsToAdd = 0;
        }
    }
    
    private IEnumerator BeatTimer()
    {
        while (true)
        {
            if (_beatCount > 125)
            {
                PlayerPrefs.GetInt("HighScore",0);
                if (PlayerPrefs.GetInt("HighScore") < points)
                {
                    PlayerPrefs.SetInt("HighScore",points);
                }
                StopAllCoroutines();
            }
            StartCoroutine(BeatShower());
            yield return new WaitForSeconds(timer);
            RandomInput();
            if (!breakTime)
            {
                _uiController.ChangeImageColor(currentInput - 1);
            }
            _beatCount++;
        }
    }

    private IEnumerator ConsumerAnim()
    {
        while (true)
        {
            flyingMoney = true;
            yield return new WaitForSeconds(timer);
            arm.SetTrigger("Pullingout");
            consumer.SetTrigger("WalkOut");
            yield return new WaitForSeconds(timer);
            _uiController.ChangeImageColor(4);
            consumer.SetTrigger("WalkIn");
            arm.SetTrigger("PuttingIn");
            yield return new WaitForSeconds((timer*2)+(timer/10));
            clearArmPieces();
            breakTime = false;
            playingAnim = false;
            expectingInput = true;
            _uiController.ChangeImageColor(currentInput - 1);
            _uiController.DisplayExpectedInput(currentInput);
            yield break;
        }
    }
    private IEnumerator BeatShower()
    {
        var CurrentTime = timer; // Time.deltaTime bugs man....
        while(CurrentTime>0)
        {
            CurrentTime -= Time.deltaTime;
            currentTime = CurrentTime;
            _uiController.DisplayBeat(CurrentTime);
            GameCheck();
            yield return null;
        }
    }

    private void clearArmPieces()
    {
        for (int i = 0; i < armPieces.Length; i++)
        {
            armPieces[i] = false;
        }
    }
    private void RandomInput()
    {
        while(expectedInput == currentInput)
        {
            var rand = Random.Range(1, 5);
            armPieces[expectedInput-1] = true;
            if (ArmCheck())
            {
                if (rand != expectedInput)
                {
                    currentInput = rand;
                    breakTime = true;
                    break;
                }
            }
            armPieces[expectedInput-1] = false;
            if(!armPieces[rand-1])
            {
                currentInput = rand;
            }
        }
    }
}
