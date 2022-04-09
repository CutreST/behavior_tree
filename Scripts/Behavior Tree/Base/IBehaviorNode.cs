using Godot;
using System;

namespace BehaviorTree.Base
{
    /// <summary>
    /// The possible states of a node 
    /// </summary>
    public enum States : byte { FAILURE, SUCCESS, RUNNING, INOPERATIVE }
    
    /// <summary>
    /// Interface for all the behaviour nodes.
    /// <remarks>
    /// So, in early implementations this was a simple Abstract Class, but, as
    /// we will use the Godot Nodes and in order to expand this at somewhat other
    /// engines I thought that maybe an interface was better, so now every
    /// godot node can be a behaviour node without to much hassle.
    /// For example, if we want to reproduce a sound and don't have a sound
    /// system working, we can use a <see cref="AudioStreamPlayer2D"/>, implement
    /// the interface and on tick play the sound. 
    /// The example is not the most perfomant example, but, not only it works but 
    /// it allows to test and run the tree without worrying about other custom systems
    /// </remarks>
    /// </summary>
    public interface IBehaviorNode
    {
        /// <summary>
        /// The current state of the node
        /// </summary>
        States NodeState { get; set; }

        /// <summary>
        /// Called when entered this node
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>The result of the node entered</returns>
        States Tick(in TreeController controller);
        

        /// <summary>
        /// Called to init the node, when the tree is created
        /// </summary>
        /// <param name="controller"></param>
        void InitNode(in TreeController controller);       

    }
}
