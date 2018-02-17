namespace StateMachine.Tool.Core
{
    public interface INode : IGraphElement
    {
        bool IsFinal { get; }
    }
}