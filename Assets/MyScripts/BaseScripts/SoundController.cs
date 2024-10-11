using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
   public enum SoundType
    {
        hit,
        attack,
        normalparry,
        executeparry
    }
    [SerializeField] private SoundAssests soundAssests;

    private void Awake()
    {
        instance = this;
    }


    public void PlaySound(AudioSource audiosource,SoundType soundTpye)
    {
        
        switch (soundTpye)
        {
            case SoundType.hit: 
                audiosource.PlayOneShot(soundAssests.hit[Random.Range(0,(soundAssests.hit.Length)-1)]);
                break;
            case SoundType.attack:
                audiosource.PlayOneShot(soundAssests.attack[Random.Range(0, (soundAssests.attack.Length) - 1)]);
                break;
            case SoundType.normalparry:
                audiosource.PlayOneShot(soundAssests.normalparry[Random.Range(0, (soundAssests.normalparry.Length) - 1)]);
                break;
            case SoundType.executeparry:
                audiosource.PlayOneShot(soundAssests.executeparry[Random.Range(0, (soundAssests.executeparry.Length) - 1)]);
                break;
        }
    }
}
