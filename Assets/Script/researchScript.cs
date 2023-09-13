using UnityEngine;
using UnityEngine.UI;

public class researchScript : MonoBehaviour
{

    [SerializeField] private Text _loadingPercent;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private MenuManager _menuManager;
    private float _targetInformationValue;
    private float _timeMultiplier = 10f;
    public float informationValue;


    #region Mono
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
            informationValue += Time.deltaTime * _timeMultiplier;
            UpdateLoadingBar();
        }
    }
    #endregion

    #region VariableRelatedFuntions
    public void UpdateInformationData(float x)
    {
        _targetInformationValue += x;
        _targetInformationValue = Mathf.Clamp(_targetInformationValue, 0f, 100f);
        UpdateLoadingBar();
    }

    public void ResetValues(float x)
    {
        _targetInformationValue = x;
        informationValue = x;
        UpdateLoadingBar();
    }
    #endregion

    #region UIFuntions
    public void UpdateLoadingBar()
    {
        _loadingBar.fillAmount = informationValue/100;
        _loadingPercent.text = "Data aquired : " + informationValue.ToString() + "%";
        if (informationValue == 100f)
        {
            _menuManager.Win();
        }
    }
    #endregion
}
