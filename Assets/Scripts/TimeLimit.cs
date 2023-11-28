using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
    public TMP_Text gameTimeUI;
    public HP playerHP;
    float setTime = 300;
    int min;
    float sec;

    void Update()
    {
        setTime -= Time.deltaTime;

        if (setTime > 0)
        {
            min = (int)setTime / 60;
            sec = setTime % 60;
            gameTimeUI.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
        }

        if (setTime <= 0)
        {
            gameTimeUI.text = "00:00";
            playerHP.amount = 0;
            return;
        }
    }
}
