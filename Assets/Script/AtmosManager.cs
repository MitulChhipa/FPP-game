using UnityEngine;

public class AtmosManager : MonoBehaviour
{
    //float targetColor;
    private float _defaultDensity = 0.005f;
    private float _targetDensity;
    private float _densityMultiplier = 0.1f;
    //private bool _targetDensityReached;

    private void Start()
    {
        EnableFog();
    }

    private void Update()
    {
        RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, _targetDensity, Time.deltaTime * _densityMultiplier);
    }

    public void DisableFog()
    {
        _targetDensity = 0f;
    }
    public void EnableFog()
    {
        _targetDensity = _defaultDensity;
    }
}
