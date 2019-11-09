using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatorUtility;

public class Canvas_Script : MonoBehaviour
{
    // Start is called before the first frame update

    public int
        stepCounter=10,
        jumpCounter=10,
        rotCounter=10;
    Text 
        stepCountText,
        rotCountText,
        jumpCountText,
        purchText,
        purchPointsText,
        videoPointsText;

    GameObject purchasePanel;

    AudioSource aud;

    UtilityOne ugo = new UtilityOne();

    void Start()
    {
        stepCountText = ugo.FindTransform("StepCounterText", transform).GetComponent<Text>();
        rotCountText = ugo.FindTransform("RotCounterText", transform).GetComponent<Text>();
        jumpCountText = ugo.FindTransform("JumpCounterText", transform).GetComponent<Text>();

        purchText = ugo.FindTransform("Purch_Text", transform).GetComponent<Text>();
        purchPointsText = ugo.FindTransform("PurchPoints_Text", transform).GetComponent<Text>();
        videoPointsText = ugo.FindTransform("VideoPoints_Text", transform).GetComponent<Text>();

        purchasePanel = ugo.FindTransform("Purchase_Panel", transform).gameObject;
        aud = purchasePanel.GetComponent<AudioSource>();

        updateValuesText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    /// <summary>
    /// Обновляет значения в скрипте для счетчиков шагов, вращения и прыжков
    /// </summary>
    /// <param name="step">шаги</param>
    /// <param name="rot">вращение</param>
    /// <param name="jmp">прыжки</param>
    public void updateValues(int step, int rot,int jmp)
    {
        stepCounter -= step;
        rotCounter -= rot;
        jumpCounter -= jmp;

        if (stepCounter <= 0 || rotCounter <= 0 || jumpCounter <= 0)
            openPurchMenu();

        //Debug.LogFormat("Values: {0}, stepCounter: {1}, rotCounter {2}, jumpCounter {3}", values, stepCounter, rotCounter, jumpCounter);
        //Debug.LogFormat("Values: {0}, values.x: {1}, values.y {2}, jumpCounter {3}", values, stepCounter, rotCounter, jumpCounter);

        updateValuesText();

    }

    private void updateValuesText()
    {
        stepCountText.text = stepCounter.ToString();
        rotCountText.text = rotCounter.ToString();
        jumpCountText.text = jumpCounter.ToString();
    }



    [ContextMenu("Open Purchase Menu")]
    public void openPurchMenu()
    {

        Time.timeScale = 0.00001f;

        purchasePanel.SetActive(true);
        playMusic();

        purchText.text = "Кончились " + getReqPoints() + "? Не беда!";
        purchPointsText.text = "Купить " + getReqPoints(14);
        videoPointsText.text = "Посмотреть видео за " + getReqPoints(14);

    }

    string getReqPoints(int fontSize=40)
    {
        string outp = "";
        List<string> names = new List<string>();
        if (stepCounter <= 0)
            names.Add("ШАГИ");
        if (rotCounter <= 0)
            names.Add("КРУГИ");
        if (jumpCounter <= 0)
            names.Add("ПРЫГИ");

        if (names.Count == 1)
            outp = names[0];
        else if (names.Count == 2)
            outp = names[0] + " и " + names[1];
        else if (names.Count == 3)
            outp = names[0] + ", " + names[1] + " и " + names[2];

        /*
        if(names.Count>2)
            for (int i = 1; i < names.Count; i++)
            {
                outp = names[i] + (i < names.Count - 1 ? ", " : " и ");
            }
        else
        {
            outp= names[0];
        }
        */


        return "<size="+fontSize+"> "+outp+" </size>";
    }


    void playMusic()
    {
        float[] times = new float[] { 11f, 39f };


        aud.time = times[Random.Range(0,times.GetLength(0))];
        aud.Play();
    }


    public void _openVideo(int ptsAdd)
    {
        string[] urls = new string[] 
            {
            "https://www.youtube.com/watch?v=dQw4w9WgXcQ?autoplay=1",
            "https://www.youtube.com/watch?v=QH2-TGUlwu4?autoplay=1",
            "https://www.youtube.com/embed/G1IbRujko-A?autoplay=1",
            "https://www.youtube.com/watch?v=kffacxfA7G4?autoplay=1&amp;loop=1&amp;&amp;playlist=Video_ID"
            
            };

        if (stepCounter <= 0)
            stepCounter += ptsAdd;
        if (rotCounter <= 0)
            rotCounter += ptsAdd;
        if (jumpCounter <= 0)
            jumpCounter += ptsAdd;

        updateValuesText();

        Application.OpenURL(urls[Random.Range(0,urls.GetLength(0))]);
    }

    public void _buySkyrim()
    {
        Application.OpenURL("https://elderscrolls.bethesda.net/en/skyrim");
    }

    public void _closePanel()
    {
        purchasePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
