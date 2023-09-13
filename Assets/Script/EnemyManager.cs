using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    #region VaribaleDeclaraion
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemy2;
    [SerializeField] private Transform _enemyParent;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _samples;
    [SerializeField] private int _maxEnemy;
    public Transform[] spawnPoints;
    private int _count = 0;
    #endregion

    #region Mono
    private void Start()
    {
        InvokeRepeating("instantiateEnemy", 0, 1);
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
        _count++;
        if (_count % 2 == 0)
        {
            y = _enemy;
        }
        else
        {
            y = _enemy2;
        }

        GameObject x = Instantiate(y, spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity, _enemyParent);
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
    #endregion
}
