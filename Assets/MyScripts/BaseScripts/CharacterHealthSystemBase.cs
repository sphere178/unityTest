using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundController;

public class CharacterHealthSystemBase : MonoBehaviour, IDamager
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected SoundController soundController;
    

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void HitEvent(string hitAnimName,Transform attacker)
    {
        
        
    }
    protected void PlayHitSound()
    {
        soundController.PlaySound(audioSource, SoundType.hit);
    }
    protected void PlayNormalParrySound()
    {
        Debug.Log("PLAY PARRY SOUND");
        soundController.PlaySound(audioSource, SoundType.normalparry);
    }
    protected void PlayExecuteParrySound()
    {
        soundController.PlaySound(audioSource, SoundType.executeparry);
    }

    
}
