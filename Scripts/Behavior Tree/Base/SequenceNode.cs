using Godot;
using System;
using System.Diagnostics;

namespace BehaviorTree.Base
{
    /// <summary>
    /// A sequence node. Looks foreach child and returns <see cref="States.FAILURE"/> if one 
    /// of the child fails. So, in other words, we execute each node until there are 
    /// no more children or one of them returns <see cref="States.FAILURE"/>
    /// </summary>
    public class SequenceNode : CompositeNode
    {
        public SequenceNode() : base(new System.Collections.Generic.List<IBehaviorNode>())
        {

        }
        public override States Tick(in TreeController controller)
        {

            Debug.Assert((Children != null || Children.Count > 0), "There must be, at least, one child");

            for (byte i = Index; i < Children.Count; i++)
            {
                controller.EnterNode(Children[i]);
                switch (Children[i].Tick(controller))
                {
                    case States.FAILURE:
                        Index = 0;
                        //return base.ChangeNodeStatus(controller, States.FAILURE);
                        controller.ExitNode(this, States.FAILURE);
                        return this.NodeState;
                        
                    case States.RUNNING:
                        Index = i;
                        //return base.ChangeNodeStatus(controller, States.RUNNING);
                        controller.ExitNode(this, States.RUNNING);
                        return this.NodeState;
                        
                }                
            }

            //controller.ExitNode(Children[i]);
            Index = 0;
            return this.NodeState = States.SUCCESS;            
        }      
    }
}
