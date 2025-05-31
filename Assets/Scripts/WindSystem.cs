using UnityEngine;

public class WindSystem : MonoBehaviour
{
    [Header("�ٶ� ����")]
    [Range(0f,10f)]
    public float minInterval = 1f;      // �ٶ��� �δ� �ּ� ����
    [Range(0f, 10f)]
    public float maxInterval = 3f;      // �ٶ��� �δ� �ִ� ����
    public float windTorque = 10f;      // �ٶ��� ���� (ȸ����)


    public GameObject targetObj;
    private Rigidbody rb;
    private bool isBlowing = false;
    private float _time = 0f;
    private float _timeThreshold = 0f;


    void Start()
    {
        rb = targetObj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("WindSystem: Rigidbody�� �����ϴ�.");
            enabled = false;
            return;
        }


        _timeThreshold = Random.Range(minInterval, maxInterval);

    }

    public void WindUpdate()
    {
        _time += Time.deltaTime;

        if (_time > _timeThreshold)
        {
            _timeThreshold = Random.Range(minInterval, maxInterval);

            _time = 0f;

            float tiltInput = Random.Range(-1, 1);
            tiltInput = tiltInput > 0 ? 1f : -1f;


            rb.AddTorque(new Vector3(0, 0, 1f) * windTorque * tiltInput, ForceMode.Acceleration);

        }
    }




}
