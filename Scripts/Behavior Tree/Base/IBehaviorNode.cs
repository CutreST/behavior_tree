using Godot;
using System;

namespace BehaviorTree.Base
{
    /// <summary>
    /// The possible states of a node 
    /// </summary>
    public enum States : byte { FAILURE, SUCCESS, RUNNING, INOPERATIVE }
    
    public interface IBehaviorNode
    {
        /// <summary>
        /// The state of the node
        /// </summary>
        States NodeState { get; set; }

        /// <summary>
        /// Called when entered this node
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>The result of the node entered</returns>
        States Tick(in TreeController controller);
        

        /*
        /// Ideally, this method is called before any tick operation. For example, if we need to
        /// create some instances, check for data, etc. we use this method. In case we don't need 
        /// preparations, then there's no need to call and, because of this, <see cref="Tick(in TreeController)"/>
        /// is abstract and not virtual
        /// </summary>
        /// <param name="controller"></param>
        void OnEnter(in TreeController controller);*/

        /// <summary>
        /// Called to init the node, when the tree is created
        /// </summary>
        /// <param name="controller"></param>
        void InitNode(in TreeController controller);

        // esto irá en el controler ahora, creo...
        /*public virtual States ChangeNodeStatus(in TreeController controller, in States state){
            this.NodeState = state;
            controller.ExitNode(this);
            /*
            if(this.NodeState == States.FAILURE || this.NodeState == States.SUCCESS){
                controller.ExitNode(this);
            }
            return this.NodeState;
        }*/

        

    }
}
