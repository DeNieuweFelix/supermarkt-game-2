using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using NUnit.Framework;

public class OffensiveBuilding : MonoBehaviour
{
    public float fireRate = 1f;
    public float damage = 10f;

    public byte upgradeLevel = 0;
    public List <GameObject> upgrades = new List<GameObject>();

    [SerializeField] private bool hasTarget = false;

    [SerializeField] private List<Enemy> EnemiesInRange = new List<Enemy>();
    [SerializeField] private Enemy target;

    [SerializeField] private List<ParticleSystem> particles = new List<ParticleSystem>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SearchLoop());
        StartCoroutine(ShootLoop());

        ResetTower();
    }

    private void ResetTower()
    {
        foreach(GameObject g in upgrades)
        {
            if(g.activeSelf == true)
            {
                g.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEnemy(Enemy enemy)
    {
        EnemiesInRange.Add(enemy);

        Debug.Log("found new enemy!");
    }

    public void RemoveEnemy(Enemy enemy)
    {
        EnemiesInRange.Remove(enemy);
    }

    public void UpgradeBuilding()
    {
        
    }

    private void FindFirstEnemy()
    {
        if(EnemiesInRange.Count == 0)
        {
            hasTarget = false;
            return;
        }

        Enemy[] enemyList = EnemiesInRange.ToArray();
        
        Enemy[] orderedList = enemyList.OrderBy(e => e.lifeTime).ToArray();
        target = orderedList[orderedList.Length - 1];

        hasTarget = true;
    }

    private IEnumerator SearchLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            FindFirstEnemy();
        }
    }

    private IEnumerator ShootLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            if (hasTarget)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Vector3 direction = target.transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 euler = targetRotation.eulerAngles;
        Debug.LogWarning(targetRotation);

        //rotation? (please)
        transform.rotation = Quaternion.Euler(
            0f,
            euler.y,
            0f            
        );

        bool isDead = target.ReceiveDamage(damage);
        if (isDead)
        {
            EnemiesInRange.Remove(target);
        }

        ShootParticles();
    }

    private void ShootParticles()
    {
        if(particles.Count == 0)
        {
            return;
        }

        foreach(ParticleSystem p in particles)
        {
            p.Play();
        }
    }
}
