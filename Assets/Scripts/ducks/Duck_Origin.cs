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

    private void Start()
    {
        DuckEvent();
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

}
