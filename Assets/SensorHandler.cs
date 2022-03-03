using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorHandler : MonoBehaviour
{
    private DatabaseInstance databaseInstance;
    public LineRenderer sensor1;
    public LineRenderer sensor2;
    public LineRenderer sensor3;
    public GameObject arm; 

    [SerializeField]
    private Color sensorGreen;

    [SerializeField]
    private Color sensorRed;

    // Start is called before the first frame update
    void Start()
    {
        databaseInstance = DatabaseInstance.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (databaseInstance.jsonData.Status_M104 == 1)
            SetSensorOn(sensor1);
        else
            SetSensorOff(sensor1);

        if (databaseInstance.jsonData.Status_M102 == 1)
            SetSensorOn(sensor2);
        else
            SetSensorOff(sensor2);

        if (databaseInstance.jsonData.Status_M103 == 1)
            SetSensorOn(sensor3);
        else
            SetSensorOff(sensor3);
        if (databaseInstance.jsonData.Status_M122 == 1)
            arm.transform.eulerAngles = new Vector3(90f, -90f, 90f);
        else
            arm.transform.eulerAngles = new Vector3(90f, -90f, 0f);
    }

    void SetSensorOn(LineRenderer line)
    {
        line.startColor = sensorGreen;
        line.endColor = sensorGreen;
    }

    void SetSensorOff(LineRenderer line)
    {
        line.startColor = sensorRed;
        line.endColor = sensorRed;
    }
}
