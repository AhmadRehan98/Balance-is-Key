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
    private string minutes, seconds, timerText;
    private float flashingTime;
    private bool flashTimer;
    // Start is called before the first frame update
    void Start()
    {
        PauseTimer = false;
        startTime = Time.time;
        NumResets = 0;
        flashingTime = 0.75f;
        flashTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseTimer)
        {
            currentTime = Time.time - startTime;
            minutes = ((int) currentTime / 60).ToString("00");
            seconds = (currentTime % 60).ToString("f2");
            timerText = "Timer:   " + minutes + ":" + (currentTime % 60).ToString("00") + ":" +
                        seconds.Substring(seconds.Length - 2);
            TimerText.text = timerText;
            
            ResetsText.text = "Resets: " +  NumResets;
        }
        else
        {
            if (!flashTimer)
            {
                StartCoroutine(FlashTimer(flashingTime));
                flashTimer = !flashTimer;
            }
        }

        // ResetsText.text = "Resets: " + ((NumResets == 0) ? "" : NumResets.ToString());
        
    }

    IEnumerator FlashTimer(float time)
    {
        while (true)
        {
            Debug.Log("Flashing timer");
            // Debug.Log(timerText);
            TimerText.text = "";
            ResetsText.text = "";
            yield return new WaitForSeconds(time/2);
            TimerText.text = timerText;
            ResetsText.text = "Resets: " +  NumResets;
            yield return new WaitForSeconds(time);
        }
    }
}
