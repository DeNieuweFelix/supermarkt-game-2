using UnityEngine;
using UnityEngine.Timeline;

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

    public void RemoveMaterial(string name, int amountRemove)
    {
        MaterialOwned mat = playerStats.MaterialsOwned.Find(x => x.material.name == name);
        mat.amount -= amountRemove;
    }

    public int GetMaterial(string name)
    {
        MaterialOwned mat = playerStats.MaterialsOwned.Find(x => x.material.name == name);

        return mat.amount;
    }

    public void CheatAmount()
    {
        foreach(MaterialOwned o in playerStats.MaterialsOwned)
        {
            o.amount = 100;
        }
    }
}
