using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Duck_Horn : DuckBase
{
    public float Seach_Range = 3f;
    public float DashPower = 300f;
    public float DashTime = 1f;

    [SerializeField]
    bool isAttacked = false;


    Ship _ship;
    // Start is called before the first frame update
    void Start()
    {
        _ship = FindAnyObjectByType<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
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
            this.transform.LookAt(thisPos - dir.normalized);
            DuckEvent();
        }


    }

    [ContextMenu("TEST")]
    public void TEST()
    {

        Vector3 playerPos = _ship.gameObject.transform.position;
        Vector3 thisPos = this.transform.position;
        Vector3 dir = (playerPos - thisPos);

        this.GetComponent<Rigidbody>().AddForce(Vector3.up
            * DashPower, ForceMode.Impulse);
        

    }

    public override void DuckEvent()
    {
        Vector3 playerPos = _ship.gameObject.transform.position;
        Vector3 thisPos = this.transform.position;
        Vector3 dir = (playerPos - thisPos).normalized;

        Vector3 targetVec = new Vector3(playerPos.x, thisPos.y, playerPos.z);

        this.GetComponent<FloatingObj>().Dash(playerPos, DashTime);
    }
}
