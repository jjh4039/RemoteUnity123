using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Typing, Ready, Use, Error, CheckPoint }

    void Awake()
    {
        instance = this;
        Init();
        PlayBgm(true);
    }

    void Init()
    {
        // BGM Player 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false; 
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // SFX Player 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for (int index = 0; index < sfxPlayers.Length; index++){
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }
    
    public void PlaySfx(Sfx sfx, int detail)
    {
        for (int index=0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying) { 
                continue;
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].volume = 1f;

            switch (sfx) // 예외처리
            {
                case Sfx.Typing:
                    sfxPlayers[loopIndex].pitch = Random.Range(1f, 1.2f);
                    sfxPlayers[loopIndex].loop = true;
                    break;
                case Sfx.Ready:
                    sfxPlayers[loopIndex].volume = 0.9f;
                    if (detail == 1)
                    {
                        sfxPlayers[loopIndex].pitch = 1.2f;
                        sfxPlayers[loopIndex].loop = false;
                    }
                    else if (detail == 2)
                    {
                        sfxPlayers[loopIndex].pitch = 1.3f;
                        sfxPlayers[loopIndex].loop = false;
                    }
                    break;
                case Sfx.Use:
                    sfxPlayers[loopIndex].volume = 0.7f;
                    if (detail == 1)
                    {
                        sfxPlayers[loopIndex].pitch = 1.2f;
                        sfxPlayers[loopIndex].loop = false;
                    }
                    else if (detail == 2)
                    {
                        sfxPlayers[loopIndex].pitch = 1.3f;
                        sfxPlayers[loopIndex].loop = false;
                    }
                    break;
                case Sfx.Error:
                    sfxPlayers[loopIndex].volume = 0.4f;
                    sfxPlayers[loopIndex].pitch = 1.3f;
                    sfxPlayers[loopIndex].loop = false;
                    break;
                case Sfx.CheckPoint:
                    sfxPlayers[loopIndex].volume = 0.4f;
                    sfxPlayers[loopIndex].pitch = 1.9f;
                    sfxPlayers[loopIndex].loop = false;
                    break;
            }
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void StopSfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            if (sfxPlayers[index].isPlaying && sfxPlayers[index].clip == sfxClips[(int)sfx])
            {
                sfxPlayers[index].Stop();
                break;
            }
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay) {
            bgmPlayer.Play();
        }

        else {
            bgmPlayer.Stop();
        }
    }
}
