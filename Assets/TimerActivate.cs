using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerActivate : MonoBehaviour
{
    public GameObject timerUI;
    public TMP_Text timer;
    float rawTime = 0;
    [SerializeField] int countdownTimer = 40;
    int timeRemaining;
    string displayTime;

    // Update is called once per frame
    void Update()
    {
        if(timerUI.activeSelf)
        {
            rawTime += Time.deltaTime;

            if(Mathf.Floor(rawTime) >= 1)
            {
                countdownTimer--;
                if(countdownTimer >= 10)
                {
                    timer.SetText("0:" + countdownTimer.ToString());
                }
                else if(countdownTimer < 10 && countdownTimer >= 0)
                {
                    timer.SetText("0:0" + countdownTimer.ToString());
                }
                else
                {
                    timer.SetText("BOOM");
                }

                rawTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!timerUI.activeSelf)
        {
            timerUI.SetActive(true);
            timer.SetText("1:00");
        }
    }
}
