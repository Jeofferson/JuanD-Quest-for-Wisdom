using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    private const float DISTANCE_BEHIND_PLAYER_BEFORE_RESPAWN_TERRAIN = 300f;

    public float scrollSpeed;
    public float totalLength;

    private float scrollLocation;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!IsScrolling) { return; }

        scrollLocation += scrollSpeed * Time.deltaTime;
        Vector3 newLocation = (playerTransform.position.z + scrollLocation) * Vector3.forward;
          
        transform.position = newLocation;

        if (transform.GetChild(0).transform.position.z < playerTransform.position.z - DISTANCE_BEHIND_PLAYER_BEFORE_RESPAWN_TERRAIN)
        {
            SpawnNextTerrain();
            //SpawnNextTerrain();
        }
    }

    private void SpawnNextTerrain()
    {
        transform.GetChild(0).localPosition += Vector3.forward * totalLength;
        transform.GetChild(0).SetSiblingIndex(transform.childCount);
    }

    public bool IsScrolling { get; set; }

}
