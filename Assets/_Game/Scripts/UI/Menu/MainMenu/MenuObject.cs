using System;
using UnityEngine;

public class MenuObject : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _rotate;

    private Vector3 _rotationAngle;

    private Vector2 _targetPosition;
    public event Action Ondestroyed;

    private void Start()
    {
        _rotationAngle = new(UnityEngine.Random.Range(-0.5f, 0.5f),
            UnityEngine.Random.Range(-0.5f, 0.5f),
            UnityEngine.Random.Range(-0.5f, 0.5f));

        Destroy(gameObject, 20f);
    }

    public void SetDestination(Vector2 destination)
    {
        _targetPosition = destination;

        Vector2 direction = (_targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(new Vector3(angle + 90, -90, -90));
    }

    private void Update()
    {
        if (_rotate)
        {
            transform.Rotate(_rotationAngle);
        }

        Move();
        HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera cam = Camera.main;
            if (cam != null)
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = cam.ScreenPointToRay(mousePos);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        DestroyObject();
                    }
                }
            }
        }
    }

    private void Move()
    {
        float multiplier = UnityEngine.Random.Range(0.8f, 1.21f);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * multiplier * Time.deltaTime);
        if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
        {
            DestroyObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        DestroyObject();
    }

    private void DestroyObject()
    {
        Ondestroyed?.Invoke();
        Destroy(gameObject);
    }
}
