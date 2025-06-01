using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ending : MonoBehaviour
{
    bool isCheck = false;

    public UnityEvent events;


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

            if (!isCheck)
            {
                isCheck = true;
                events.Invoke();



            }
            
        }

        
        
    }
}
