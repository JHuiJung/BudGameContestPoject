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
        // �� ����ġ ���
        float totalWeight = weight_Duck_Origin + weight_Duck_Goose + weight_Duck_Gold;

        // ���� �� ����
        float rand = Random.Range(0f, totalWeight);

        // ������Ʈ ���� �� ����
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
