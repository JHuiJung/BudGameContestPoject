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

}
