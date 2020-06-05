using UnityEngine;

public class CameraMotor : MonoBehaviour
{


    public Transform lookAt;
    public Vector3 offset;


    private void LateUpdate()
    {

        Vector3 desiredPosition = lookAt.position + offset;

        Vector3 desiredPositionWithLerp = Vector3.Lerp(transform.position, desiredPosition, (Time.deltaTime * 2f));

        // To remove the lerp when jumping or changing lanes
        desiredPositionWithLerp.x = desiredPosition.x;
        desiredPositionWithLerp.y = desiredPosition.y;

        transform.position = desiredPositionWithLerp;

    }


}
