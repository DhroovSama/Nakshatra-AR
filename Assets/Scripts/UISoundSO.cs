using UnityEngine;

[CreateAssetMenu(fileName = "UISound", menuName = "ScriptableObjects/UISound", order = 1)]
public class UISoundSO : ScriptableObject
{
    public AudioClip tapSound;
    public AudioClip portalSound;
    public AudioClip successSound;
    public AudioClip errorSound;
    public AudioClip playerCheckerSound;
    public AudioClip landerLaunchSound;
    public AudioClip transitionSceneSound;
    public AudioClip factCollectedSound;

    [Header("Rover SFX")]
    public AudioClip roverMovingSound;
    public AudioClip roverRotationSound;
    public AudioClip roverBrakeSound;
    public AudioClip roverRotationBrakeSound;

    [Header("Quiz SFX")]
    public AudioClip correctSound;
    public AudioClip wrongSound;

    public void PlayTapSound()
    {
        GlobalAudioPlayer.GetPlaySound(tapSound);
    }

    public void PlayPlayerCheckerSound()
    {
        GlobalAudioPlayer.GetPlaySound(playerCheckerSound);
    }

    public void PlayPortalSound()
    {
        GlobalAudioPlayer.GetPlaySound(portalSound);
    }

    public void PlaySuccessSound()
    {
        GlobalAudioPlayer.GetPlaySound(successSound);
    }

    public void PlayErrorSound()
    {
        GlobalAudioPlayer.GetPlaySound(errorSound);
    }
    public void PlayLanderLaunchSound()
    {
        GlobalAudioPlayer.GetPlaySound(landerLaunchSound);
    }

    public void PlayRoverMoveSound()
    {
        GlobalAudioPlayer.GetPlaySound(roverMovingSound);
    }

    public void PlayRoverBrakeSound()
    {
        GlobalAudioPlayer.GetPlaySound(roverBrakeSound);
    }
    public void PlayRoverRotationSound()
    {
        GlobalAudioPlayer.GetPlaySound(roverRotationSound);
    }
    public void PlayRoverRotationBrakeSound()
    {
        GlobalAudioPlayer.GetPlaySound(roverRotationBrakeSound);
    }
    public void PlaySceneTransitionSound()
    {
        GlobalAudioPlayer.GetPlaySound(transitionSceneSound);
    }
    public void PlayFactCollectedSound()
    {
        GlobalAudioPlayer.GetPlaySound(factCollectedSound);
    }
    public void PlayCorrectSound()
    {
        GlobalAudioPlayer.GetPlaySound(correctSound);
    }
    public void PlayWrongSound()
    {
        GlobalAudioPlayer.GetPlaySound(wrongSound);
    }
}
