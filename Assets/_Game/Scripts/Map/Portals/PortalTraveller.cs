using System.Collections;
using UnityEngine;

public class PortalTraveler : MonoBehaviour
{
    private Portal _currentPortal;
    private bool _hasClone = false;
    private GameObject _clone;
    private Vector3 _previousPosition;

    private void Start()
    {
        _previousPosition = transform.position;
    }

    private void Update()
    {
        if (_currentPortal != null)
        {
            UpdateClone();
            CheckForTeleport();
        }
        _previousPosition = transform.position;
    }

    public void SetPortal(Portal portal)
    {
        _currentPortal = portal;
        CreateClone();
    }

    public void ExitPortal(Portal portal)
    {
        if (_currentPortal == portal)
        {
            _currentPortal = null;
            DestroyClone();
        }
    }

    private void CreateClone()
    {
        if (_hasClone || _currentPortal?.LinkedPortal == null) return;

        _clone = new GameObject("PortalClone_" + gameObject.name);

        SpriteRenderer originalRenderer = GetComponent<SpriteRenderer>();
        if (originalRenderer != null)
        {
            SpriteRenderer cloneRenderer = _clone.AddComponent<SpriteRenderer>();
            cloneRenderer.sprite = originalRenderer.sprite;
            cloneRenderer.color = originalRenderer.color;
            cloneRenderer.sortingLayerName = originalRenderer.sortingLayerName;
            cloneRenderer.sortingOrder = originalRenderer.sortingOrder;
        }

        _hasClone = true;
        UpdateClone();
    }

    private void UpdateClone()
    {
        if (!_hasClone || _currentPortal?.LinkedPortal == null) return;

        Vector3 clonePosition = _currentPortal.TransformPosition(transform.position);
        _clone.transform.position = clonePosition;

        float rotationDiff = _currentPortal.LinkedPortal.transform.eulerAngles.z - _currentPortal.transform.eulerAngles.z;
        _clone.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rotationDiff);
        _clone.transform.localScale = transform.localScale;
    }

    private void CheckForTeleport()
    {
        if (_currentPortal?.LinkedPortal == null) return;

        Vector3 portalToObject = transform.position - _currentPortal.transform.position;
        Vector3 portalToPrevious = _previousPosition - _currentPortal.transform.position;
        Vector3 portalNormalY = _currentPortal.transform.up;
        Vector3 portalNormalX = _currentPortal.transform.right;

        float currentSideX = Vector3.Dot(portalToObject, portalNormalX);
        float previousSideX = Vector3.Dot(portalToPrevious, portalNormalX);

        float currentSideY = Vector3.Dot(portalToObject, portalNormalY);
        float previousSideY = Vector3.Dot(portalToPrevious, portalNormalY);

        if ((previousSideX >= 0 && currentSideX < 0) || (previousSideX <= 0 && currentSideX > 0) ||
            (previousSideY >= 0 && currentSideY < 0) || (previousSideY <= 0 && currentSideY > 0))
        {
            Teleport();
        }
    }

    private void Teleport()
    {
        if (_currentPortal?.LinkedPortal == null) return;

        Vector3 newPosition = _currentPortal.TransformPosition(transform.position);
        transform.position = newPosition;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 newVelocity = _currentPortal.TransformDirection(rb.linearVelocity);
            rb.linearVelocity = newVelocity;
        }

        float rotationDiff = _currentPortal.LinkedPortal.transform.eulerAngles.z - _currentPortal.transform.eulerAngles.z;
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rotationDiff);

        Portal newPortal = _currentPortal.LinkedPortal;
        _currentPortal = null;
        DestroyClone();

        StartCoroutine(DelayedPortalSet(newPortal));
    }

    private IEnumerator DelayedPortalSet(Portal portal)
    {
        yield return new WaitForFixedUpdate();
        SetPortal(portal);
    }

    private void DestroyClone()
    {
        if (_hasClone && _clone != null)
        {
            Destroy(_clone);
            _hasClone = false;
        }
    }

    private void OnDestroy()
    {
        DestroyClone();
    }
}
