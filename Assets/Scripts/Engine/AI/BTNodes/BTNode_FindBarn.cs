using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    public class BTNode_FindBarn: BTNode
    {
        public override string Name => "Find Barn";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort barn;

        [SerializeField, BlackboardKey]
        private ushort target;
        
        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(character, out IAtomicObject movingCharacter))
                return BTState.FAILURE;
            if(!blackboard.TryGetObject(barn, out IAtomicObject barnValue))
                return BTState.FAILURE;
            
            blackboard.SetObject(target,barnValue);
            return BTState.SUCCESS;
        }
    }
}