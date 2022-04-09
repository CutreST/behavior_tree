using Godot;
using System;

using System.Collections.Generic;

namespace BehaviorTree.Base
{
    /// <summary>
    /// The base class for all the composite node, it have a list of children and an byte index
    /// </summary>
    public abstract class CompositeNode : Node, IBehaviorNode
    {
        /// <summary>
        /// List of the children
        /// </summary>
        /// <value></value>
        public List<IBehaviorNode> Children { get; protected set; }

        public IBehaviorNode CurrentChild { get => Children[Index]; }
        public States NodeState { get; set; }

        /// <summary>
        /// The index
        /// </summary>
        protected byte Index;

        /// <summary>
        /// empty constructor
        /// </summary>
        public CompositeNode(){}

        /// <summary>
        /// Constructor withd a list of children
        /// </summary>
        /// <param name="childList"></param>
        public CompositeNode(in List<IBehaviorNode> childrenList)
        {
            Children = childrenList;
        }

        /// <summary>
        /// Adds a child without check
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(in IBehaviorNode child)
        {
            this.Children.Add(child);
        }

        /// <summary>
        /// Adds a unique child to avoid duplicates.
        /// OJU! Only checks the instance
        /// </summary>
        /// <param name="child"></param>
        public void AddUniqueChild(in IBehaviorNode child)
        {
            if (this.Children.Contains(child))
            {
                return;
            }

            Children.Add(child);
        }
        
        public void InitNode(in TreeController controller)
        {
            this.Children = new List<IBehaviorNode>();
            IBehaviorNode node;
            for (byte i = 0; i < base.GetChildCount(); i++)
            {
                if ((node = base.GetChild((int)i) as IBehaviorNode) != null)
                {
                    this.AddUniqueChild(base.GetChild((int)i) as IBehaviorNode);
                    node.InitNode(controller);
                    continue;
                }                
            }
        }

        public abstract States Tick(in TreeController controller);
    }
}
