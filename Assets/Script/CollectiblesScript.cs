using UnityEngine;

public class CollectiblesScript : MonoBehaviour
{
    [SerializeField] private Transform _collectiblesParent;
    [SerializeField] private GameObject[] _collectiblesPrefab;
    [SerializeField] private Transform[] _collectiblesTransform;
    private GameObject[] _collectiblesPool;


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
            _collectiblesPool[i] = Instantiate(_collectiblesPrefab[_randomRange], _collectiblesTransform[i].position, _collectiblesTransform[i].rotation, _collectiblesParent);
        }
    }

    public void ResetCollectibles()
    {
        SwapRandomly();
        for(int i = 0; i < _collectiblesPool.Length; i++)
        {
            _collectiblesPool[i].SetActive(true);
            _collectiblesPool[i].transform.position = _collectiblesTransform[i].position;
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
        }
    }
}
