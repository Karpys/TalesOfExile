[System.Serializable]
public class ZoneSelection
{
    public ZoneOrigin Origin = ZoneOrigin.Self;
    public ZoneType DisplayType = ZoneType.Square;
    public int Range = 0;
    public ValidationType ValidationType = null;
    public bool ActionSelection = false;

    public ZoneSelection(ZoneType type, int range)
    {
        DisplayType = type;
        Range = range;
    }
}