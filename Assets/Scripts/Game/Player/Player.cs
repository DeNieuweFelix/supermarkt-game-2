using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

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

    public PlayerStats playerStats;

    void Start()
    {
        CheatAmount();
    }

    public void AddMaterial(string name, int amountAdd)
    {
        MaterialOwned mat = playerStats.MaterialsOwned.Find(x => x.material.name == name);

        mat.amount += amountAdd;
        Debug.Log("added: " + amountAdd + " to: " + mat.material.name);
    }

    public void CheatAmount()
    {
        foreach(MaterialOwned o in playerStats.MaterialsOwned)
        {
            o.amount = 99999;
        }
    }
}
