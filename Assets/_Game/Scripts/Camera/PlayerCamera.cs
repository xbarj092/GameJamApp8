using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) movement.z += 1f;
        if (Input.GetKey(KeyCode.S)) movement.z -= 1f;
        if (Input.GetKey(KeyCode.A)) movement.y += 1f;
        if (Input.GetKey(KeyCode.D)) movement.y -= 1f;

        transform.Translate(_moveSpeed * Time.deltaTime * movement);
    }
}
