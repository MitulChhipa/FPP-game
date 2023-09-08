using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemy2;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _samples;
    [SerializeField] private int _maxEnemy;
    public Transform[] spawnPoints;
    int count = 0;

    private void Start()
    {
        InvokeRepeating("instantiateEnemy", 0, 1);
    }


    private void instantiateEnemy()
    {
        if (count == _maxEnemy)
        {
            return;
        }
        GameObject y;
        count++;
        if (count % 2 == 0)
        {
            y = _enemy;
        }
        else
        {
            y = _enemy2;
        }

        GameObject x = Instantiate(y, spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity, this.transform);
        x.GetComponent<EnemyScript>().sampleTransform = _samples;
    }
    public Vector3 RandomPosition()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;
    }

    public void ResetAllEnemies()
    {
        BroadcastMessage("ResetAndRespawn");
    }
}
