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
    public float driftSpeed = 0.2f;       // Z�� �������� �귯���� �ӵ�

    bool isWater = true;
    bool isStop = false;

    private float swayTimer = 0f;
    private float floatTimer = 0f;

    private Rigidbody rb;
    private Vector3 initialPosition;
    private float driftZ = 0f;

    void Start()
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
        driftZ = initialPosition.z;
    }

    private void FixedUpdate()
    {
        HandleWaveSway();
    }

    void HandleWaveSway()
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
        driftZ += Time.fixedDeltaTime * driftSpeed;
        float dZ = Time.fixedDeltaTime * driftSpeed;

        Vector3 newPosition = new Vector3(
            rb.position.x,
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            isWater = true;
        }
    }
}
