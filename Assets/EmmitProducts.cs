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

    int lastStatusM102 = 0;
    
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
        if (databaseInstance.jsonData.Status_M120 == 1 && lastStatusM102 == 0)
        {
            lastStatusM102 = databaseInstance.jsonData.Status_M120;
            return true;
        }
        lastStatusM102 = databaseInstance.jsonData.Status_M120;
        return false;
    }
}
