using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioClipsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliverFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footSteps;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
