using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Duck_Swan : DuckBase
{
    [Range(0f, 100f)]
    public float _speed = 1f;

    
    FloatingObj floatingObj;

    private void Start()
    {
        floatingObj = GetComponent<FloatingObj>();

        float _x = 1f;
        _x = Random.Range(-0.5f, 0.5f) > 0 ? 1 : -1;
        _x *= _speed;
        floatingObj.ChangeDriftX(_x);

        StartRot();    
    }

    public override void DuckEvent()
    {
        float _xx = floatingObj.driftX * -1;
        floatingObj.ChangeDriftX(_xx);


    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Water" &&
            collision.gameObject.tag != "Duck")
        {
            DuckEvent();
        }
    }

    void StartRot()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // 0������ 360�� ������ ������ ���� ����
        float randomY = Random.Range(0f, 360f);

        // ���� ������Ʈ�� ȸ������ ������ Y������ ����
        transform.rotation = Quaternion.Euler(0f, randomY, 0f);


        // 1 �Ǵ� -1 �� �����ϰ� �����ؼ� �ð�/�ݽð� ���� ����
        float direction = Random.value < 0.5f ? -1f : 1f;

        // Y������ ȸ������ ����
        Vector3 torque = new Vector3(0f, direction * RotPower, 0f);
        rb.AddTorque(torque, ForceMode.Impulse);
    }
}
