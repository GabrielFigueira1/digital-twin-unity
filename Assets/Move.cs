using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject waypointArm;
    public GameObject waypointCurve;
    public GameObject waypointExit_1;
    public GameObject waypointExit_2;
    public ScrollViewHandler scrollView;
    public GameObject anomalieArrow;
    private float speed = 1.5f;

    [SerializeField]
    private float lifetimeLimit = 13f;

    private Vector3 direction;
    private Vector3 nextWaypoint;
    private DatabaseInstance databaseInstance;

    float spawnTimestamp;
    enum Path
    {
        Start,
        Exit_1,
        Exit_2,
        Exit_2_end
    }

    Path path = Path.Start;

    // Start is called before the first frame update
    void Start()
    {
        waypointArm = GameObject.Find("WaypointArm");
        //waypointCurve = GameObject.Find("WaypointCurve");
        waypointExit_1 = GameObject.Find("WaypointExit_1");
        waypointExit_2 = GameObject.Find("WaypointExit_2");
        scrollView = GameObject.FindObjectOfType<ScrollViewHandler>();
        databaseInstance = DatabaseInstance.GetInstance();

        direction = (waypointArm.transform.position - transform.position).normalized;
        nextWaypoint = waypointArm.transform.position;

        spawnTimestamp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        switch (path)
        {
            case Path.Start:
                if ((transform.position - nextWaypoint).magnitude > 0.01)
                {
                    transform.Translate(direction * Time.deltaTime * speed * 0.6f);
                }
                else
                {
                    transform.position = waypointArm.transform.position;
                    nextWaypoint = waypointExit_1.transform.position;
                    path = Path.Exit_1;
                    updateDirection(nextWaypoint, waypointArm.transform.position);
                }
                if (databaseInstance.jsonData.Status_M122 == 1 && (transform.position - nextWaypoint).magnitude <= 0.01)
                {
                    transform.position = waypointArm.transform.position;
                    nextWaypoint = waypointExit_2.transform.position;
                    path = Path.Exit_2_end;
                    updateDirection(nextWaypoint, waypointArm.transform.position);
                }
                break;
            case Path.Exit_1:
                if ((transform.position - nextWaypoint).magnitude > 0.01)
                {
                    transform.Translate(direction * Time.deltaTime * speed * 0.75f);
                }
                else if ((transform.position - nextWaypoint).magnitude <= 0.01 && databaseInstance.jsonData.Status_M103 == 1)
                {
                    transform.position = waypointExit_1.transform.position;
                    Destroy(gameObject, 1);
                }
                break;
            /*case Path.Exit_2:
                if ((transform.position - nextWaypoint).magnitude > 0.01)
                {
                    transform.Translate(direction * Time.deltaTime * speed * 0.7f);
                }
                else
                {
                    transform.position = waypointCurve.transform.position;
                    nextWaypoint = waypointExit_2.transform.position;
                    path = Path.Exit_2_end;
                    updateDirection(nextWaypoint, waypointCurve.transform.position);
                }
                break;
            */
            case Path.Exit_2_end:
                if ((transform.position - nextWaypoint).magnitude > 0.01)
                {
                    transform.Translate(direction * Time.deltaTime * speed * .6f);
                }
                else if ((transform.position - nextWaypoint).magnitude <= 1 && databaseInstance.jsonData.Status_M104 == 1)
                {
                    transform.position = waypointExit_2.transform.position;
                    Destroy(gameObject, 1);
                }
                break;
            default:
                break;
        }
        if (Time.time > spawnTimestamp + lifetimeLimit){
            scrollView.Log("Erro na virtualiza????o do produto. Pode ser que hajam anomalias na planta ou o produto tenha sido removido indevidamente.");
            //Debug.LogError("Lifetime period exceded limit. Product did not receive an instruction to be destroyed. Destroying now.");
            Instantiate(anomalieArrow,
                        new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
                        Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void updateDirection(Vector3 nextW, Vector3 lastW)
    {
        direction = (nextW - lastW).normalized;
    }
}
