using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _reloadStart;
    [SerializeField] private AudioSource _reloadEnd;
    [SerializeField] private AudioSource _eqip;



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
    public void Equip()
    {
        StopAllSound();
        _eqip.Play();
    }

    public void StopAllSound()
    {
        _reloadEnd.Stop();
        _reloadStart.Stop();
        _eqip.Stop();
    }
}

