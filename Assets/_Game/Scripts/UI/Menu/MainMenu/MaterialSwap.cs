using UnityEngine;

public class MaterialSwap : MonoBehaviour
{
    [SerializeField] private Material _playerOneMat;
    [SerializeField] private Material _playerTwoMat;

    [SerializeField] private MeshRenderer _renderer;

    private bool _playerOne = true;

    public void SwapMaterial()
    {
        _renderer.material = _playerOne ? _playerTwoMat : _playerOneMat;
        _playerOne = false;
    }
}
