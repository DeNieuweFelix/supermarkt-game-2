using System;
using TMPro;
using UnityEngine;

public class LoadTextUpdater : MonoBehaviour
{
    public static LoadTextUpdater Instance;
    [SerializeField] private TextMeshProUGUI textHolder;

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

    public void UpdateUI(string text)
    {
        textHolder.text = text;
    }
}
