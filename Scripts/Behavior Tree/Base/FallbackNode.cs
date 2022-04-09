using Godot;
using System;
using System.Diagnostics;


namespace BehaviorTree.Base
{
    /// <summary>
    /// Fallback or selector node. Returns success if one child success or,
    /// in other words, looks for every children until there're no more or
    /// one returns success
    /// </summary>
    public class FallbackNode : CompositeNode
    {
        public FallbackNode() : base(new System.Collections.Generic.List<IBehaviorNode>())
        {

        }

        public override States Tick(in TreeController controller)
        {
            Debug.Assert((Children != null), "The children are null");
            Debug.Assert(Children.Count > 0, "There must be, at least, one child at " + this.GetType().Name);
            //controller.EnterNode(this);
            GD.Print("Entered a Fallback Node!");
            
            for (byte i = Index; i < Children.Count; i++)
            {
                controller.EnterNode(Children[i]);
                switch (Children[i].Tick(controller))
                {
                    case States.SUCCESS:
                        Index = 0;
                        controller.ExitNode(this, States.SUCCESS);
                        return this.NodeState;
                    case States.RUNNING:
                        Index = ++i;
                        controller.ExitNode(this, States.RUNNING);
                        return this.NodeState;
                }

            }

            Index = 0;
            controller.ExitNode(this, States.FAILURE);
            return this.NodeState;
        }
    }
}
