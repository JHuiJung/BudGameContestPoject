using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck_Origin : DuckBase
{
    public GameObject obj_Duck_Origin;
    public GameObject obj_Duck_Goose;
    public GameObject obj_Duck_Gold;

    public float weight_Duck_Origin = 10f;
    public float weight_Duck_Goose = 10f;
    public float weight_Duck_Gold = 10f;

    public float _Xspeed = 1;

    FloatingObj floatingObj;

    private void Start()
    {
        DuckEvent();

        
        StartRot();
        Setup();
    }

    void Setup()
    {
        floatingObj = GetComponent<FloatingObj>();

        float _x = 1f;
        _x = Random.Range(-0.5f, 0.5f) > 0 ? 1 : -1;
        _x *= _Xspeed;
        floatingObj.ChangeDriftX(_x);
    }

    [ContextMenu("Duck_Chose")]
    public override void DuckEvent()
    {
        // 총 가중치 계산
        float totalWeight = weight_Duck_Origin + weight_Duck_Goose + weight_Duck_Gold;

        // 랜덤 값 추출
        float rand = Random.Range(0f, totalWeight);

        // 오브젝트 선택 및 설정
        if (rand < weight_Duck_Origin)
        {
            obj_Duck_Origin.SetActive(true);
            obj_Duck_Goose.SetActive(false);
            obj_Duck_Gold.SetActive(false);
        }
        else if (rand < weight_Duck_Origin + weight_Duck_Goose)
        {
            obj_Duck_Origin.SetActive(false);
            obj_Duck_Goose.SetActive(true);
            obj_Duck_Gold.SetActive(false);
        }
        else
        {
            obj_Duck_Origin.SetActive(false);
            obj_Duck_Goose.SetActive(false);
            obj_Duck_Gold.SetActive(true);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Water" &&
            collision.gameObject.tag != "Duck")
        {
            float _xx = floatingObj.driftX * -1;
            floatingObj.ChangeDriftX(_xx);
        }
    }

}
