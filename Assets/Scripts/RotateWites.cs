using UnityEngine;

public class RotateWites : MonoBehaviour
{
    // Rotates the object around a target point 20 degrees per second
    public Transform targetPivot;
    float rotateTime;


    void Update()
    {
        rotateTime += Time.deltaTime;
        if(rotateTime < 3)
        {
            transform.RotateAround(targetPivot.position, Vector3.forward, 20 * Time.deltaTime);
        }
        else if(rotateTime < 6)
        {
            transform.RotateAround(targetPivot.position, Vector3.back, 20 * Time.deltaTime);
        }
        else
        {
            rotateTime = 0;
        }
        
    }

}
