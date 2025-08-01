using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _visual;
    [SerializeField] private float _rotationDuration;
    [SerializeField] private Vector3 _rotationAngleDefault;
    [SerializeField] private Vector3 _rotationAngleLeft;
    [SerializeField] private Vector3 _rotationAngleRight;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private KeyCode _leftKey;
    [SerializeField] private KeyCode _rightKey;

    private float[] _positions = { -3.75f, -1.25f, 1.25f };
    public int CurrentLine = 1;

    private bool _isMoving = false;

    private void Update()
    {
        transform.Translate(GameManager.Instance.MovementSpeed * Time.deltaTime * Vector3.forward);

        if (_isMoving)
        {
            return;
        }

        if (Input.GetKeyDown(_leftKey))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(_rightKey))
        {
            MoveRight();
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new(_positions[CurrentLine], transform.position.y, transform.position.z);
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
        }
        else
        {
            if (_isMoving)
            {
                _visual.transform.DORotate(_rotationAngleDefault, _rotationDuration).SetEase(Ease.OutBack);
            }

            _isMoving = false;
        }
    }

    private void MoveLeft()
    {
        if (CurrentLine > 0)
        {
            _visual.transform.DORotate(_rotationAngleLeft, _rotationDuration).SetEase(Ease.OutBack);

            MoveToLine(CurrentLine - 1);
        }
    }

    private void MoveRight()
    {
        if (CurrentLine < _positions.Length - 1)
        {
            _visual.transform.DORotate(_rotationAngleRight, _rotationDuration).SetEase(Ease.OutBack);

            MoveToLine(CurrentLine + 1);
        }
    }

    public void MoveToLine(int lineIndex)
    {
        if (lineIndex >= 0 && lineIndex < _positions.Length)
        {
            CurrentLine = lineIndex;
            _isMoving = true;
        }
    }

    public void Death()
    {
        Time.timeScale = 0;
        StartCoroutine(LerpProgress());

        Destroy(gameObject, 1.5f);
    }

    private IEnumerator LerpProgress()
    {
        /*float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float progress = Mathf.Lerp(0.4f, 1, elapsedTime / duration);
            _renderer.material.SetFloat("_Progress", progress);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        _renderer.material.SetFloat("_Progress", 1);*/
        yield return null;
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Result);
    }
}
