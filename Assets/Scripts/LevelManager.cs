using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{


    private const int NUM_INITIAL_TRANSITION_SEGMENTS = 4;
    private const int NUM_INITIAL_OBSTACLE_SEGMENTS = 11;

    private const float DISTANCE_BEFORE_SPAWN_SEGMENT = 100f;
    private const float CHANCE_TO_SPAWN_TRANSITION_SEGMENT = 0.25f;
    private const int NUM_CURRENT_SEGMENTS_BEFORE_DESPAWN_SEGMENT = 15;

    public bool showCollider;
    public bool showVisual;
    public bool isMoving;

    private int currentPositionY;
    private int currentSpawnZ;
    private int numCurrentActiveSegments;
    private int continuousSegments;

    private int y1;
    private int y2;
    private int y3;

    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    public List<Piece> cars = new List<Piece>();
    public List<Piece> carsReversed = new List<Piece>();
    public List<Piece> vans = new List<Piece>();
    public List<Piece> vansReversed = new List<Piece>();
    public List<Piece> buses = new List<Piece>();
    public List<Piece> busesReversed = new List<Piece>();
    public List<Piece> trucks = new List<Piece>();
    public List<Piece> trucksReversed = new List<Piece>();
    [HideInInspector]
    public List<Piece> pieces = new List<Piece>();

    public List<Segment> availableTransitionSegments = new List<Segment>();
    public List<Segment> availableObstacleSegments = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments = new List<Segment>();

    private Transform cameraContainer;


    private void Awake()
    {

        Instance = this;

        cameraContainer = Camera.main.transform;

        currentSpawnZ = 0;
        currentPositionY = 0;

    }


    private void Start()
    {

        for (int i = 0; i < NUM_INITIAL_OBSTACLE_SEGMENTS; i++)
        {

            if (i < NUM_INITIAL_TRANSITION_SEGMENTS)
            {

                SpawnTransitionSegment();

            }
            else
            {

                SpawnSegment();

            }

        }

    }


    private void Update()
    {

        if (currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN_SEGMENT)
        {

            SpawnSegment();

        }

        if (numCurrentActiveSegments >= NUM_CURRENT_SEGMENTS_BEFORE_DESPAWN_SEGMENT)
        {

            segments[numCurrentActiveSegments - 1].Despawn();
            numCurrentActiveSegments--;

        }

    }


    private void SpawnSegment()
    {

        SpawnObstacleSegment();

        if (Random.Range(0f, 1f) < (continuousSegments * CHANCE_TO_SPAWN_TRANSITION_SEGMENT))
        {

            continuousSegments = 0;
            SpawnTransitionSegment();

        }
        else
        {

            continuousSegments++;

        }

    }


    private void SpawnObstacleSegment()
    {

        List<Segment> validObstacleSegments = availableObstacleSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int segmentIndex = Random.Range(0, validObstacleSegments.Count);

        Segment segment = GetSegment(false, segmentIndex);

        y1 = segment.endY1;
        y2 = segment.endY2;
        y3 = segment.endY3;

        segment.transform.SetParent(transform);
        segment.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += segment.length;
        numCurrentActiveSegments++;

        segment.Spawn();

    }


    private void SpawnTransitionSegment()
    {

        List<Segment> validTransitonSegments = availableTransitionSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int segmentIndex = Random.Range(0, validTransitonSegments.Count);

        Segment segment = GetSegment(true, segmentIndex);

        y1 = segment.endY1;
        y2 = segment.endY2;
        y3 = segment.endY3;

        segment.transform.SetParent(transform);
        segment.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += segment.length;
        numCurrentActiveSegments++;

        segment.Spawn();

    }


    public Segment GetSegment(bool isTransitionSegment, int segmentIndex)
    {

        Segment segment = segments.Find(x => x.SegmentIndex == segmentIndex && x.isTransitionSegment == isTransitionSegment && !x.gameObject.activeSelf);

        if (segment == null)
        {

            GameObject go = Instantiate(isTransitionSegment ? availableTransitionSegments[segmentIndex].gameObject : availableObstacleSegments[segmentIndex].gameObject) as GameObject;
            segment = go.GetComponent<Segment>();

            segment.SegmentIndex = segmentIndex;
            segment.isTransitionSegment = isTransitionSegment;

            segments.Insert(0, segment);

        }
        else
        {

            segments.Remove(segment);
            segments.Insert(0, segment);

        }

        return segment;

    }


    public Piece GetPiece(PieceType pieceType, int visualIndex)
    {

        Piece piece = pieces.Find(x => x.pieceType == pieceType && x.visualIndex == visualIndex && !x.gameObject.activeSelf);

        if (piece == null)
        {

            GameObject go = null;

            switch (pieceType)
            {

                case PieceType.jump:
                    go = jumps[visualIndex].gameObject;
                    break;

                case PieceType.slide:
                    go = slides[visualIndex].gameObject;
                    break;

                case PieceType.car:
                    go = cars[visualIndex].gameObject;
                    break;

                case PieceType.carReversed:
                    go = carsReversed[visualIndex].gameObject;
                    break;

                case PieceType.van:
                    go = vans[visualIndex].gameObject;
                    break;

                case PieceType.vanReversed:
                    go = vansReversed[visualIndex].gameObject;
                    break;

                case PieceType.bus:
                    go = buses[visualIndex].gameObject;
                    break;

                case PieceType.busReversed:
                    go = busesReversed[visualIndex].gameObject;
                    break;

                case PieceType.truck:
                    go = trucks[visualIndex].gameObject;
                    break;

                case PieceType.truckReversed:
                    go = trucksReversed[visualIndex].gameObject;
                    break;

            }

            go = Instantiate(go);
            piece = go.GetComponent<Piece>();

            pieces.Add(piece);

        }

        return piece;

    }


    public static LevelManager Instance { set; get; }


}
