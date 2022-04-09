using Godot;
using System;

namespace BehaviorTree.Base
{
    public class TestNode : Node, IBehaviorNode
    {   
        [Export]
        public States NodeState { get; set; }

        public void InitNode(in TreeController controller)
        {
            GD.Print("Init node!");
        }

        
        public States Tick(in TreeController controller)
        {
            GD.Print("State Of the node: " + this.NodeState.ToString());
            return this.NodeState;
        }
    }
}

