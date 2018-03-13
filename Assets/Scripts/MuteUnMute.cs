using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteUnMute : MonoBehaviour
{
    public Button musicButton;
    public Button soundButton;

    Image musicImage;
    Image soundImage;

    public Sprite MusicOn;
    public Sprite MusicOff;

    public Sprite SoundOn;
    public Sprite SoundOff;

    GameObject soundEffectsSource;
    GameObject musicSource;
    AudioSource musicPlaying;
    AudioSource soundPlaying;

    void Awake()
    {
        soundEffectsSource = GameObject.FindGameObjectWithTag("Sound");
        musicSource = GameObject.FindGameObjectWithTag("Music");

        if (musicSource != null)
        {
            musicPlaying = musicSource.GetComponent<AudioSource>();
        }

        if (soundEffectsSource != null)
        {
            soundPlaying = soundEffectsSource.GetComponent<AudioSource>();
        }

        musicImage = musicButton.GetComponent<Image>();
        soundImage = soundButton.GetComponent<Image>();

        if (musicPlaying != null)
        {
            if (musicPlaying.mute == false)
            {
                musicImage.sprite = MusicOn;
            }
            else
            {
                musicImage.sprite = MusicOff;
            }
        }

        if (soundPlaying != null)
        {
            if (soundPlaying.mute == false)
            {
                soundImage.sprite = SoundOn;
            }
            else
            {
                soundImage.sprite = SoundOff;
            }
        }
    }

    public void MuteAndUnMuteMusic()
    {
        if (musicPlaying.mute == false)
        {
            musicImage.sprite = MusicOff;
            if (musicPlaying != null)
            {
                musicPlaying.mute = true;
            }
        }
        else
        {
            musicImage.sprite = MusicOn;
            if (musicPlaying != null)
            {
                musicPlaying.mute = false;
            }
        }
    }

    public void MuteAndUnMuteSound()
    {
        if (soundPlaying.mute == false)
        {
            soundImage.sprite = SoundOff;
            if (soundPlaying != null)
            {
                soundPlaying.mute = true;
            }
        }
        else
        {
            soundImage.sprite = SoundOn;
            if (soundPlaying != null)
            {
                soundPlaying.mute = false;
            }
        }
    }
}