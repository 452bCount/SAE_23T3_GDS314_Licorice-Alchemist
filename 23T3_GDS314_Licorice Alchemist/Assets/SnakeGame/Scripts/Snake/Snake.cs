using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [Header("MOVEMENT & ROTATION")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;

    [Separator]

    [Header("EDIT EFFECTS")]
    [SerializeField] Color exColor;

    [Separator]

    [Header("REFRENCE DRAG & DROP")]
    [SerializeField] Camera snakeCamera = null;
    [SerializeField] GameObject explosionEffect = null;

    private Vector2 _inputM;
    private float _inputR;

    Vector3 targetPos;
    bool isMoving;

    private List<Transform> _segments;
    public Transform segmentPrefab;
    [HideInInspector] public bool _isDead;

    public void SetStart()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    // Update is called once per frame
    public void SetUpdate()
    {
        InputLeftUpdate();
        InputRightUpdate();

        #region Move
        if (!isMoving)
        {
            if (_inputM != Vector2.zero || !_isDead) // I only put this so it could stop when pause
            {
                for (int i = _segments.Count -1; i > 0; i--)
                {
                    StartCoroutine(Move(_segments[i - 1].position, _segments[i].transform, true, i));
                }

                // Calculate the direction based on the current rotation
                Vector3 direction = Quaternion.Euler(0, _inputR, 0) * Vector3.forward;

                // Update targetPos based on the direction
                targetPos = transform.position + direction;

                IsCheckFood(targetPos);
                IsCheckWall(targetPos);
                IsCheckSegment(targetPos);
                StartCoroutine(Move(targetPos, transform));
            }
        }
        #endregion
    }

    public void InputLeftUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _inputM = Vector2.left;
            _inputR -= 90f;
        }
    }

    public void InputRightUpdate()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _inputM = Vector2.right;
            _inputR += 90f;
        }
    }

    IEnumerator Move(Vector3 targetPos, Transform _targetTransform, bool _isSegment = false, int index = 0)
    {
        if (!_isSegment)
        {
            isMoving = true;
        }

        while (_targetTransform && !_isDead && (_targetTransform.position - targetPos).sqrMagnitude > Mathf.Epsilon)
        {
            _targetTransform.position = Vector3.MoveTowards(_targetTransform.position, targetPos, moveSpeed * Time.deltaTime);

            if (_isSegment)
            {
                if (_segments.Count > index - 1 && _segments[index - 1] != null)
                {
                    // Calculate the direction to the next segment
                    Vector3 directionToNext = (_segments[index - 1].position - _targetTransform.position).normalized;

                    // Rotate toward the next segment
                    Quaternion targetRotation = Quaternion.LookRotation(directionToNext, Vector3.up);
                    _targetTransform.rotation = Quaternion.Slerp(_targetTransform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                }
            }
            else
            {
                // Calculate the target rotation
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, _inputR, 0));

                // Lerp between the current rotation and the target rotation
                if (_targetTransform)
                {
                    _targetTransform.rotation = Quaternion.Lerp(_targetTransform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                }
            }

            yield return null;
        }

        if (_targetTransform && !_isDead)
        {
            _targetTransform.position = targetPos;
        }

        if (!_isSegment)
        {
            isMoving = false;
        }
    }


    void IsCheckFood(Vector3 targetPos)
    {
        Collider[] colliders = Physics.OverlapSphere(targetPos, 0.3f);

        foreach (Collider c in colliders)
        {
            if (c.GetComponent<Food>() != null)
            {
                Grow(c.GetComponent<Food>().asset.transform, c.GetComponent<Food>());
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
                if (!_isDead)
                {
                    StartCoroutine(DestroySnake());
                }
            }
        }
    }

    void IsCheckSegment(Vector3 targetPos)
    {
        Collider[] colliders = Physics.OverlapSphere(targetPos, 0.3f);

        foreach (Collider c in colliders)
        {
            if (c.GetComponent<SnakeSegment>() != null)
            {
                if (!_isDead)
                {
                    StartCoroutine(DestroySnake());
                }
            }
        }
    }


    private void Grow(Transform food, Food _base)
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
        food.transform.parent = segment.transform;
        food.localPosition = Vector3.zero;

        // Rotate the head immediately after eating
        _segments[0].rotation = Quaternion.Euler(0, _inputR, 0);

        segment.GetComponent<SnakeSegment>().exColor = _base._base.ColorEx;

        LevelStageController.Instance.levelStage.foodGottenList.Add(_base._base);
        LevelStageController.Instance.levelStage.SetEaten();
    }

    IEnumerator DestroySnake()
    {
        _isDead = true;

        if (snakeCamera != null)
        {
            snakeCamera.transform.parent = null;
        }

        for (int i = _segments.Count - 1; i >= 0; i--)
        {
            SnakeSegment snakeSegment = _segments[i].GetComponent<SnakeSegment>();
            if (snakeSegment != null)
            {
                snakeSegment.DeathExplode();
            }

            Snake snakeHead = _segments[i].GetComponent<Snake>();
            if (snakeHead != null)
            {
                snakeHead.DeathExplode();
            }

            Destroy(_segments[i].gameObject);
            _segments.RemoveAt(i);
            yield return new WaitForSeconds(0.5f); // Adjust the delay between segment destructions
        }
    }

    public void DeathExplode()
    {
        // play graphics
        GameObject explodeAsset;
        explodeAsset = Instantiate(explosionEffect, this.transform.position, this.transform.rotation);
        explodeAsset.GetComponent<FinishEffect>().exColorA = exColor;
        explodeAsset.GetComponent<FinishEffect>().SetUp();
        // play sound

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetPos, 0.3f);
    }
}
