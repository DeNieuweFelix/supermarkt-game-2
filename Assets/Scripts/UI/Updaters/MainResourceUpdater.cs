using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainResourceUpdater : MonoBehaviour
{
    public static MainResourceUpdater Instance;

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

    [SerializeField] private TextMeshProUGUI populationDisplay;
    [SerializeField] private Image populationImage;
    public List<ResourceText> allResourceText = new List<ResourceText>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AutoUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAll()
    {
        GameStats stats = GameManager.Instance.stats;
        populationDisplay.text = stats.CurrentResidents + " / " + stats.MaxResidents;
    }

    public void UpdatePopulationColor(int difference)
    {
        if(difference > 50)
        {
            populationImage.color = Color.purple;
        }else if(difference > 25)
        {
            populationImage.color = Color.blue;
        }else if(difference > 10)
        {
            populationImage.color = Color.turquoise;
        }
        else
        {
            populationImage.color = Color.green;
        }

        StartCoroutine(PopulationIncreaseEffect(difference));
    }

    private IEnumerator PopulationIncreaseEffect(int increase)
    {
        RectTransform rect = populationImage.gameObject.GetComponent<RectTransform>();

        int way = -1;
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                float a = Mathf.Floor(Mathf.Clamp(increase / 10, 0f, 10f));
                rect.sizeDelta += new Vector2(1f + a, 1f + a) * way;
                yield return new WaitForFixedUpdate();
            }

            if(way == -1)
            {
                way = 1;
            }
        }
    }

    private IEnumerator AutoUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            UpdateAllResources();
        }
    }

    public void UpdateAllResources()
    {
        //returns if the instance isnt loaded yet
        if(Player.Instance.playerStats == null) return;
        if(allResourceText.Count == 0) return;

        foreach(MaterialOwned mO in Player.Instance.playerStats.MaterialsOwned)
        {
            int c = 0;

            p_Material m = mO.material;
            ResourceText rT = allResourceText.Find(x => x.name == m.name);

            rT.UpdateT(mO.amount);
        }
    }
}

[System.Serializable]
public class ResourceText
{
    public string name;
    public TextMeshProUGUI textHolder;

    public int oldValue;
    //float just te be sure (make sure to convert it when calling)
    public void UpdateT(float amount){
        textHolder.text = amount.ToString();
        int newValue = Int32.Parse(textHolder.text);

        if(newValue > oldValue)
        {
            textHolder.color = Color.lightGreen;
        }else if(newValue < oldValue)
        {
            textHolder.color = Color.red;
        }
        else
        {
            textHolder.color = Color.white;
        }

        oldValue = Int32.Parse(textHolder.text);
    }
}
