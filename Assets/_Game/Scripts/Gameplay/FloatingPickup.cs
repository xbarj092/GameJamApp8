using DG.Tweening;
using UnityEngine;

public class FloatingPickup : MonoBehaviour
{

    public float rotationSpeed = 50f;
    public float floatAmplitude = 0.6f;
    public float floatDuration = 1f;

    private void Start()
    {
        transform
            .DOMoveY(floatAmplitude, floatDuration)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}