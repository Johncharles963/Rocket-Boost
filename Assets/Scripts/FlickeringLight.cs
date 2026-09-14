using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    Light pointLight;
    float lightFactor;
    [SerializeField] float speed = 10f;

    void Start()
    {
        pointLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lightFactor = Mathf.PingPong(Time.time * speed, 200f);

        pointLight.intensity = lightFactor;
    }
}
