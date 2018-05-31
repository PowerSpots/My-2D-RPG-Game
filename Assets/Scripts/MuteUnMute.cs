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
        // 根据标签查找声音对象并分配给变量
        soundEffectsSource = GameObject.FindGameObjectWithTag("Sound");
        // 根据标签查找音乐对象
        musicSource = GameObject.FindGameObjectWithTag("Music");

        if (musicSource != null)
        {
            // 查找音源组件
            musicPlaying = musicSource.GetComponent<AudioSource>();
        }

        if (soundEffectsSource != null)
        {
            // 查找音源组件
            soundPlaying = soundEffectsSource.GetComponent<AudioSource>();
        }

        musicImage = musicButton.GetComponent<Image>();
        soundImage = soundButton.GetComponent<Image>();

        // 开启、关闭音乐时显示正确的音乐图像
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
        // 开启、关闭声音时显示正确的音乐图像
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
        // 关闭音乐
        if (musicPlaying.mute == false)
        {
            musicImage.sprite = MusicOff;
            if (musicPlaying != null)
            {
                musicPlaying.mute = true;
            }
        }
        // 开启音乐
        else
        {
            musicImage.sprite = MusicOn;
            if (musicPlaying != null)
            {
                musicPlaying.mute = false;
            }
        }
    }

    //关闭开启声音
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