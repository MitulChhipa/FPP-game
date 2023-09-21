using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemy2;
    [SerializeField] private GameObject[] _enemyPool;
    [SerializeField] private Transform _enemyParent;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _samples;
    [SerializeField] private int _maxEnemy;
    public Transform[] spawnPoints;
    private int _count = 0;

    #region Mono
    private void Start()
    {
        _enemyPool = new GameObject[_maxEnemy];
        InvokeRepeating("instantiateEnemy", 0, 0.01f);
    }
    #endregion

    #region EnemiesSpawn
    private void instantiateEnemy()
    {
        if (_count == _maxEnemy)
        {
            return;
        }
        GameObject y;
        if (_count % 2 == 0)
        {
            y = _enemy;
        }
        else
        {
            y = _enemy2;
        }

        _enemyPool[_count] = Instantiate(y, spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity, _enemyParent);
        _enemyPool[_count].GetComponent<EnemyScript>().sampleTransform = _samples;

        _count++;
    }
    public Vector3 RandomPosition()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;
    }

    public void ResetAllEnemies()
    {
        for(int i = 0; i < _maxEnemy; i++)
        {
            if(_enemyPool[i] == null)
            {
                continue;
            }
            _enemyPool[i].SetActive(true);
        }
        BroadcastMessage("CancelInvoke");
        BroadcastMessage("ResetAndRespawn");
    }
    #endregion
}
