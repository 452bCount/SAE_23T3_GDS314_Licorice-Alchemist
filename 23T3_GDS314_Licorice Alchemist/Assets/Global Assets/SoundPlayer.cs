using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [Header("Sound Clips")]
    public AudioClip buttonSound;
    public AudioClip confirmSound;
    public AudioClip deniedSound;
    public AudioClip transitionSound;
    public AudioClip failSound;
    public AudioClip victorySound;
    public AudioClip pickUpSound;
    public AudioClip catchSound;
    public AudioClip themeMusic;
    
    [Space]
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayButtonSound()
    {
        source.PlayOneShot(buttonSound);
    }

    public void PlayConfirmSound()
    {
        source.PlayOneShot(confirmSound);
    }

    public void PlayDenidedSound()
    {
        source.PlayOneShot(deniedSound);
    }

    public void PlayTransitionSound()
    {
        source.PlayOneShot(transitionSound);
    }

    public void PlayFailSound()
    {
        source.PlayOneShot(failSound);
    }

    public void PlayVictorySound()
    {
        source.PlayOneShot(victorySound);
    }

    public void PlayPickUpSound()
    {
        source.PlayOneShot(pickUpSound);
    }

    public void PlayCatchSound()
    {
        source.PlayOneShot(catchSound);
    }

    public void PlayThemeMusic()
    {
        source.PlayOneShot(themeMusic);
    }
}
