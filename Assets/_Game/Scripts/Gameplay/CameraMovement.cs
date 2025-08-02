using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(GameManager.Instance.MovementSpeed * Time.deltaTime * Vector3.forward, Space.World);
        Debug.Log(GameManager.Instance.MovementSpeed);
    }
}
