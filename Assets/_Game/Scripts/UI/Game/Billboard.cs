using UnityEngine;

public class Billboard : MonoBehaviour
{
    [Header("Billboard Settings")]
    [SerializeField] private Camera _targetCamera;
    [SerializeField] private bool _lockY = true;
    [SerializeField] private bool _freezeX = false;
    [SerializeField] private bool _freezeZ = false;

    private void OnEnable()
    {
        FaceCamera();
    }

    private void FaceCamera()
    {
        Vector3 targetPosition = (_targetCamera != null ? _targetCamera : Camera.main).transform.position;
        Vector3 direction = -(targetPosition - transform.position);

        if (_lockY)
        {
            direction.y = 0;
        }
        if (_freezeX)
        {
            direction.x = 0;
        }
        if (_freezeZ)
        {
            direction.z = 0;
        }

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Vector3 euler = targetRotation.eulerAngles;
            euler.y = transform.rotation.eulerAngles.y;
            euler.z = GameManager.Instance.CurrentPlayerIndex == 0 ? 0 : 180;
            transform.rotation = Quaternion.Euler(euler);
        }
    }
}
