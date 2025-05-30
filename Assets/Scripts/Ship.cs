using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3.0f;
    public float rotationSpeed = 100.0f;
    public float acceleration = 2.0f;
    public float drag = 1.5f;

    [Header("Wave Sway Settings")]
    public float swayAmount = 5f;
    public float swaySpeed = 1.5f;

    [Header("Tilt Recovery Settings")]
    public float tiltTorque = 10f;        // Q/E ������ �� ȸ�� ��
    public float maxTiltAngle = 30f;

    private Vector3 velocity = Vector3.zero;
    private float swayTimer = 0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleWaveSway();
        HandleTiltControl();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // ���� ����
        Vector3 forwardMovement = transform.forward * moveInput * acceleration * Time.fixedDeltaTime;
        velocity += forwardMovement;

        // ȸ�� ����
        Quaternion deltaRotation = Quaternion.Euler(0f, turnInput * rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // ��ġ �̵�
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        // ����
        velocity = Vector3.Lerp(velocity, Vector3.zero, drag * Time.fixedDeltaTime);
    }

    void HandleWaveSway()
    {
        swayTimer += Time.fixedDeltaTime * swaySpeed;
        float swayX = Mathf.Sin(swayTimer) * swayAmount;

        Quaternion swayRotation = Quaternion.Euler(swayX, rb.rotation.eulerAngles.y, rb.rotation.eulerAngles.z);
        rb.MoveRotation(swayRotation);
    }

    void HandleTiltControl()
    {
        float tiltInput = 0f;
        if (Input.GetKey(KeyCode.Q)) tiltInput = 1f;
        if (Input.GetKey(KeyCode.E)) tiltInput = -1f;

        if (tiltInput != 0f)
        {
            float currentZ = rb.rotation.eulerAngles.z;
            if (currentZ > 180f) currentZ -= 360f;

            // �ִ� ���� ����
            if ((tiltInput > 0f && currentZ < maxTiltAngle) ||
                (tiltInput < 0f && currentZ > -maxTiltAngle))
            {
                rb.AddTorque(Vector3.forward * tiltInput * tiltTorque, ForceMode.Acceleration);
            }
        }
    }
}
