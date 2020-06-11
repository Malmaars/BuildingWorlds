using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeAdjust : MonoBehaviour
{
    public void adjustVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
