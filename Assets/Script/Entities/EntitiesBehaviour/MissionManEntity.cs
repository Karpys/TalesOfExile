using UnityEngine;

namespace KarpysDev.Script.Entities.EntitiesBehaviour
{
    public class MissionManEntity : PnjEntity
    {
        protected override void OnPlayerAroundEntity()
        {
            Debug.Log("Display Mission UI");
        }
    }
}