using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/EnemySpawn")]
public class EnemySpawn : MonoBehaviour
{

    public Transform m_enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 7));
            Vector3 rot = new Vector3(0, 0, 0);
            Instantiate(m_enemyPrefab, this.transform.position, Quaternion.LookRotation(rot));
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "item.png", true);
    }
}
