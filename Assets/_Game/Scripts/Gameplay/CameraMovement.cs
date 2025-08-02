using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _playerOneRotation;
    [SerializeField] private Vector3 _playerTwoRotation;

    [SerializeField] private Vector3 _playerOnePosition;
    [SerializeField] private Vector3 _playerTwoPosition;

    private void OnEnable()
    {
        GameManager.Instance.OnPlayersSwapped += SwapPlayers;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayersSwapped -= SwapPlayers;
    }

    private void LateUpdate()
    {
        transform.Translate(GameManager.Instance.MovementSpeed * Time.deltaTime * Vector3.forward, Space.World);
        Debug.Log(GameManager.Instance.MovementSpeed);
    }

    private void SwapPlayers()
    {
        transform.DORotate(GameManager.Instance.CurrentPlayerIndex == 0 ? _playerTwoRotation : _playerOneRotation, 0.5f).SetEase(Ease.OutBack);
        transform.DOMoveX(GameManager.Instance.CurrentPlayerIndex == 0 ? _playerTwoPosition.x : _playerOnePosition.x, 0.5f).SetEase(Ease.OutBack);
        transform.DOMoveY(GameManager.Instance.CurrentPlayerIndex == 0 ? _playerTwoPosition.y : _playerOnePosition.y, 0.5f).SetEase(Ease.OutBack);
    }
}
