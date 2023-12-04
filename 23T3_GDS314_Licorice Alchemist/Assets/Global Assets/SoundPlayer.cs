using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public AudioSource source;

    void PlayButtonSound()
    {
        source.PlayOneShot(buttonSound);
    }

    void PlayConfirmSound()
    {
        source.PlayOneShot(confirmSound);
    }

    void PlayDenidedSound()
    {
        source.PlayOneShot(deniedSound);
    }

    void PlayTransitionSound()
    {
        source.PlayOneShot(transitionSound);
    }

    void PlayFailSound()
    {
        source.PlayOneShot(failSound);
    }

    void PlayVictorySound()
    {
        source.PlayOneShot(victorySound);
    }

    void PlayPickUpSound()
    {
        source.PlayOneShot(pickUpSound);
    }

    void PlayCatchSound()
    {
        source.PlayOneShot(catchSound);
    }

    void PlayThemeMusic()
    {
        source.PlayOneShot(themeMusic);
    }
}
