[System.Serializable]
public class ZoneSelection
{
    public ZoneOrigin Origin = ZoneOrigin.Self;
    public Zone Zone = null;
    public ValidationType ValidationType = null;
    public bool ActionSelection = false;

    
}

[System.Serializable]
public class Zone
{
    public ZoneType DisplayType = ZoneType.NONE;
    public int Range = 0;
    
    public Zone(ZoneType type, int range)
    {
        DisplayType = type;
        Range = range;
    }
}