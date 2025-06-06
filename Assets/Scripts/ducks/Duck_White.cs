using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck_White : DuckBase
{
    public float _Xspeed = 1;
    public float _YForce = 10f;
    FloatingObj floatingObj;
    Rigidbody rb;
    public override void DuckEvent()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        StartRot();
        Setup();
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

    void Setup()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;



        floatingObj = GetComponent<FloatingObj>();

        float _x = 1f;
        _x = Random.Range(-0.5f, 0.5f) > 0 ? 1 : -1;
        _x *= _Xspeed;
        floatingObj.ChangeDriftX(_x);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Water" &&
            collision.gameObject.tag != "Duck")
        {
            float _xx = floatingObj.driftX * -1;
            floatingObj.ChangeDriftX(_xx);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * _YForce, ForceMode.Impulse);
        }
    }
}
