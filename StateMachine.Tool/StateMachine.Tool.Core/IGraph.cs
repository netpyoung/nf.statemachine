namespace StateMachine.Tool.Core
{
    public interface IGraph : IGraphElement
    {
        INode InitialNode { get; }
        INode[] Nodes { get; }
        ITransition[] Transitions { get; }
    }
}