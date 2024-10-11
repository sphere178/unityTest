using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPoolSystem : MonoBehaviour
{
    public GameObjectPool gameObjectPool;
    public static GameObjectPoolSystem instance;
    public List<Timer> timers = new List<Timer>();
    
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < gameObjectPool.GameObjectCount; i++)
        {
           GameObject instantiatedTimer= Instantiate(gameObjectPool.GameObjectPrefab, transform);
            timers.Add(instantiatedTimer.GetComponent<Timer>());
            instantiatedTimer.SetActive(false);
            

        }

    }


    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public Timer TakeAvailableTimer()
    {
        for(int i = 0; i < timers.Count; i++)
        {
            if (timers[i].gameObject.activeSelf == false)
            {
                
                timers[i].gameObject.SetActive(true);
                return timers[i];
            }
            else
            {
                continue;
            }
        }
       
        return null;
    }
}
