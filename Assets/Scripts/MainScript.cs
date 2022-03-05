using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public Request req;

    [SerializeField]
    private const float databaseUpdateInterval = 0.1f;
    private float nextReadTime = 0f;
    private DatabaseInstance databaseInstance;

    // Start is called before the first frame update
    void Awake()
    {
        databaseInstance = DatabaseInstance.GetInstance();
    }
    void Start()
    {

        req.TestConnection((requestBody) =>
            {
                if (requestBody == "ERROR")
                {
                    Debug.LogError("Error on server connection.");
                    return;
                }
                Debug.Log(requestBody);
            }
        );
        req.ReadStatus((requestBody) =>
            {
                databaseInstance.jsonData = JsonUtility.FromJson<JsonData>(requestBody);
            }
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextReadTime)
        {
            req.ReadStatus((requestBody) =>
             {
                databaseInstance.jsonData = JsonUtility.FromJson<JsonData>(requestBody);
                //Debug.Log(database.Status_M101);
             }
            );
            req.ReadAnomalies((requestBody) =>
             {
                databaseInstance.jsonAnomalies = JsonUtility.FromJson<JsonAnomalies>(requestBody);
                if (databaseInstance.jsonAnomalies != null){
                     Debug.LogWarning("Anomalie number: " + databaseInstance.jsonAnomalies.anomalieId);
                     Debug.LogWarning(databaseInstance.jsonAnomalies.message);
                     Debug.Log("");
                 }
             }
            );
            nextReadTime = Time.time + databaseUpdateInterval;
        }
    }
}
