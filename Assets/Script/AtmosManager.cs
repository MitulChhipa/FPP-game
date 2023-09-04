using UnityEngine;

public class AtmosManager : MonoBehaviour
{
    //float targetColor;
    Color defaultColor;
    Color targetColor;

    private void Start()
    {
        defaultColor = RenderSettings.fogColor;
        targetColor = RenderSettings.fogColor;
    }

    private void Update()
    {
        RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, targetColor, Time.deltaTime * (0.1f));
    }

    public void DisableFog()
    {
        targetColor = Color.black;
        //RenderSettings.fog = false;
    }
    public void EnableFog()
    {
        targetColor = defaultColor;
       
    }
}
