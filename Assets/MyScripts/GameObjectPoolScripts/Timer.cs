using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool isUsed;
    [SerializeField]private bool First=true;
    private Action action;
    private float currentTime;
    
    void Start()
    {
       
    }


    private void FixedUpdate()
    {
        StartTimer(action, currentTime);
    }
    

    private void StartTimer(Action _action,float _time)
    {
        
        
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = 0;
            isUsed = false;
            First = true;
            action.Invoke();
            gameObject.SetActive(false);
        }


    }

    //外部调用
    public void InitTimer(Action _action, float _time)
    {
        if (First)
        {
           
            action = _action;
            currentTime = _time;
            First = false;
            isUsed = true;
        }
    }
}
