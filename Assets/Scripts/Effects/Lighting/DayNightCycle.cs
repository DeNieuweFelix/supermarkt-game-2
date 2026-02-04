using System;
using System.Collections;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0, 2400)]
    public int time = 0;
    public Times currentTime;
    [SerializeField] private float TickTime = 0.2f;
    [SerializeField] private int TickStep = 2;
    [SerializeField] private Color[] timeColors = new Color[4];
    [SerializeField] private Light SunLight;
    [SerializeField] private Color targetColor;
    [SerializeField] private float colorChangeSpeed = 3f;

    public enum Times
    {
        Morning = 600,
        Day = 1200,
        Evening = 1800,
        Night = 2400
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TimeTicker());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(SunLight.color != targetColor)
        {
            SunLight.color = Color.Lerp(SunLight.color, targetColor, colorChangeSpeed * Time.deltaTime);
        }
    }

    private IEnumerator TimeTicker()
    {
        while (true)
        {
            time += TickStep;

            if(time > 2400)
            {
                time = 0;
            }
            
            CheckCurrentTime();

            yield return new WaitForSeconds(TickTime);
        }
    }

    private void CheckCurrentTime()
    {
        if (time < (int)Times.Morning)
        {
            currentTime = Times.Night;
        }
        else if (time < (int)Times.Day)
        {
            currentTime = Times.Morning;
        }
        else if (time < (int)Times.Evening)
        {
            currentTime = Times.Day;
        }
        else
        {
            currentTime = Times.Evening;
        }

        targetColor = timeColors[((int)currentTime / 600) - 1];
    }
}
