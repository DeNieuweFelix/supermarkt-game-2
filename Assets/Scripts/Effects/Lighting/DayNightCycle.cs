using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    [Range(0, 2400)]
    public int time = 0;
    public Times currentTime;
    [SerializeField] private float TickTime = 0.2f;
    [SerializeField] private int TickStep = 2;
    [SerializeField] private Color[] timeColors = new Color[4];
    [SerializeField] private Light SunLight;
    [SerializeField] private Color targetColor;
    [SerializeField] private float colorChangeSpeed = 3f;

    [SerializeField] private VolumeProfile[] TimeProfiles = new VolumeProfile[4];
    [SerializeField] private Volume TimeVolume;

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

        int timeIndex = 0;

        switch (currentTime)
        {
            case Times.Morning: timeIndex = 0; break;
            case Times.Day: timeIndex = 1; break;
            case Times.Evening: timeIndex = 2; break;
            case Times.Night: timeIndex = 3; break;
        }

        Debug.Log(timeIndex);

        targetColor = timeColors[timeIndex];
        
        if (TimeVolume.profile != TimeProfiles[timeIndex])
        {
            TimeVolume.profile = TimeProfiles[timeIndex];
        }
    }
}
