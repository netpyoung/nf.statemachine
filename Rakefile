task :default do
  root = Dir.pwd

  output_dir = File.join(root, 'StateMachine/Sample01/output')
  template_dir = File.join(root, 'template')
  graph_dir = File.join(root, 'graphml')
  input_fpath = File.join(graph_dir, 'almost_real_state_machine.graphml')
  puts input_fpath

  Dir.chdir('StateMachine.Tool/StateMachine.Tool.CLI') do
    sh "dotnet run -- -i #{input_fpath} -t #{template_dir} -o #{output_dir}"
  end
end
