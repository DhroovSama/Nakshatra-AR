using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioPlayer : MonoBehaviour
{
    public static GlobalAudioPlayer Instance { get; private set; }

    [SerializeField]
    private AudioSource globalAudioPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensures the object persists across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (globalAudioPlayer != null && clip != null)
        {
            globalAudioPlayer.PlayOneShot(clip);
        }
    }

    public static void getPlaySound(AudioClip clip)
    {
        Instance?.PlaySound(clip);
    }
}
