namespace StateMachine.Tool.Core
{
    public interface ITransition : IGraphElement
    {
        INode Source { get; }
        INode Target { get; }
    }
}