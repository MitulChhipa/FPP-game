using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class researchScript : MonoBehaviour
{
    [SerializeField] private Text _loadingPercent;
    [SerializeField] private Image _loadingBar;
    public float informationValue;
    private float _targetInformationValue;

    private void Start()
    {
        UpdateLoadingBar();
    }

    private void Update()
    {
        if(informationValue > _targetInformationValue)
        {
            informationValue = _targetInformationValue;
            UpdateLoadingBar();
        }

        if (informationValue != _targetInformationValue)
        {
            informationValue += Time.deltaTime;
            UpdateLoadingBar();
        }
    }

    public void UpdateInformationData(float x)
    {
        _targetInformationValue += x;
        _targetInformationValue = Mathf.Clamp(_targetInformationValue, 0f, 100f);
        UpdateLoadingBar();
    }

    public void UpdateLoadingBar()
    {
        _loadingBar.fillAmount = informationValue/100;
        _loadingPercent.text = "Data aquired : " + informationValue.ToString() + "%";
    }
    public void ResetValues(float x)
    {
        _targetInformationValue = x;
        informationValue = x;
        UpdateLoadingBar();
    }
}
