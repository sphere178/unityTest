using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="SoundAssests",menuName = "SoundAssests/SoundAssests")]
public class SoundAssests : ScriptableObject
{
    /*enum Sound{
        hit,
        attack,
        parry,
        executeparry
         
    }*/
    public AudioClip[] hit;
    public AudioClip[] attack;
    public AudioClip[] normalparry;
    public AudioClip[] executeparry; //时间变慢，可以发动处决
}
