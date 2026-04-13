using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public List<Vector3> positions;
    public float speed = 10f;
    [SerializeField] private float health = 100f;
    private float originalHealth = 100f;

    public int lifeTime;

    [Header("UI settings")]
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBar;

    void FixedUpdate()
    {
        Vector3 direction = Camera.main.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 euler = targetRotation.eulerAngles;

        canvasRect.transform.rotation = Quaternion.Euler(euler);
    }

    public void Setup(List<Vector3> pos)
    {
        positions = pos;

        transform.position = positions[0];

        originalHealth = health;

        StartCoroutine(EnemyMove());
    }

    private IEnumerator EnemyMove()
    {
        yield return new WaitForSeconds(0.1f);

        foreach(Vector3 v in positions)
        {
            while(transform.position != v)
            {
                transform.position = Vector3.MoveTowards(transform.position, v, speed * Time.deltaTime);
                yield return new WaitForFixedUpdate();

                lifeTime++;
            }
            // yield return new WaitForSeconds(0.1f);
        }
    }

    public bool ReceiveDamage(float amount)
    {
        health -= amount;

        UpdateHealthUI();

        if(health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    private void UpdateHealthUI()
    {
        healthText.text = health + "/" + originalHealth;
        healthBar.fillAmount = health / originalHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

