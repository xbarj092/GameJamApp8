using UnityEngine;

public class ColorSwap : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private Color[] backgroundColors;
    private bool _upperColor = true;

    private void Awake() => _camera = GetComponent<Camera>();
    private void OnEnable() => GameManager.Instance.OnPlayersSwapped += SwapCameraColor;
    private void OnDisable() => GameManager.Instance.OnPlayersSwapped -= SwapCameraColor;
    public void SwapCameraColor()
    {
        _camera.backgroundColor = _upperColor ? backgroundColors[1] : backgroundColors[0];
        _upperColor = !_upperColor;
    }
}
