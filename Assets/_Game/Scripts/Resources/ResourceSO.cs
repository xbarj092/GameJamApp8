using UnityEngine;

[CreateAssetMenu(fileName = "TrackPart", menuName = "RollerCoaster/Resource")]
public class ResourceSO : ScriptableObject
{
    public ResourceType ResourceType;
    public GameObject BreakVFX;
    public SoundType BreakSFX;
}
