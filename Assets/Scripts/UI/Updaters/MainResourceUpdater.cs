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
            populationImage.color = Color.darkBlue;
        }else if(difference > 10)
        {
            populationImage.color = Color.blue;
        }
    }
}
