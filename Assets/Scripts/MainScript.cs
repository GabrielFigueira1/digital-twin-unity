using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public Request req;

    // Start is called before the first frame update
    void Start()
    {
        req.TestConnection((requestBody) => 
            {
                if(requestBody == "ERROR")
                {
                    Debug.LogError("Error on server connection.");
                    return;
                }
                Debug.Log(requestBody);
            }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
