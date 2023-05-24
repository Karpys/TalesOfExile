namespace KarpysDev.Script.Map_Related.MapGeneration
{
    public abstract class Room
    {
        protected Map m_Map = null;

        public Map Map => m_Map;
        public Room(Map map)
        {
            m_Map = map;
        }

        public abstract void Generate();
    }
}