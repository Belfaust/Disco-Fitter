using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    MenuMenager menuMenager;
    float delayyy;
    float delay;
    [SerializeField]
    string Key;

    // Start is called before the first frame update
    void Start()
    {
        delayyy = menuMenager.delayyy;
        delay = menuMenager.delay;
        StartCoroutine(ButtonAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ButtonAnimation()
    {

        TMP_Text wholeText = GetComponentInChildren<TMP_Text>();
        char[] textarray = wholeText.text.ToCharArray();
        List<char> textList = new List<char>();
        textList.AddRange(wholeText.text);
        string mem = "?";

        
        
        //if (wholeText.text == "Play")
        //{
            /*float memsize = wholeText.fontSize;
            wholeText.fontSize = 50;
            wholeText.text = Key;
            yield return new WaitForSeconds(delayyy);
            wholeText.text = "_";
            yield return new WaitForSeconds(delayyy / 2);
            wholeText.text = "";

            textList.Add(':');
            textList.Add('B');
            textList.Add('i');
            textList.Add('g');

            for (int i = 0; i < textList.Count; i++)
            {
                wholeText.text += textList[i];
                mem = wholeText.text;
                wholeText.text += "_";
                yield return new WaitForSeconds(delay);

                if (i == textList.Count - 1)
                {
                    yield return new WaitForSeconds(delayyy/2);
                }
                wholeText.text = mem;
            }
            wholeText.fontSize = memsize;

            wholeText.text = "Play";*/

        //}
        //else
        ///{
            wholeText.text = Key;
            yield return new WaitForSeconds(delayyy);
            wholeText.text = "_";
            yield return new WaitForSeconds(delayyy / 2);
            wholeText.text = "";

            for (int i = 0; i < textarray.Length; i++)
            {
                wholeText.text += textarray[i];
                mem = wholeText.text;
                wholeText.text += "_";
                yield return new WaitForSeconds(delay);
                if (i == textarray.Length - 1)
                {
                    yield return new WaitForSeconds(delay);
                }
                wholeText.text = mem;
            }

            wholeText.text = mem;
        //}

        yield return null;
    }
}
