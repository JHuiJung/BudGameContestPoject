using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;


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

    [Header("GameOver")]
    public GameObject Cam_GameOver;
    public GameObject UI_GameOver;
    //public GameObject UI_Main;
    public bool isGameOver = false;


    [Header("GameStart")]
    public Vector3 startPos = Vector3.zero; 
    public Vector3 startRot = Vector3.zero; 


    private Vector3 velocity = Vector3.zero;
    private float swayTimer = 0f;

    private Rigidbody rb;
    private WindSystem windSystem;

    public bool isGameStart = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        windSystem = GetComponent<WindSystem>();

        Physics.gravity = new Vector3(0, -2f, 0); // �߷� ũ�� ����

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX;

        MasterAudio.PlaySound("water_Slow");
        MasterAudio.PlaySound("Froest_1");
    }

    void FixedUpdate()
    {
        if (isGameOver)
            return;

        HandleWaveSway();

        if (!isGameStart)
            return;

        HandleMovement();
        HandleTiltControl();
        windSystem.WindUpdate();
        
        
        
        CheckGameover();
    }

    public void CheckGameover()
    {
        if(isGameOver) return;

        float _nowZ = transform.rotation.z;
        float _nowX = transform.rotation.x;
        float _Amount = new Vector3(_nowX, 0, _nowZ).magnitude;

        if (_Amount > 0.50f)
        {
            StartCoroutine(GameOver());
        }


    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // ���� ����
        //Vector3 forwardMovement = transform.forward * moveInput * acceleration * Time.fixedDeltaTime;
        Vector3 forwardMovement = transform.forward * 1f * acceleration * Time.fixedDeltaTime;
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

    [ContextMenu("GameStart")]
    public void GameStart()
    {
        isGameOver = false;

        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ ;

        this.transform.position = startPos;
        this.transform.eulerAngles = startRot;

        velocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX;


        MasterAudio.StartPlaylist("BGM");
        MasterAudio.PlaySound("water_Slow");
        MasterAudio.PlaySound("Froest_1");
        UI_GameOver.SetActive(false);
        Cam_GameOver.SetActive(false);
        isGameStart = true;

        //������ �� ���ֱ�

        DuckBase[] ducks = FindObjectsOfType<DuckBase>();

        foreach (DuckBase duck in ducks)
        {
            Destroy(duck.gameObject);
        }

        DuckSpawner[] duckSpawners = FindObjectsOfType<DuckSpawner>();

        foreach(DuckSpawner spawners in duckSpawners)
        {
            spawners.ResetSpawner();
        }

        //UI_Main.SetActive(true);
    }

    public void GameOverForce()
    {
        StartCoroutine(GameOver());
    }

    [ContextMenu("GameOver")]
    public IEnumerator GameOver()
    {
        isGameOver = true;

        Cam_GameOver.SetActive(true);
        //UI_Main.SetActive(false);

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;

        yield return new WaitForSeconds(2f);

        


        UI_GameOver.SetActive(true);
    }


    public void GameEnd()
    {
        isGameStart = false;
    }
}
