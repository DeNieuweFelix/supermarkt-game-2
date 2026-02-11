using System.Collections;
using TMPro;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
