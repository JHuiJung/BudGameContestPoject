using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadObj : MonoBehaviour
{

    public bool useForce = false;
    public float force = 10f;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Ship _ship = collision.gameObject.GetComponent<Ship>();
            StartCoroutine(_ship.GameOver());

            if (useForce)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();


                float RandY = Random.Range(0f, 1f);
                Vector3 dir = (_ship.gameObject.transform.position - this.transform.forward) + Vector3.up*RandY;
                
                
                dir.Normalize();

                rb.AddForce(dir * force, ForceMode.Acceleration);
            }


            


        }
    }
}
