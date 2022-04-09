using Godot;
using System;

namespace BehaviorTree.Base
{
    public class TestNode2 : Node, IBehaviorNode
    {   
        [Export]
        public States NodeState { get; set; }

        [Export]
        private int countLimit = 50000;
        private int count = 0;

        public void InitNode(in TreeController controller)
        {
            GD.Print("Init node!");
        }

        
        public States Tick(in TreeController controller)
        {
            GD.Print("Count: " + count.ToString());
            count++;

            if(count > countLimit){
                GD.Print("reached limit!");
                count = 0;
                controller.ExitNode(this, States.SUCCESS);
            }else{
                controller.ExitNode(this, States.RUNNING);
            }

            return this.NodeState;

        }
    }
}

