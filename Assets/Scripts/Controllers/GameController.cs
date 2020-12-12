using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private Animator consumer,arm;
    [SerializeField]private float BPM;
    [SerializeField] private UIController _uiController;
    public int pointsToAdd = 0;
    private int expectedInput=1,currentInput=1;
    private bool pressedCorrectly = true,expectingInput = true,breakTime= false,playingAnim = false; 
    [SerializeField]private bool[] armPieces = new bool[4];
    private int _beatCount = 0,errorMargin = 0;
    private float timer,currentTime;

    public void InputCheck(int input)
    {
        if (expectingInput == true)
        {
            if (input == expectedInput&&currentInput == expectedInput)
            {
                if (currentTime < (timer * .2f))
                {
                    expectingInput = false;
                    pointsToAdd = 100;
                }
                else if (currentTime < (timer * .7f))
                {
                    expectingInput = false;
                    pointsToAdd = 50;
                }
            }
            else if(input != expectedInput&&currentInput == expectedInput)
            {
                if (currentTime < (timer * .7f))
                {
                    pointsToAdd = 30;
                    errorMargin = 20;
                } 
            }
            else if(input == expectedInput&&currentInput != expectedInput)
            {
                if (currentTime >timer - (timer * .2f))
                {
                    pointsToAdd = 100;
                }
            }
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
        StartCoroutine(BeatTimer());
    }
    private void GameCheck()
    {
        if(currentTime<timer -(timer/10)&&expectedInput != currentInput)
        {
            if (!breakTime)
            {
                armPieces[expectedInput - 1] = true;
                expectedInput = currentInput; 
                _uiController.DisplayExpectedInput(expectedInput);
                _uiController.AddPoints(pointsToAdd-errorMargin);
                expectingInput = true;
                pointsToAdd = 0;
                errorMargin = 0;
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
        }
    }
    
    private IEnumerator BeatTimer()
    {
        while (true)
        {
            StartCoroutine(BeatShower());
            yield return new WaitForSeconds(timer);
            RandomInput();
            _uiController.ChangeImageColor(currentInput-1);
            _beatCount++;
        }
    }

    private IEnumerator ConsumerAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            arm.SetTrigger("Pullingout");
            consumer.SetTrigger("WalkOut");
            yield return new WaitForSeconds(timer);
            consumer.SetTrigger("WalkIn");
            arm.SetTrigger("PuttingIn");
            yield return new WaitForSeconds((timer*2)+(timer/10));
            clearArmPieces();
            breakTime = false;
            playingAnim = false;
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
