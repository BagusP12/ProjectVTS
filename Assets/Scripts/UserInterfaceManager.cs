using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterfaceManager : MonoBehaviour {

    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;

    //Timer
    float startTime;
    float minutes;
    float seconds;

    private void Update()
    {
        //Timer
        float t = Time.time - startTime;
        minutes = (int)t / 60;
        seconds = t % 60;
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

}
