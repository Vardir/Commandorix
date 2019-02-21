﻿using System;
using ParserLib;
using Core.Attributes;
using System.Reflection;
using System.Collections.Generic;
using static ParserLib.CmdParser;

namespace Core.Commands
{
    public class ExecutionService
    {
        private readonly Dictionary<string, Command> commands;

        public ExecutionService()
        {
            commands = new Dictionary<string, Command>();
            LoadCommands();
        }

        public void AddCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");
            if (string.IsNullOrWhiteSpace(cmd.Id))
                throw new ArgumentException("cmd ID is not valid");

            if (commands.ContainsKey(cmd.Id))
                return;

            commands.Add(cmd.Id, cmd);
            cmd.ExecutionService = this;
        }
        public void AddCommands(IEnumerable<Command> commands)
        {
            if (commands == null)
                throw new ArgumentNullException("commands");

            foreach (var cmd in commands)
                AddCommand(cmd);
        }

        public Command FindCommand(string id)
        {
            if (commands.TryGetValue(id, out Command cmd))
                return cmd;
            return null;
        }
        public ExecutionResult Execute(string line)
        {
            var result = Interop.runParser(line);
            
            if (result.successfull)
                return Execute(Interop.extractInnerExpression(result.expression));
            else
                return ExecutionResult.Error(result.message);
        }
        public IEnumerable<string> GetAllCommandsIDs()
        {
            foreach (var kvp in commands)
            {
                yield return kvp.Key;
            }
        }

        private void LoadCommands()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                if (assemblies[i].GetCustomAttribute<ContainsCommandsAttribute>() == null)
                    continue;
                Type[] types = assemblies[i].GetTypes();
                for (int j = 0; j < types.Length; j++)
                {
                    var attr = types[j].GetCustomAttribute<AutoRegistrateAttribute>();
                    if (attr == null)
                        continue;
                    AddCommand(Activator.CreateInstance(types[j]) as Command);
                }
            }
        }

        private ExecutionResult Execute(Expression expr)
        {
            if (expr.IsCCommand)
            {
                var (id, query) = Interop.extractCmd(expr);
                return extractAndExecute(id, query, ExecutionResult.Empty());
            }
            else if (expr.IsCPipeline)
            {
                var cmds = Interop.extractPipeline(expr);
                ExecutionResult result = ExecutionResult.Empty();
                foreach (var cmd in cmds)
                {
                    result = extractAndExecute(cmd.Item1, cmd.Item2, result);
                    if (!result.isSuccessfull && !result.isEmpty)
                    {
                        return ExecutionResult.Error($"{result.errorMessage}\ncmd.error: can not compute pipeline");
                    }
                }
                return result;
            }
            else if (expr.IsCNumber)
                return ExecutionResult.Success(Interop.extractNumber(expr));
            else if (expr.IsCString)
                return ExecutionResult.Success(Interop.extractString(expr));
            else if (expr.IsCArray)
                return ExecutionResult.Success(Interop.extractArray(expr));

            return ExecutionResult.Error("cmd.error: can not execute the given expression");

            ExecutionResult extractAndExecute(string id, Expression query, ExecutionResult input)
            {                
                if (commands.TryGetValue(id, out Command cmd))
                    return cmd.Execute(query, input);
                else
                    return ExecutionResult.Error($"cmd.error: can not find command '{id}'");
            }
        }
    }
}