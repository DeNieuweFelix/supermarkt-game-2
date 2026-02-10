using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    public GameStats stats;
    [SerializeField] private bool debugReset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (debugReset)
        {
            stats.MaxResidents = 0;
            stats.CurrentResidents = 0;
        }

        StartCoroutine(UIloop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator UIloop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            MainResourceUpdater.Instance.UpdateAll();
        }
    }
}
