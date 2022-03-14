using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmitProducts : MonoBehaviour
{
    private JsonData database;

    [SerializeField]
    private GameObject productPrefab;

    [SerializeField]
    private GameObject inputWaypoint;

    private DatabaseInstance databaseInstance;

    int lastStatusM101 = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        databaseInstance = DatabaseInstance.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (canEmmit())
        {
            Instantiate(productPrefab, inputWaypoint.transform.position, Quaternion.identity);
        }
    }

    bool canEmmit()
    {
        //Debug.Log(databaseInstance.jsonData.Status_M101);
        if (databaseInstance.jsonData.Status_M102 == 1 && lastStatusM101 == 0)
        {
            lastStatusM101 = databaseInstance.jsonData.Status_M102;
            return true;
        }
        lastStatusM101 = databaseInstance.jsonData.Status_M102;
        return false;
    }
}
