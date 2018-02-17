using System.Xml.Linq;
using StateMachine.Tool.Core;

namespace StateMachine.Tool.Adapter.yWorks
{
    internal class TransitionStub : ITransition
    {
        public TransitionStub(string semantic, INode source, INode target)
        {
            Semantic = semantic;
            Source = source;
            Target = target;
        }

        public string Semantic { get; }
        public INode Source { get; }
        public INode Target { get; }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", Source.Semantic, Target.Semantic);
        }
    }

    public class Transition : GraphObject
    {
        public string Source { get; private set; }
        public string Target { get; private set; }

        public override void Load(XElement element, KeyMapping keyMapping)
        {
            base.Load(element, keyMapping);

            CheckIdProperty(element);

            Source = (string) element.Attribute("source");
            Target = (string) element.Attribute("target");

            if (Source != null)
                Source = Source.Trim();

            if (Target != null)
                Target = Target.Trim();
        }

        internal void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }

        public static Transition Create(XElement element, KeyMapping keyMapping)
        {
            var transition = new Transition();
            transition.Load(element, keyMapping);
            return transition;
        }

        public override string ToString()
        {
            return base.ToString() + " { " + string.Format("{0} -> {1}", Source, Target) + " }";
        }
    }
}