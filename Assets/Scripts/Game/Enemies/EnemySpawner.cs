using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<Enemy> allEnemies = new List<Enemy>();
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

    public Enemy GetFrontEnemy()
    {
        if(allEnemies.Count == 0){
            Debug.LogWarning("nothing in list ):");
            return null;
        }

        Enemy[] listCopy = allEnemies.ToArray();

        Enemy[] orderedList = listCopy.OrderBy(e => e.lifeTime).ToArray();

        Enemy e = orderedList[orderedList.Length - 1];
        Debug.Log("front enemy found: " + e);

        return e;
    }

    private IEnumerator SpawnQueue(List<Vector3> posses)
    {
        for(int i = 0; i < testAmount; i++)
        {
            GameObject e = Instantiate(testEnemy);
            Enemy eScript = e.GetComponent<Enemy>();

            eScript.Setup(posses);

            allEnemies.Add(eScript);

            yield return new WaitForSeconds(1f);
        }
    }
}
