using DG.Tweening;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("GridSizeSetup")]
    [SerializeField] private int _levelGridCellSize;
    [SerializeField] private int _levelGridWidth;
    [SerializeField] private int _levelGridHeight;

    [Header("GridVisualization")]
    [SerializeField] private Transform _gridVisualizationTransform;
    [SerializeField] private MeshRenderer _gridVisualizationRenderer;
    [Space(5)]
    [SerializeField] private float _gridHideTime;
    [SerializeField] private float _hiddenGridAlpha;
    [Space(5)]
    [SerializeField] private float _gridShowTime;
    [SerializeField] private float _shownGridAlpha;

    private Grid<GridNode> _grid;

    private bool _isMouseActive = true;

    private const string GRID_VISUALIZATION_SHADER_PROPERTY_ALPHA = "_Alpha";
    private const string GRID_VISUALIZATION_SHADER_PROPERTY_TILING = "_Tiling";

    private void Start()
    {
        SetupGrid();
    }

    private void Update()
    {
        if (_isMouseActive)
        {
            HideGridVisualization();
            _isMouseActive = false;
        }
    }

    private void ShowGridVisualization() => UpdateGridVisual(_shownGridAlpha, _gridShowTime);
    private void HideGridVisualization() => UpdateGridVisual(_hiddenGridAlpha, _gridHideTime);

    private void UpdateGridVisual(float alpha, float changeTime)
    {
        DOTween.To(() => _gridVisualizationRenderer.material.GetFloat(GRID_VISUALIZATION_SHADER_PROPERTY_ALPHA),
                   x => _gridVisualizationRenderer.material.SetFloat(GRID_VISUALIZATION_SHADER_PROPERTY_ALPHA, x),
                   alpha, changeTime);
    }

    private void SetupGrid()
    {
        _grid = new Grid<GridNode>(_levelGridWidth, _levelGridHeight, _levelGridCellSize, (g, x, y) => new GridNode(g, x, y));

        _gridVisualizationRenderer.material.SetVector(GRID_VISUALIZATION_SHADER_PROPERTY_TILING, new Vector2(_levelGridWidth, _levelGridHeight));

        float defaultPlaneSize = 10f;
        _gridVisualizationTransform.localScale = new((float)(_levelGridWidth / defaultPlaneSize), 1f, (float)(_levelGridHeight / defaultPlaneSize));
        _gridVisualizationTransform.position = new Vector3((float)(_levelGridWidth / 2f), (float)(_levelGridHeight / 2f), 0);

        ShowGridVisualization();
    }
}
