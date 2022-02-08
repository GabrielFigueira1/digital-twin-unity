using UnityEngine;
using UnityEngine.UI;
using MySql.Data;

public class DatabaseHandler : MonoBehaviour
{
    [SerializeField]
    private string server = "";
    [SerializeField]
    private string database = "";
    [SerializeField]
    private string userID = "";
    [SerializeField]
    private string password = "";

    private MySql.Data.MySqlClient.MySqlConnection conn;

    void Start()
    {
        string connectionString = "server=" + server + ";" +
                                  "database=" + database + ";" +
                                  "uid=" + userID +";" +
                                  "pwd=" + password;
        Debug.Log(connectionString);
        try
        {            
            conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            conn.Open();
            Debug.Log("Connected to database.");
        }
        catch
        {
            Debug.LogError("Error in database connection");
        }
    }
}