using UnityEngine;

public class CameraMotor : MonoBehaviour
{


    private bool hasStartedRunning;

    public MainCamera mainCamera;

    public Transform lookAt;
    public Vector3 offset;


    private void LateUpdate()
    {

        if (!hasStartedRunning) { return; }

        Vector3 desiredPosition = lookAt.position + offset;

        Vector3 desiredPositionWithLerp = Vector3.Lerp(transform.position, desiredPosition, (Time.deltaTime * 2f));

        // To remove the lerp when jumping or changing lanes
        desiredPositionWithLerp.x = desiredPosition.x;
        desiredPositionWithLerp.y = desiredPosition.y;

        transform.position = desiredPositionWithLerp;

    }


    public void StartRunning()
    {

        hasStartedRunning = true;

    }


    public void Die()
    {

        hasStartedRunning = false;

        mainCamera.Die();

    }


}
