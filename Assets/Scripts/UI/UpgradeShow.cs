using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShow : MonoBehaviour
{
    [Header("ui settings")]
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private GameObject countHolder;
    [SerializeField] private TextMeshProUGUI costHolder;
    [SerializeField] private TextMeshProUGUI levelHolder;
    [SerializeField] private RectTransform rect;

    [SerializeField] private GameObject mainHolder;

    [Header("UI values")]
    [SerializeField] private float idlePosX = 1200f;
    [SerializeField] private float activePosX = 800f;

    [SerializeField] private Vector2 originalUIpos;

    private bool open = false;

    public static UpgradeShow Instance;

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

    void FixedUpdate()
    {
        if(open == false)
        {
            if(rect.anchoredPosition.x != idlePosX)
            {
                rect.anchoredPosition += new Vector2(5f, 0f);
                if(rect.anchoredPosition.x > idlePosX) rect.anchoredPosition = new Vector2();
            }
            else
            {
                return;
            }
        }
        else
        {
            if(rect.anchoredPosition.x != activePosX)
            {
                rect.anchoredPosition -= new Vector2(5f, 0f);
            }
            else
            {
                return;
            }
        }
    }

    public void Show(UpgradeInfo u)
    {
        SetVisualUpgradeLevel(u);

        name.text = u.buildingName;
        costHolder.text = "$" + u.upgradeCost.ToString();
        levelHolder.text = "LVL " + u.currentLevel.ToString();

        open = true;
    }

    public void Hide()
    {
        open = false;
    }

    private void SetVisualUpgradeLevel(UpgradeInfo u)
    {
        int i = 0;
        List<Transform> activeT = new List<Transform>();

        foreach(Transform part in countHolder.transform)
        {
            part.gameObject.GetComponent<Image>().color = Color.darkGreen;

            if(i < u.amountOfUpgrades)
            {
                if(part.gameObject.activeSelf == false)
                {
                    part.gameObject.SetActive(true);
                }

                activeT.Add(part);
            }
            else
            {
                if(part.gameObject.activeSelf == true)
                {
                    part.gameObject.SetActive(false);
                }
            }

            i++;
        }

        for(int j = 0; j < u.currentLevel; j++)
        {
            activeT[j].GetComponent<Image>().color = Color.green;
        }
    }
}

[System.Serializable]
public struct UpgradeInfo
{
    public string buildingName;
    public byte amountOfUpgrades;
    public byte currentLevel;
    public int upgradeCost;
}
