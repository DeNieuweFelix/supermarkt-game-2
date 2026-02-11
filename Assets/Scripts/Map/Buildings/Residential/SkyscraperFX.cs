using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SkyscraperFX : MonoBehaviour
{
    public GameObject[] Lights = new GameObject[3];

    [SerializeField] private TimeLightSettings[] lightSettings = new TimeLightSettings[4];
    [SerializeField] private DayNightCycle.Times lastTime;
    [SerializeField] private Renderer[] buildingRenderers = new Renderer[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LightBlink());

        foreach(GameObject g in Lights)
        {
            g.SetActive(false);
        }
    }

    private IEnumerator LightBlink()
    {
        yield return new WaitForSeconds(0.5f + Random.Range(1f, 2f));

        while (true)
        {
            DayNightCycle.Times currentTime = DayNightCycle.Instance.currentTime;

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

            if(currentTime != lastTime)
            {
                StartCoroutine(ChangeLight(currentTime));
                Debug.Log("time of day changed!");
            }

            lastTime = DayNightCycle.Instance.currentTime;
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator ChangeLight(DayNightCycle.Times time)
    {
        float delay = Random.Range(1f, 4f);

        yield return new WaitForSeconds(delay);

        TimeLightSettings t = lightSettings.ToList().Find(x => x.timeBelong == time);

        foreach(Renderer r in buildingRenderers)
        {
            //skips some buildings for some variation
            if(Random.Range(0, 100) > 90) continue;

            ChangeMaterial(r.material, t);
            
            float delay2 = Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(delay2);
        }
    }

    private void ChangeMaterial(Material mat, TimeLightSettings tls)
    {
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", tls.color * tls.strength);
    }
}

[System.Serializable]
public class TimeLightSettings
{
    [Header("settings for emmision color")]
    public Color color;
    public float strength;
    public DayNightCycle.Times timeBelong;
}
