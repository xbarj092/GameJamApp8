using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    [SerializeField] private LayerMask _portalMask = -1;
    [SerializeField] private int _renderTextureSize = 512;

    public Portal LinkedPortal;

    [Header("Components")]
    [SerializeField] private SpriteRenderer _portalRenderer;

    private Camera _portalCamera;
    private RenderTexture _renderTexture;

    private void Start()
    {
        SetupPortal();
    }

    private void LateUpdate()
    {
        if (LinkedPortal != null && _portalCamera != null)
        {
            UpdatePortalCamera();
        }
    }

    private void OnDestroy()
    {
        if (_renderTexture != null)
        {
            _renderTexture.Release();
        }
    }

    private void SetupPortal()
    {
        _renderTexture = new RenderTexture(_renderTextureSize, _renderTextureSize, 16);
        _renderTexture.Create();

        Material portalMaterial = new Material(Shader.Find("Sprites/Default"));
        portalMaterial.mainTexture = _renderTexture;
        _portalRenderer.material = portalMaterial;

        GameObject cameraObj = new GameObject("PortalCamera_" + gameObject.name);
        cameraObj.transform.SetParent(transform);
        _portalCamera = cameraObj.AddComponent<Camera>();

        _portalCamera.targetTexture = _renderTexture;
        _portalCamera.orthographic = true;
        _portalCamera.cullingMask = _portalMask;
        _portalCamera.clearFlags = CameraClearFlags.SolidColor;
        _portalCamera.backgroundColor = Color.black;

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            _portalCamera.orthographicSize = mainCamera.orthographicSize;
        }
    }

    private void UpdatePortalCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        Vector3 offsetFromPortal = mainCamera.transform.position - transform.position;

        Vector3 portalCameraPosition = LinkedPortal.transform.position + offsetFromPortal;
        _portalCamera.transform.position = portalCameraPosition;

        _portalCamera.transform.rotation = mainCamera.transform.rotation;

        float rotationDifference = LinkedPortal.transform.eulerAngles.z - transform.eulerAngles.z;
        _portalCamera.transform.Rotate(0, 0, rotationDifference);
    }

    public Vector3 TransformPosition(Vector3 position)
    {
        if (LinkedPortal == null) return position;

        Vector3 relativePos = position - transform.position;

        float rotationDiff = LinkedPortal.transform.eulerAngles.z - transform.eulerAngles.z;
        relativePos = Quaternion.Euler(0, 0, rotationDiff) * relativePos;

        return LinkedPortal.transform.position + relativePos;
    }

    public Vector3 TransformDirection(Vector3 direction)
    {
        if (LinkedPortal == null) return direction;

        float rotationDiff = LinkedPortal.transform.eulerAngles.z - transform.eulerAngles.z;
        return Quaternion.Euler(0, 0, rotationDiff) * direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PortalTraveler traveler))
        {
            traveler.SetPortal(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PortalTraveler traveler))
        {
            traveler.ExitPortal(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider2D>().bounds.size);

        if (LinkedPortal != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, LinkedPortal.transform.position);
        }
    }
}
