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
    [SerializeField]
    public GameObject canvas;
    [SerializeField]
    public GameObject artcanvas;
    [SerializeField]
    public GameObject wrench;
    [SerializeField]
    public GameObject discoboi;
    [SerializeField]
    public GameObject torsodiscoboi;

    // Start is called before the first frame update
    void Start()
    {
        
        artcanvas.SetActive(false);
        artcanvas.SetActive(true);
        canvas.SetActive(false);
        canvas.SetActive(true);
        wrench.SetActive(false);
        wrench.SetActive(true);
        discoboi.SetActive(false);
        discoboi.SetActive(true);
        torsodiscoboi.SetActive(false);
        torsodiscoboi.SetActive(true);

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
        if (isCreditsPanel == false)
        {
            creditsPanel.SetActive(true);
            isCreditsPanel = true;
            discoboi.SetActive(false);
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

    private IEnumerator Reset()
    {
        yield return new WaitForFixedUpdate();
        artcanvas.SetActive(false);
        artcanvas.SetActive(true);
        yield return new WaitForFixedUpdate();
        canvas.SetActive(false);
        canvas.SetActive(true);
        yield return new WaitForFixedUpdate();
    }
}
