using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class Duck_Horn : DuckBase
{
    public float Seach_Range = 3f;
    public float DashPower = 300f;
    public float DashTime = 1f;

    [SerializeField]
    bool isAttacked = false;

    public float _Xspeed = 1;

    FloatingObj floatingObj;

    Ship _ship;
    // Start is called before the first frame update
    void Start()
    {
        _ship = FindAnyObjectByType<Ship>();

        StartRot();
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }

    void Setup()
    {
        floatingObj = GetComponent<FloatingObj>();

        float _x = 1f;
        _x = Random.Range(-0.5f, 0.5f) > 0 ? 1 : -1;
        _x *= _Xspeed;
        floatingObj.ChangeDriftX(_x);
    }

    void CheckPlayer()
    {

        

        if (isAttacked)
            return;
        Vector3 playerPos = _ship.gameObject.transform.position;
        Vector3 thisPos = this.transform.position;
        Vector3 dir = (playerPos - thisPos);





        if (dir.magnitude < Seach_Range && !isAttacked)
        {
            isAttacked = true; // 여기서 바로 true로
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            this.transform.LookAt(thisPos - dir.normalized);
            MasterAudio.PlaySound("DuckSFX");

            DuckEvent();
        }


    }

    public override void DuckEvent()
    {

        Vector3 playerPos = _ship.gameObject.transform.position;
        Vector3 thisPos = this.transform.position;
        Vector3 dir = (playerPos - thisPos).normalized;

        Vector3 targetVec = new Vector3(playerPos.x, thisPos.y, playerPos.z);

        DOVirtual.DelayedCall(0.5f, () =>
        {
            this.GetComponent<FloatingObj>().Dash(playerPos, DashTime);
        }).OnUpdate(() =>
        {
            this.transform.LookAt(thisPos - dir.normalized);
        });

        
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
