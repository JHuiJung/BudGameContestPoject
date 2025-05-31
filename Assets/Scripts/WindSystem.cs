using UnityEngine;

public class WindSystem : MonoBehaviour
{
    [Header("바람 설정")]
    [Range(0f,10f)]
    public float minInterval = 1f;      // 바람이 부는 최소 간격
    [Range(0f, 10f)]
    public float maxInterval = 3f;      // 바람이 부는 최대 간격
    public float windTorque = 10f;      // 바람의 세기 (회전력)


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
            Debug.LogError("WindSystem: Rigidbody가 없습니다.");
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
