using UnityEngine;

public class WeaponAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _reloadStart;
    [SerializeField] private AudioSource _reloadEnd;

    public void StartReload()
    {
        StopAllSound();
        _reloadStart.Play();    
    }
    public void EndReload()
    {
        StopAllSound();
        _reloadEnd.Play();
    }

    public void StopAllSound()
    {
        _reloadEnd.Stop();
        _reloadStart.Stop();
    }
}

