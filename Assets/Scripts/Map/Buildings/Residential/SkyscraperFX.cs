using System.Collections;
using UnityEngine;

public class SkyscraperFX : MonoBehaviour
{
    public GameObject[] Lights = new GameObject[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LightBlink());

        foreach(GameObject g in Lights)
        {
            g.SetActive(false);
        }
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    private IEnumerator LightBlink()
    {
        yield return new WaitForSeconds(0.5f + Random.Range(1f, 2f));

        while (true)
        {
            
            if(Random.Range(0, 30) > 25 && (
                DayNightCycle.Instance.currentTime == DayNightCycle.Times.Night ||
                DayNightCycle.Instance.currentTime == DayNightCycle.Times.Evening
                )
            )
            {
                GameObject lightActivated = Lights[Random.Range(0, Lights.Length)];
                lightActivated.SetActive(true);

                float delay = Random.Range(0.15f, 1.15f);

                yield return new WaitForSeconds(delay);
                lightActivated.SetActive(false);
            }
            else
            {
                foreach(GameObject l in Lights)
                {
                    l.SetActive(false);
                }    
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
