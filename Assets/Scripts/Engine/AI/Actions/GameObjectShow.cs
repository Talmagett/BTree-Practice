using System;
using AIModule;
using Game.Engine;
using UnityEngine;

namespace Engine.AI.Actions
{
    [CreateAssetMenu(menuName = "SO/Create GameObjectShow", fileName = "GameObjectShow", order = 0)]
    public class GameObjectShow : AIAction
    {
        [SerializeField, BlackboardKey]
        private ushort targetToShow;
        
        public override void Perform(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(targetToShow, out GameObject target))
                throw new NullReferenceException("Set target key");
            target.SetActive(true);
        }
    }
}