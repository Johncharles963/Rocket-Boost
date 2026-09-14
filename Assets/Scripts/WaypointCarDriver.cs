using Unity.VisualScripting;
using UnityEngine;

public class WaypointCarDriver : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed;
    [SerializeField]  float rotationSpeed;


    int curentWaypointIndex = 0;
    bool isInitialized = false;


    // Update is called once per frame
    void Update()
    {
        if (!isInitialized || curentWaypointIndex >= waypoints.Length) return;

        Transform targetWapoint = waypoints[curentWaypointIndex];

        transform.position =Vector3.MoveTowards(transform.position, targetWapoint.position, speed * Time.deltaTime);
        
        Vector3 direction = targetWapoint.position - transform.position;

        if(direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, targetWapoint.position) < 0.5f)
        {
            curentWaypointIndex++;
        }
    }

    public void InitializeWaypoint(Transform[] sceneWaypoints)
    {
        waypoints = sceneWaypoints;
        isInitialized = true;
    }
}
