using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlarmBehaviour : MonoBehaviour
{

    [SerializeField] TMP_Text textField;
    bool alarmState = false;
    private void SoundAlarm()
    {
        Actions.OnAlarmSound();
        textField.text = "A palear";
        alarmState = true;
    }

    private void TurnOffAlarm() {
        Actions.OnAlarmSoundOff();
        textField.text = "!";
        alarmState = false;
    }

    public void Behaviour() {
        if (!alarmState)
            SoundAlarm();
        else
            TurnOffAlarm();
    }

}

