using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FootstepSounds : MonoBehaviour
{
    public AudioClip[] grassClips;
    public AudioClip[] cityClips;
    
    public void playFootSteps()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        if (y == 1)
        {
            SoundFXManager.instance.playRandomSoundFXClip(grassClips, transform, 0.2f);
        }
        else if (y == 2)
        {
            SoundFXManager.instance.playRandomSoundFXClip(cityClips, transform, 0.4f);
        }
            
    }
}
