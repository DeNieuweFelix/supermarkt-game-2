using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowBuildingStats : MonoBehaviour
{
    public static ShowBuildingStats Instance;

    [Header("Text fields:")]
    [SerializeField] private TextMeshProUGUI nameTXT;
    [SerializeField] private ResourceCostHolder[] holders = new ResourceCostHolder[4];


    public Sprite emptySprite;

    [SerializeField] private Sprite residentialIcon;
    [SerializeField] private Sprite productiveIcon;
    [SerializeField] private Sprite offensiveIcon;

    [SerializeField] private Image typeSpecialDisplay;
    [SerializeField] private TextMeshProUGUI typeSpecialValue;

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

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        Reset();
    }

    public void Show(Building building)
    {
        Reset();

        nameTXT.text = building.name;

        MaterialCost[] thisCosts = building.materialsCosts.ToArray();

        for(int i = 0; i < 4; i++)
        {
            ResourceCostHolder c = holders[i];

            if(i >= thisCosts.Length)
            {
                c.Set(true);
            }
            else
            {
                c.Set(
                    false,
                    thisCosts[i].cost,
                    thisCosts[i].material.sprite
                );
            }
        }

        if(building.type == Building.Types.Residential)
        {
            typeSpecialDisplay.sprite = residentialIcon;
            
            typeSpecialValue.text = building.personCapacity.ToString();
        }else if(building.type == Building.Types.Productive)
        {
            typeSpecialDisplay.sprite = productiveIcon;

            string x = "";

            int j = 0;
            foreach(MaterialProduce p in building.materialsProduce)
            {
                x += p.material.name + " (" + p.amount.ToString() + "/" + p.delay.ToString() + "s" + ")";
                
                if(j < building.materialsProduce.Count - 1)
                {
                    x += ", ";
                }

                j++;
            }

            Debug.Log("name: " + x);

            typeSpecialValue.text = x;
        }
    }

    public void Reset()
    {
        foreach(ResourceCostHolder rch in holders)
        {
            rch.Set(true);
        }
    }
}

[System.Serializable]
public struct InfoToDisplay
{
    public string name;
    public MaterialCost[] costs;
}

[System.Serializable]
public class ResourceCostHolder
{
    public TextMeshProUGUI textHolder;
    public Image resourceImage;

    public void Set(bool empty, int cost = 0, Sprite resourceSprite = null)
    {
        if (empty)
        {
            textHolder.text = "...";
            resourceImage.sprite = ShowBuildingStats.Instance.emptySprite;

            resourceImage.color = new Color(0.5f, 0.5f, 0.5f, 0.4f);

            return;
        }
        resourceImage.sprite = resourceSprite;
        textHolder.text = cost.ToString();

        resourceImage.color = Color.white;
    }
}
