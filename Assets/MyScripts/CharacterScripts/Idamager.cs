using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamager 
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void HitEvent(string hitAnimName,Transform attacker);
    

}
