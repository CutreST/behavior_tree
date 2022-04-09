
using Godot;
using System;
using System.Collections.Generic;

//using Entities.BehaviourTree.Loaders;

namespace BehaviorTree.Base
{
    /// <summary>
    /// The behaviour tree controller
    /// <remarks>
    /// There's work to do, i think. But it works for what we need.
    /// </remarks>
    /// </summary>
    public class TreeController : Node
    {
        /// <summary>
        /// The root node.
        /// </summary>
        private IBehaviorNode _root;

        public IBehaviorNode Root{
            get => _root;
            set{
                if(_root != null){
                    // cambiar esto, ya que se supone que vamos a interfaz y no hereda de node normal      
                    Node godotNode = _root as Node;
                    if(godotNode != null){
                        godotNode.QueueFree();
                    }else{
                        GD.PrintErr("Tree Controller Says: the root is not a Godot node. Why?");
                    }
                    
                }
                _root = value;
                this.InitRoot();
                GD.Print("New root on Behaviour Tree");
            }
        }

        /// <summary>
        /// The current node. At some points this is going to be a stack, so, we 
        /// can keep track of the running onoes
        /// </summary>
        private IBehaviorNode _currentNode;

        private Stack<IBehaviorNode> _nodeStack;

        #region Godot MEthods
        public override void _Ready()
        {
            // no root, no node, no error this way
            if(this.TrySetRoot() == false){
                base.SetPhysicsProcess(false);
                GD.Print("Tree Controller says: There's no root node here");
            }
        }

        public override void _ExitTree()
        {
            
        }
        #endregion

        /// <summary>
        /// Sets the root if it exists as a child of the controller and puts it to <see cref="TreeController._currentNode"
        /// OJU! The root has to be the first child (index = 0)!
        /// </summary>
        /// <returns>got the root?</returns>
        private bool TrySetRoot()
        {
            _root = base.GetChild(0) as IBehaviorNode;
            if(_root == null){
                return false;
            }

            this.InitRoot();
            return true;
        }

        private void InitRoot(){
            _root.InitNode(this);
            EnterNode(_root);
            _currentNode = _root;
        }

        public void EnterNode(in IBehaviorNode node)
        {
            //_currentNode = node;                      
        }

        Queue<IBehaviorNode> Q;

        public void ExitNode(in IBehaviorNode node, in States state){
            node.NodeState = state;

            if(node.NodeState == States.RUNNING){
                _currentNode = node;
            }
        }

        public override void _PhysicsProcess(float delta)
        {
            _currentNode.Tick(this);
        }
 
    }
}
