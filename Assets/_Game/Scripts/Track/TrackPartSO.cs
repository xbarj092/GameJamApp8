using UnityEngine;

[CreateAssetMenu(fileName = "TrackPart", menuName = "RollerCoaster/TrackPart")]
public class TrackPartSO : ScriptableObject
{
    public Vector2Int Size = Vector2Int.one;
    public TrackConnection EntryPoint;
    public TrackConnection ExitPoint;
    public TrackType TrackType;
    public float SpeedMultiplier = 1f;
    public bool RequiresSupport;
    public int RequiredWood;
    public int RequiredIron;
    public int RequiredPowerSupply;
}
