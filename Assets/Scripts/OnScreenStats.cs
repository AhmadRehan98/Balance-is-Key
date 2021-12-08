using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenStats : MonoBehaviour
{
    public static bool PauseTimer;
    public static int NumResets;
    public TextMeshProUGUI TimerText, ResetsText;
    private float startTime, currentTime;
    private string minutes, seconds;
    // Start is called before the first frame update
    void Start()
    {
        PauseTimer = false;
        startTime = Time.time;
        NumResets = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseTimer)
        {
            currentTime = Time.time - startTime;
            minutes = ((int) currentTime / 60).ToString("00");
            seconds = (currentTime % 60).ToString("f2");
            TimerText.text = "Timer:   " + minutes + ":" + (currentTime % 60).ToString("00") + ":" +
                             seconds.Substring(seconds.Length - 2);
        }

        // ResetsText.text = "Resets: " + ((NumResets == 0) ? "" : NumResets.ToString());
        ResetsText.text = "Resets: " +  NumResets;
    }
}
