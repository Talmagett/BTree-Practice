using System;
using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_UnloadResources : BTNode
    {
        public override string Name => "Unload Resources";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort targetStorage;
        
        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(character, out IAtomicObject aiCharacter))
                return BTState.FAILURE;
            if(!blackboard.TryGetObject(targetStorage, out IAtomicObject barn))
                return BTState.FAILURE;

            var aiCharacterResourceStorage = aiCharacter.Get<ResourceStorage>(ObjectAPI.ResourceStorage);
            var barnResourceStorage = barn.Get<ResourceStorage>(ObjectAPI.ResourceStorage);
            if (barnResourceStorage.IsFull())
                return BTState.FAILURE;
            
            var minPuttingResources = Mathf.Min(barnResourceStorage.FreeSlots, aiCharacterResourceStorage.Current);
            barnResourceStorage.PutResources(minPuttingResources);
            aiCharacterResourceStorage.ExtractResources(minPuttingResources);
            
            return BTState.SUCCESS;
        }
    }
}