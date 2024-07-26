using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoverSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource roverAudioPlayer;

    [SerializeField]
    private AudioClip roverMovingSFX, roverTurningSFX;

    [Space]
    [SerializeField]
    private Button forward, backward;

    [SerializeField]
    private Button right, left;

}
