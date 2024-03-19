using System;
using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_FindResource : BTNode
    {
        public override string Name => "Find Resource";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort resourceService;

        [SerializeField, BlackboardKey]
        private ushort target;

        [SerializeField, BlackboardKey]
        private ushort resource;
        
        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(character, out IAtomicObject movingCharacter))
                return BTState.FAILURE;
            if(!blackboard.TryGetObject(resourceService, out ResourceService resourceServiceValue))
                return BTState.FAILURE;
            
            var transform = movingCharacter.Get<Transform>(ObjectAPI.Transform);
            if (resourceServiceValue.FindClosestResource(transform.position, out IAtomicObject tree))
            {
                blackboard.SetObject(target,tree);
                blackboard.SetObject(resource,tree);
                return BTState.SUCCESS;
            }
            return BTState.FAILURE;
        }
    }
}