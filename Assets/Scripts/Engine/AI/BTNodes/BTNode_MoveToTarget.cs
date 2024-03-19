using System;
using AIModule;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_MoveToTarget : BTNode
    {
        public override string Name => "Move To Target";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort target;

        [SerializeField, BlackboardKey]
        private ushort stoppingDistance;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(character, out IAtomicObject movingCharacter))
                return BTState.FAILURE;
            if(!blackboard.TryGetObject(target, out IAtomicObject targetTree))
                return BTState.FAILURE;
            if (!blackboard.TryGetFloat(stoppingDistance, out float distance))
                return BTState.FAILURE;
            
            var transform = movingCharacter.Get<Transform>(ObjectAPI.Transform);
            var targetTransform = targetTree.Get<Transform>(ObjectAPI.Transform);
            var targetDirection = (targetTransform.position-transform.position);

            if (Vector3.SqrMagnitude(targetDirection) > distance * distance)
            {
                movingCharacter.InvokeAction(ObjectAPI.MoveStepRequest, targetDirection.normalized);
                return BTState.RUNNING;
            }
            
            return BTState.SUCCESS;
        }
    }
}