using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private Image _health;
    [SerializeField] private Image _stamina;
    [SerializeField] private Image _water;
    [SerializeField] private Image _food;
    [SerializeField] private Animator _panelAnimator;
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private AudioSource _hurt;

    public bool canRun = true;
    public float health = 100f;
    public float water = 100f;
    public float stamina = 100f;
    public float food = 100f;


    #region MonoFuntions
    private void Start()
    {
        updateUIBar(_health,health);
        updateUIBar(_water, water);
        updateUIBar(_stamina, stamina);
        updateUIBar(_food, food);
    }
    #endregion

    #region HealthChangeFuntions
    public void changeHealth(float valueChange)
    {
        if (!_menuManager.canActiveMenu)
        {
            return;
        }
        if(valueChange < 0f)
        {
            _panelAnimator.SetTrigger("Attack");
            _hurt.Play();
        }
        changeValues(ref health, valueChange,ref _health);
        if(health == 0)
        {
            _menuManager.GameOver();
        }
    }
    public void changeWater(float valueChange)
    {
        changeValues(ref water, valueChange,ref _water);
    }
    public void changeStamina(float valueChange)
    {
        changeValues(ref stamina, valueChange,ref _stamina);
    }
    public void changeFood(float valueChange)
    {
        changeValues(ref food, valueChange, ref _food);
    }

    public void changeValues(ref float x ,float y ,ref Image ui)
    {
        x += y;
        x = Mathf.Clamp(x, 0f, 100f);
        updateUIBar(ui,x);
    }
    #endregion

    #region UIUpdateFunctions
    public void updateUIBar(Image i, float value)
    {
        i.fillAmount = value / 100;
    }

    public void UpdateAllHpUi()
    {
        updateUIBar(_health, health);
        updateUIBar(_water, water);
        updateUIBar(_stamina, stamina);
        updateUIBar(_food, food);
    }
    #endregion
}
