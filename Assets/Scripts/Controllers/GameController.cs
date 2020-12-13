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
    [SerializeField] private Animator[] Tools,Buttons;
    [SerializeField] private Animator consumer,arm,money;
    [SerializeField]private float BPM;
    [SerializeField] private UIController _uiController;
    [SerializeField]private int expectedInput=1,currentInput=1;
    [SerializeField]private bool[] armPieces = new bool[4];
    [SerializeField] private Vector3 armResetPos;
    private bool pressedCorrectly = true,expectingInput = true,breakTime= false,playingAnim = false,flyingMoney = false; 
    private int _beatCount = 0;
    private float timer,currentTime;
    public bool completedLevel;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        timer = 1 / (BPM / 60);
        StartCoroutine(StartGame());
    }
    public void InputCheck(int input)
    {
        Buttons[input-1].Play("Pressed");
        if (expectingInput == true)
        {
            Tools[input-1].SetTrigger("Activate");
            if (input == expectedInput&&currentInput == expectedInput)
            {
                if (currentTime < (timer * .2f))
                {
                    pointsToAdd = 50;
                }
                else if (currentTime < (timer * .7f))
                {
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
            _uiController.AddPoints(pointsToAdd);
            expectingInput = false;
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
    IEnumerator StartGame()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            GetComponent<AudioSource>().Play();
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
                expectingInput = true;
            }
            else
            {
                if (!playingAnim)
                {
                    StartCoroutine(ConsumerAnim());
                    playingAnim = true;
                }
                expectedInput = currentInput;
            }
            pointsToAdd = 0;
        }
    }
    
    private IEnumerator BeatTimer()
    {
        while (true)
        {
            if (_beatCount > 3)
            {
                PlayerPrefs.GetInt("HighScore",0);
                if (PlayerPrefs.GetInt("HighScore") < points)
                {
                    PlayerPrefs.SetInt("HighScore",points);
                }
                StopAllCoroutines();
                completedLevel = true;
                SceneManager.LoadScene("MainMenu");
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
            _uiController.ChangeImageColor(4);
            yield return new WaitForSeconds(timer);
            consumer.SetTrigger("Dance");
            arm.SetTrigger("Pullingout");
            yield return new WaitForSeconds(timer);
            consumer.SetTrigger("ThrowMoney");
            yield return new WaitForSeconds(timer);
            yield return new WaitUntil(() => currentTime < .9f);
            flyingMoney = true;
            money.Play("FlyingCash");
            consumer.SetTrigger("WalkOut");
            yield return new WaitForSeconds(timer);
            consumer.SetTrigger("WalkIn");
            arm.SetTrigger("PuttingIn");
            yield return new WaitForSeconds((timer)+(timer/10));
            clearArmPieces();
            breakTime = false;
            playingAnim = false;
            expectingInput = true;
            _uiController.ChangeImageColor(currentInput - 1);
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
            if (currentTime < (timer * .8f))
            {
                _uiController.ChangeIntensity(1-currentTime);
            }
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
