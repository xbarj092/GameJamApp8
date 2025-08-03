using UnityEngine;

public class MenuObject : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Vector2 _targetPosition;

    private void Start()
    {
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
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void Move()
    {
        float multiplier = Random.Range(0.8f, 1.21f);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * multiplier * Time.deltaTime);
        if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        Destroy(gameObject);
    }
}
