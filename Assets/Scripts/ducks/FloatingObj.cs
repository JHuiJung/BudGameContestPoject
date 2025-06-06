using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingObj : MonoBehaviour
{
    [Header("Wave Sway Settings")]
    public float swayAmount = 5f;
    public float swaySpeed = 1.5f;

    [Header("Floating Settings")]
    public float floatHeight = 0.5f;      // ���Ʒ� �̵� ����
    public float floatSpeed = 1.0f;       // ���Ʒ� �̵� �ӵ�

    [Header("Drift Settings")]
    public float driftZ = 1f;       // Z�� �������� �귯���� �ӵ�
    public float driftX = 0f;

    bool isWater = true;
    bool isStop = false;

    private float swayTimer = 0f;
    private float floatTimer = 0f;

    private Rigidbody rb;
    private Vector3 initialPosition;
    //private float driftZ = 0f;

    public bool isNormal = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        Physics.gravity = new Vector3(0, -2f, 0); // �߷� ũ�� ����

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isNormal)
        {
            HandleWaveSwayNormal();
        }
        else
        {
            HandleWaveSwayAbNormal();
        }
    }

    void HandleWaveSwayAbNormal()
    {
        if (!isWater)
            return;
        if (isStop)
            return;

        swayTimer += Time.fixedDeltaTime * swaySpeed;
        float swayX = Mathf.Sin(swayTimer) * swayAmount;

        // ȸ�� (�¿� ��鸲)
        Quaternion swayRotation = Quaternion.Euler(swayX, rb.rotation.eulerAngles.y, rb.rotation.eulerAngles.z);
        rb.MoveRotation(swayRotation);

        // Z�� �̵� (õõ�� �귯��)
        float dZ = Time.fixedDeltaTime * driftZ;
        float dX = Time.fixedDeltaTime * driftX;

        Vector3 newPosition = new Vector3(
            rb.position.x + dX,
            rb.position.y,
            rb.position.z + dZ
        );
        rb.MovePosition(newPosition);
    }

    void HandleWaveSwayNormal()
    {
        if (!isWater)
            return;
        if (isStop)
            return;

        swayTimer += Time.fixedDeltaTime * swaySpeed;
        float swayX = Mathf.Sin(swayTimer) * swayAmount;

        // ȸ�� (�¿� ��鸲)
        Quaternion swayRotation = Quaternion.Euler(swayX, rb.rotation.eulerAngles.y, rb.rotation.eulerAngles.z);
        rb.MoveRotation(swayRotation);

        // ���Ʒ� �̵� (���� �� �� �ִ� ����)
        floatTimer += Time.fixedDeltaTime * floatSpeed;
        float floatY = Mathf.Sin(floatTimer) * floatHeight;

        // Z�� �̵� (õõ�� �귯��)
        float dZ = Time.fixedDeltaTime * driftZ;
        float dX = Time.fixedDeltaTime * driftX;

        Vector3 newPosition = new Vector3(
            rb.position.x + dX,
            initialPosition.y + floatY,
            rb.position.z + dZ
        );
        rb.MovePosition(newPosition);
    }


    public void AddForce(Vector3 dir, float force)
    {
        isStop = true;

    }

    public void Dash(Vector3 targetVec, float time)
    {
        isStop= true;

        transform.DOMove(targetVec, time).SetEase(Ease.InBack)
            .OnComplete(() => {

                isStop = false;
            });
    }

    public void ChangeDriftX(float _driftX)
    {
        driftX = _driftX;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            isWater = true;
        }
    }
}
