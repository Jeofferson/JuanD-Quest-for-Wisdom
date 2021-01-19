using UnityEngine;

public class MainCamera : MonoBehaviour
{

    private bool hasPlayerDied;

    public Transform lookAt;

    void Update()
    {
        if (!hasPlayerDied) { return; }

        transform.LookAt(lookAt);
    }

    public void Die()
    {
        hasPlayerDied = true;
        GetComponent<Animator>().SetTrigger("Die");
    }

}
