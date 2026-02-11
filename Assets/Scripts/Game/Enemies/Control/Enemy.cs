using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Vector3> positions;
    public float speed = 10f;

    public void Setup(List<Vector3> pos)
    {
        positions = pos;

        transform.position = positions[0];

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
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
