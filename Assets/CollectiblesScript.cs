using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _collectiblesPrefab;
    [SerializeField] private Transform[] _collectiblesTransform;
    private GameObject[] _collectiblesPool;
    [SerializeField] private Transform _parent;


    private void Start()
    {
        SpawnCollectibles();
    }

    private void SpawnCollectibles()
    {
        int _range = _collectiblesPrefab.Length;
        int _randomRange;
        _collectiblesPool = new GameObject[_collectiblesTransform.Length];
        for(int i = 0;i < _collectiblesTransform.Length;i++)
        {
            _randomRange = Random.Range(0,_range);
            print(_randomRange);
            _collectiblesPool[i] = Instantiate(_collectiblesPrefab[_randomRange], _collectiblesTransform[i].position, _collectiblesTransform[i].rotation,_parent);
        }
    }

    public void ResetCollectibles()
    {
        SwapRandomly();
        for(int i = 0; i < _collectiblesPool.Length; i++)
        {
            _collectiblesPool[i].SetActive(true);
        }
    }
    
    private void SwapRandomly()
    {
        int _range = _collectiblesPrefab.Length;
        int _randomRange;
        for(int i = 0; i< _collectiblesPool.Length; i++)
        {
            _randomRange = Random.Range(0, _range);
            var x = _collectiblesPool[_randomRange];
            _collectiblesPool[_randomRange] = _collectiblesPool[i];
            _collectiblesPool[i] = x;
            _collectiblesPool[i].transform.position = _collectiblesTransform[i].position;
        }
    }
}
