using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private OffensiveBuilding building;

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collided with: " + collision.gameObject);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            building.AddEnemy(collision.gameObject.GetComponent<Enemy>());
            return;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            building.RemoveEnemy(other.gameObject.GetComponent<Enemy>());
            return;
        }
    }
}
