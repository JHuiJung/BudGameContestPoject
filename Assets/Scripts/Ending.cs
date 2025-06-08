using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ending : MonoBehaviour
{
    bool isCheck = false;

    public bool isTrigger = true;
    public UnityEvent events;


    private void OnTriggerEnter(Collider other)
    {

        if (!isTrigger)
            return;

        if (other.tag == "Player")
        {

            if (!isCheck)
            {
                isCheck = true;
                events.Invoke();



            }
            
        }

        
        
    }
    public void ResetEnding()
    {
        isCheck=false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTrigger)
            return;

        if (collision.gameObject.tag == "Player")
        {

            if (!isCheck)
            {
                isCheck = true;
                events.Invoke();



            }

        }
    }
}
