using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 _input = Vector2.up;
    Vector3 targetPos;
    bool isMoving;

    private List<Transform> _segments;
    public Transform segmentPrefab;

    [ReadOnly]
    public bool isGameOver;

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameOver)
        {
            return;
        }

        #region Input
        // The input of the snake
        if (Input.GetKeyDown(KeyCode.W))
        {
            _input = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _input = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _input = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _input = Vector2.right;
        }
        #endregion

        #region Move
        if (!isMoving)
        {
            if (_input != Vector2.zero) // I only put this so it could stop when pause
            {
                for (int i = _segments.Count -1; i > 0; i--)
                {
                    StartCoroutine(MoveSegment(_segments[i - 1].position, _segments[i].transform));
                }

                targetPos = transform.position;
                targetPos.x += _input.x;
                targetPos.z += _input.y;

                IsCheckFood(targetPos);
                IsCheckWall(targetPos);
                IsCheckSegment(targetPos);
                StartCoroutine(Move(targetPos));
            }
        }
        #endregion
    }

    // To move Grid per Grid
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }

    IEnumerator MoveSegment(Vector3 targetPos, Transform _segment)
    {
        while ((targetPos - _segment.transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            _segment.transform.position = Vector3.MoveTowards(_segment.transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        _segment.transform.position = targetPos;
    }

    void IsCheckFood(Vector3 targetPos)
    {
        Collider[] colliders = Physics.OverlapSphere(targetPos, 0.3f);

        foreach (Collider c in colliders)
        {
            if (c.GetComponent<Food>() != null)
            {
                Grow(c.GetComponent<Food>().asset);
                Destroy(c.gameObject);
            }
        }
    }

    void IsCheckWall(Vector3 targetPos)
    {
        Collider[] colliders = Physics.OverlapSphere(targetPos, 0.3f);

        foreach (Collider c in colliders)
        {
            if (c.tag == "Wall")
            {
                foreach (var seg in _segments)
                {
                    Destroy(seg.gameObject);
                }
            }
        }
    }

    void IsCheckSegment(Vector3 targetPos)
    {
        Collider[] colliders = Physics.OverlapSphere(targetPos, 0.3f);

        foreach (Collider c in colliders)
        {
            foreach (var seg in _segments)
            {
                if (seg != null && c == seg.GetComponent<Collider>())
                {
                    _input = Vector2.zero;
                    isGameOver = true;
                }
            }
        }
    }


    private void Grow(Transform food)
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
        food.transform.parent = segment.transform;
        food.localPosition = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetPos, 0.3f);
    }
}
