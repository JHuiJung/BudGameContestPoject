using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DuckType
{
    Origin,
    White,
    Horn,
    WaterGun,
    Swan
}

[System.Serializable]
public class SpawnedDuck
{
    public DuckType Type;
    public Transform spawnTf;
}

public class DuckSpawner : MonoBehaviour
{
    [Header("DuckObj")]
    public GameObject obj_Origin;
    public GameObject obj_White;
    public GameObject obj_Horn;
    public GameObject obj_WaterGun;
    public GameObject obj_Swan;

    [Header("SpawnList"), Space(10)]
    public List<SpawnedDuck> spawnedDucks;

    bool isTriggered = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.tag == "Player")
        {
            isTriggered = true;

            SpawnDucks();
        }
    }

    void SpawnDucks()
    {

        foreach (var duck in spawnedDucks)
        {
            Transform duckTf = duck.spawnTf;
            DuckType duckType = duck.Type;
            GameObject obj = GetDuckObjByType(duckType);


            if( obj != null)
            {
                Instantiate(obj, duckTf.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError($"{obj.name}의 프리팹이 존재하지 않음");
            }
            
            

        }
    }

    public void ResetSpawner()
    {
        isTriggered=false;
    }

    GameObject GetDuckObjByType(DuckType type)
    {
        switch (type)
        {
            case DuckType.Origin:
                return obj_Origin;
            case DuckType.White:
                return obj_White;
            case DuckType.Horn:
                return obj_Horn;
            case DuckType.WaterGun:
                return obj_WaterGun;
            case DuckType.Swan:
                return obj_Swan;
            default:
                Debug.LogWarning("Unknown DuckType: " + type);
                return null;
        }
    }
}
