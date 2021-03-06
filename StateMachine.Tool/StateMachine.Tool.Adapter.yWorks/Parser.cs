﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using StateMachine.Tool.Core;

namespace StateMachine.Tool.Adapter.yWorks
{
    public class Parser : IParser
    {
        public bool TryParse(Stream stream, out IGraph graph)
        {
            try
            {
                graph = Parse(stream);
                return true;
            }
            catch (Exception ex)
            {
                graph = null;
                return false;
            }
        }

        public bool TryParse(string fpath, out IGraph graph)
        {
            try
            {
                graph = Parse(fpath);
                return true;
            }
            catch (Exception ex)
            {
                graph = null;
                return false;
            }
        }

        public IGraph Parse(string fpath)
        {
            using (var stream = new FileStream(fpath, FileMode.Open, FileAccess.Read))
            {
                return Parse(stream);
            }
        }

        public IGraph Parse(Stream stream)
        {
            var graph = Graph.CreateFromRootXml(XElement.Load(stream, LoadOptions.SetLineInfo));

            CheckNodesMergeability(graph.Nodes);

            var nodes = graph.Nodes
                .Select(n => (INode) new NodeStub(n.Description, n.IsFinal))
                .Distinct(new NodeEqualityComparer())
                .ToArray();

            var i = 0;
            var transitions = new ITransition[graph.Transitions.Length];

            foreach (var tr in graph.Transitions)
            {
                var sourceState = graph.Nodes.Single(n => n.Identifier == tr.Source);
                var targetState = graph.Nodes.Single(n => n.Identifier == tr.Target);

                var sourceNode = nodes.Single(n => n.Semantic == sourceState.Description);
                var targetNode = nodes.Single(n => n.Semantic == targetState.Description);

                transitions[i++] = new TransitionStub(tr.Description, sourceNode, targetNode);
            }

            INode initialNode = null;
            if (graph.InitialNode != null)
            {
                var initialNodeIndex = Array.FindIndex(graph.Nodes, n => n.Identifier == graph.InitialNode.Identifier);
                initialNode = nodes[initialNodeIndex];
            }

            return new GraphStub(initialNode, graph.Description, nodes, transitions);
        }

        private void CheckNodesMergeability(Node[] nodes)
        {
            foreach (var group in nodes.GroupBy(n => n.Description))
            {
                CheckPropertyMergeability(Constants.IsInitialStatePropertyName, n => n.IsInitial, group);
                CheckPropertyMergeability(Constants.IsFinalStatePropertyName, n => n.IsFinal, group);
            }
        }

        private void CheckPropertyMergeability<T>(string propertyName, Func<Node, T> getter,
            IGrouping<string, Node> group)
        {
            var value = getter(group.First());
            if (group.Any(n => EqualityComparer<T>.Default.Equals(getter(n), value) == false))
                throw new FormatException(string.Format(
                    "Inconsistency in partial state '{0}'. Property '{1}' does not match on all states.", group.Key,
                    propertyName));
        }
    }
}