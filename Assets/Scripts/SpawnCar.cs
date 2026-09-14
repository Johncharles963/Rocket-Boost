using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    float timer;
    [SerializeField] float spawnRateLow;
    [SerializeField] float spawnRateHigh;
    [SerializeField] GameObject[] cars;
    [SerializeField] Transform[] waypoints;

    float spawnRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnRate = Random.Range(spawnRateLow, spawnRateHigh);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnRate)
        {
            SpawnCars();
            timer = 0;
            spawnRate = Random.Range(spawnRateLow, spawnRateHigh);
        }
    }

    void SpawnCars()
    {
        if (cars.Length == 0 || waypoints.Length == 0) return;

        int randomCarIndex = Random.Range(0, cars.Length);
        GameObject selectedCard = cars[randomCarIndex];

        GameObject spawnedCar = Instantiate(selectedCard, transform.position, transform.rotation);
        WaypointCarDriver carScript = spawnedCar.GetComponent<WaypointCarDriver>();

        if(carScript != null)
        {
            carScript.InitializeWaypoint(waypoints);
        }

    }
}
