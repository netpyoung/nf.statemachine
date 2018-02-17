using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using CommandLine;
using DotLiquid;
using StateMachine.Tool.Core;

namespace StateMachine.Tool.CLI
{
    public class StateInfo : Drop
    {
        public string Name { get; set; }
        public string RelativePath { get; set; }
    }

    public class TransitionInfo : Drop
    {
        public string Name { get; set; }
    }

    public class Node : Drop
    {
        private readonly INode node = null;
        public string Name
        {
            get { return this.node.Semantic; }
        }

        public Node(INode node)
        {
            this.node = node;
        }
    }
    
    public class Graph : Drop
    {
        private readonly IGraph graph = null;
        public string Name
        {
            get { return graph.Semantic; }
        }

        public Node[] States { get; }
        public string[] Tokens { get; }

        public Graph(IGraph graph)
        {
            this.graph = graph;
            this.States = graph.Nodes.Select(x => new Node(x)).ToArray();
            this.Tokens = graph.Transitions.Select(x => x.Semantic).Distinct().ToArray();
        }
    }
    
    class Program
    {
        class Options
        {
            [Option('i', "input", Required = true, HelpText = "Input file")]
            public string InputFpath { get; set; }
            [Option('t', "template", Required = true, HelpText = "Template directory")]
            public string TemplateDir { get; set; }
            [Option('o', "output", Required = true, HelpText = "Output Directory")]
            public string OutputDir { get; set; }
        }

        static void Main(string[] args)
        {
            var program  = new Program();
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => program.RunOptionsAndReturnExitCode(opts))
                .WithNotParsed<Options>((errs) => program.HandleParseError(errs));
        }

        private void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var err in errs)
            {
                Console.Error.WriteLine(err);
            }
        }

        private void RunOptionsAndReturnExitCode(Options opts)
        {
            Console.WriteLine(opts.InputFpath);
            
            //string fpath = "/Users/pyoung/temp/bitcraft_unity_fsm_exercise/uml/state_color.graphml";
            //string graphFpath = "/Users/pyoung/temp/finite-state-machine/Tools/SampleFiles/almost_real_state_machine.graphml";
            //string templateDir = "/Users/pyoung/RiderProjects/nf.statemachine/template";
            
            //string outputDir = "/Users/pyoung/temp/output";
            //string outputDir = "/Users/pyoung/RiderProjects/nf.statemachine/StateMachine/Sample01/output";
            
            string inputFpath = opts.InputFpath;
            string templateDir = opts.TemplateDir;
            string outputDir = opts.OutputDir;
            
            // File.Exists(inputFpath)
            // Directory.Exists(templateDir);
            // Directory.Exists(outputDir);
            
            IGraph graph = null;
            IParser parser = new Adapter.yWorks.Parser();
            bool isParsed = parser.TryParse(inputFpath, out graph);

            if (!isParsed)
            {
                return;
            }
            
            string stateMachineName = graph.Semantic;
            // string graphInitialNodeName = graph.InitialNode?.Semantic;
            // INode initialNode = graph.InitialNode;
            

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            
            // StateMachineCodeGenerator
            {
                string liquidStateMachine = Path.Combine(templateDir, "statemachine.liquid");
                Template templateStateMachine = Template.Parse(File.ReadAllText(liquidStateMachine));
                string result = templateStateMachine.Render(Hash.FromAnonymousObject(new { a = new Graph(graph) }));
                string outputFpath = Path.Combine(outputDir, $"{stateMachineName}StateMachine.autogen.cs");
                File.WriteAllText(outputFpath, result);
            }
            
            // StateTokensCodeGenerator
            {
                string liquidStateToken = Path.Combine(templateDir, "statetoken.liquid");
                Template templateStateToken = Template.Parse(File.ReadAllText(liquidStateToken));
                string result = templateStateToken.Render(Hash.FromAnonymousObject(new { a = new Graph(graph) }));
                string outputFpath = Path.Combine(outputDir, $"{stateMachineName}StateToken.autogen.cs");
                File.WriteAllText(outputFpath, result);
            }

            // ActionTokensCodeGenerator
            {
                string liquidActionToken = Path.Combine(templateDir, "actiontoken.liquid");
                Template templateActionToken = Template.Parse(File.ReadAllText(liquidActionToken));
                string result = templateActionToken.Render(Hash.FromAnonymousObject(new { a = new Graph(graph) }));
                string outputFpath = Path.Combine(outputDir, $"{stateMachineName}ActionToken.autogen.cs");
                File.WriteAllText(outputFpath, result);
            }
            
            // State
            {
                string stateBaseDir = Path.Combine(outputDir, "States");
                if (!Directory.Exists(stateBaseDir))
                {
                    Directory.CreateDirectory(stateBaseDir);
                }
                
                // base state
                string liquidBaseState = Path.Combine(templateDir, "basestate.liquid");
                Template templateBaseState = Template.Parse(File.ReadAllText(liquidBaseState));
                string result = templateBaseState.Render(Hash.FromAnonymousObject(new { a = new Graph(graph) }));
                string outputFpath = Path.Combine(stateBaseDir, $"Base{stateMachineName}State.autogen.cs");
                File.WriteAllText(outputFpath, result);

                // states
                string liquidState = Path.Combine(templateDir, "state.liquid");
                Template templateState = Template.Parse(File.ReadAllText(liquidState));
                StateInfo[] states = graph.Nodes
                    .Where(x => !x.IsFinal)
                    .Select(n => new StateInfo
                    {
                        Name = n.Semantic,
                        RelativePath = $"States/{stateMachineName}{n.Semantic}State.autogen.cs",
                    })
                    .ToArray();
                foreach (StateInfo state in states)
                {
                    INode node = graph.Nodes.FirstOrDefault(x => x.Semantic == state.Name);
                    ITransition[] transitions = graph.Transitions.Where(tr => tr.Source == node).ToArray();
                    TransitionInfo[] ts = transitions.Select(x => new TransitionInfo { Name = x.Semantic}).ToArray();
                    string result2 = templateState.Render(Hash.FromAnonymousObject(new
                    {
                        a = new Graph(graph),
                        s = state,
                        transitions = ts,
                    }));
                
                    string outputFpath2 = Path.Combine(outputDir, state.RelativePath);
                    

                    File.WriteAllText(outputFpath2, result2);
                }
            }
        }
    }
}