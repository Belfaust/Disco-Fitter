using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    MenuMenager menuMenager;
    [SerializeField]
    float delayyy;
    [SerializeField]
    float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ButtonAnimation());
        delay = menuMenager.delay;
        delayyy = menuMenager.delayyy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ButtonAnimation()
    {

        TMP_Text wholeText = GetComponentInChildren<TMP_Text>();
        char[] textarray = wholeText.text.ToCharArray();
        string mem;
        while (true)
        {
        switch(wholeText.text)
        {
            case "Play":
                wholeText.text = "Q";
                yield return new WaitForSeconds(delayyy);

                wholeText.text = "_";
                yield return new WaitForSeconds(delayyy);
                wholeText.text = "";
                for (int i = 0; i < textarray.Length; i++)
                {
                    wholeText.text += textarray[i];
                    mem = wholeText.text;
                    wholeText.text += "_";
                    yield return new WaitForSeconds(delay);
                }

                break;
            case "Settings":
                wholeText.text = "W";
                break;
            case "Credits":
                wholeText.text = "E";
                break;
            case "Exit":
                wholeText.text = "R";
                break;
        }
        }
        wholeText.text = "XD";

        //string wholetext = GetComponentInChildren<TMP_Text>().text.ToString();
        
        //char[] textarray = wholetext.ToCharArray();

        yield return null;
    }
}
