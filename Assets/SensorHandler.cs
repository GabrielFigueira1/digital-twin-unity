using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorHandler : MonoBehaviour
{
    private DatabaseInstance databaseInstance;
    public Light sensor1;
    public Light sensor2;
    public Light sensor3;
    public MeshRenderer arm; 

    // Start is called before the first frame update
    void Start()
    {
        databaseInstance = DatabaseInstance.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (databaseInstance.jsonData.Status_M104 == 1)
            sensor1.intensity = 25;
        else
            sensor1.intensity = 1;

        if (databaseInstance.jsonData.Status_M102 == 1)
            sensor2.intensity = 25;
        else
            sensor2.intensity = 1;

        if (databaseInstance.jsonData.Status_M103 == 1)
            sensor3.intensity = 25;
        else
            sensor3.intensity = 1;
         if (databaseInstance.jsonData.Status_M122 == 1)
            arm.material.SetColor("Red", new Color(0.3f, 0.4f, 0.6f, 0.3f));
        else
            sensor3.intensity = 1;   
    }
}
