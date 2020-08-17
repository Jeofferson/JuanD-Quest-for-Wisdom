using UnityEngine;

public class FootholdMotor : MonoBehaviour
{


    private Transform playerTransform;


    private void Start()
    {

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }


    private void Update()
    {

        transform.position = playerTransform.position.z * Vector3.forward;
        
    }


}
