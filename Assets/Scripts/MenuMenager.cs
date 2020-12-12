using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMenager : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsPanel;
    [SerializeField]
    public float delayyy;
    [SerializeField]
    public float delay;
    private bool isCreditsPanel = false;

    // Start is called before the first frame update
    void Start()
    {
        creditsPanel.SetActive(false);
        isCreditsPanel = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnterGameplay()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCloseSettings()
    {

    }

    public void RollCredits()
    {
        if(isCreditsPanel == false)
        {
            creditsPanel.SetActive(true);
            isCreditsPanel = true;
        }
        else
        {
            creditsPanel.SetActive(false);
            isCreditsPanel = false;
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
