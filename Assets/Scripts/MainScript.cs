using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public Request req;

    public GameObject anomalieArrow;

    [SerializeField]
    private const float databaseUpdateInterval = 0.125f;
    private float nextReadTime = 0f;
    private DatabaseInstance databaseInstance;
    [SerializeField]
    private ScrollViewHandler scrollView;

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
                     if (databaseInstance.jsonAnomalies.anomalieId != 0)
                     {
                         try
                         {
                             GameObject product = GameObject.Find("Product(Clone)");
                             Instantiate(anomalieArrow,
                             new Vector3(product.transform.position.x,
                                        product.transform.position.y + 0.5f,
                                        product.transform.position.z),
                             Quaternion.identity);
                         }
                         catch (System.Exception)
                         {

                             throw;
                         }

                         scrollView.Log(databaseInstance.jsonAnomalies.anomalieId.ToString() + ": " + databaseInstance.jsonAnomalies.message);
                     }
                     else if (databaseInstance.jsonAnomalies.anomalieId == 0)
                     {
                         scrollView.Log(databaseInstance.jsonAnomalies.message);
                     }
                 }
             }
            );
            nextReadTime = Time.time + databaseUpdateInterval;
        }
    }
}
