using System.Collections.Generic;
using UnityEngine;

public class BuildOptionsSetup : MonoBehaviour
{
    [SerializeField] private GameObject ButtonObj;
    [SerializeField] private GameObject ButtonHolder;
    public List<Building> ResidentialBuildings;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupButtons()
    {
        foreach(Building b in ResidentialBuildings)
        {
            CreateButton(b);
        }
    }

    private void CreateButton(Building b)
    {
        GameObject but = Instantiate(ButtonObj);
        but.transform.SetParent(ButtonHolder.transform);

        but.GetComponent<BuildingButton>().Set(b);
    }
}
