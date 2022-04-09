using Godot;
using System;

namespace BehaviorTree.Base
{
    /// <summary>
    /// Base class for all the decorators. Only have one child.
    /// Decorators are meant to modify the return value of the child. 
    /// For example, returning <see cref="States.FAILURE"/> when the
    /// child truly returns <see cref="States.RUNNING"/>.
    /// </summary>
    public abstract class DecoratorNode : Node, IBehaviorNode
    {
        /// <summary>
        /// The child of this node
        /// </summary>
        /// <value></value>
        public IBehaviorNode Child { get; set; }
        
        public States NodeState { get; set; }

        public void InitNode(in TreeController controller)
        {
            if(base.GetChildCount() == 0){
                return;
            }
            
            IBehaviorNode node = base.GetChild(0) as IBehaviorNode;
            node.InitNode(controller);
            Child = node;
        }

        public abstract States Tick(in TreeController controller);
    }
}
