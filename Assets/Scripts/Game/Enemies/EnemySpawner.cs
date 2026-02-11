using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

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

    [SerializeField] private GameObject testEnemy;
    [SerializeField] private int testAmount = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies(List<Vector3> posses)
    {
        StartCoroutine(SpawnQueue(posses));
    }

    private IEnumerator SpawnQueue(List<Vector3> posses)
    {
        for(int i = 0; i < testAmount; i++)
        {
            GameObject e = Instantiate(testEnemy);
            Enemy eScript = e.GetComponent<Enemy>();

            eScript.Setup(posses);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
