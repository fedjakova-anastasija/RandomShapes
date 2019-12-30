using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float maxTimerValue;
    private float currentTimerValue;
    private bool isActive;

    private void Awake()
    {
        isActive = false;
    }

    public void SetMaxTime(float value)
    {
        maxTimerValue = value;
    }

    public void ResetTimer()
    {
        isActive = false;
        currentTimerValue = maxTimerValue;
    }

    public int GetCurrentTime()
    {
        return (int)currentTimerValue;
    }

    public void StartTick()
    {
        isActive = true;
    }

    public void StopTick()
    {
        isActive = false;
    }

    private void Update()
    {
        if (!isActive) return;
        currentTimerValue -= Time.deltaTime;
        if (currentTimerValue <= 0) currentTimerValue = 0;
    }
}
