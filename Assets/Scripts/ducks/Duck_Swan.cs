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

        // 0도에서 360도 사이의 랜덤한 각도 생성
        float randomY = Random.Range(0f, 360f);

        // 현재 오브젝트의 회전값을 랜덤한 Y값으로 설정
        transform.rotation = Quaternion.Euler(0f, randomY, 0f);


        // 1 또는 -1 중 랜덤하게 선택해서 시계/반시계 방향 결정
        float direction = Random.value < 0.5f ? -1f : 1f;

        // Y축으로 회전력을 가함
        Vector3 torque = new Vector3(0f, direction * RotPower, 0f);
        rb.AddTorque(torque, ForceMode.Impulse);
    }
}
