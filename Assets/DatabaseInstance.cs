using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseInstance : MonoBehaviour
{
    private static DatabaseInstance databaseInstance = new DatabaseInstance();
    public JsonData jsonData = new JsonData();

    public static DatabaseInstance GetInstance()
    {
        return databaseInstance;
    }
}