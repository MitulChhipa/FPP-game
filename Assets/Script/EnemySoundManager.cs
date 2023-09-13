using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource _idleSound;
    [SerializeField] private AudioSource _activeSound;
    [SerializeField] private AudioSource _attackSound;


    public void PlayAttackSound()
    {
        
        StopAllSound();
        _attackSound.Play();
    }
    public void PlayIdleSound()
    {
       
        StopAllSound();
        _idleSound.Play();
    }
    public void PlayActiveSound()
    {
        
        StopAllSound();
        _activeSound.Play();
    }
    public void StopAllSound()
    {
        _idleSound.Stop();
        _activeSound.Stop();
        _attackSound.Stop();
    }
}
