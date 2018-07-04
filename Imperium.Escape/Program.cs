using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Imperium.Client;
using Imperium.Server.Generation.Attributes;
using Microsoft.CSharp;
using Province.Log;

namespace Imperium.Escape
{
    internal class Program
    {
        public static Log Log;

        static Program()
        {
            Log = new Log(Console.Out);
        }
        
        public static void Main(string[] args)
        {
            Log.Message("Imperium.Escape started");

            var serverAssembly = typeof(Application.Permission).Assembly;
            
            Log.Message("Server application loaded");

            var responseMethods =
                serverAssembly
                    .GetTypes()
                    .Where(t => t.GetCustomAttributes(false).Any(a => a is ResponseContainerAttribute))
                    .SelectMany(t
                        => t.GetMethods()
                            .Where(m => m.GetCustomAttributes(false).Any(a => a is ResponseAttribute)));
            
            /*var generator = SyntaxGenerator.GetGenerator(new AdhocWorkspace(), LanguageNames.CSharp);

            var code = generator.CompilationUnit(
                generator.NamespaceImportDeclaration("Imperium.Client"),
                generator.ClassDeclaration(
                    "NetManager",
                    accessibility: Accessibility.Public,
                    baseType: generator.DottedName(typeof(AbstractNetManager).FullName),
                    members: responseMethods
                        .Select(m => generator.MethodDeclaration(
                            m.Name, 
                            accessibility: Accessibility.Public,
                            parameters: m.GetParameters()
                                .Select(p => generator.ParameterDeclaration(
                                    p.Name, 
                                    generator.DottedName(p.ParameterType.FullName))),
                            statements: new[]
                            {
                                generator.InvocationExpression(
                                    generator.GenericName(
                                        "Request", 
                                        generator.DottedName(m.ReturnType.FullName)),
                                    generator.ObjectCreationExpression(
                                        generator.GenericName(
                                            typeof(Dictionary<string, object>).FullName, 
                                            generator.DottedName(typeof(string).FullName),
                                            generator.DottedName(typeof(object).FullName))))
                            }))));*/

            var fileTemplate = ReadFile("templates/NetManager.cs");
            var methodTemplate = ReadFile("templates/RequestMethod.cs");

            var code = string.Format(
                fileTemplate,
                responseMethods
                    .Select(m => string.Format(
                        methodTemplate,
                        m.ReturnType.FullName,
                        m.Name,
                        m.GetParameters().Length == 1
                            ? ""
                            : m.GetParameters()
                                .Skip(1)
                                .Aggregate("", (sum, p) => $"{sum}, {TypeToString(p.ParameterType)} {p.Name}")
                                .Substring(2),
                        m.GetParameters().Length == 1
                            ? ""
                            : m.GetParameters()
                                .Skip(1)
                                .Aggregate("", (sum, p) => $"{sum},\n{{\"{p.Name}\", {p.Name}}}")
                                .Substring(2)))
                    .Aggregate("", (sum, str) => sum + "\n\n" + str));
            
            Log.Message("Generated code:\n\n" + code);
            
            using (var file = File.Open("NetManager.cs", FileMode.Create))
            using (var writer = new StreamWriter(file))
            {
                writer.Write(code);
            }
            
            Log.Message("Compilation is starting");
            
            var process = Process.Start(
                "C:/Windows/Microsoft.NET/Framework/v3.5/csc.exe", 
                "-target:library -out:Caesar.Net.dll -debug NetManager.cs " 
                + "-reference:Imperium.Client.dll -reference:Imperium.Server.dll "
                + "-reference:Imperium.CommonData.dll -reference:Province.Vector");
            
            Log.Message("Finished generation");
        }

        private static string ReadFile(string path)
        {
            string result;
            using (var stream = File.OpenRead(path))
            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        private static string TypeToString(Type type)
        {
            return type.IsGenericType
                ? type.FullName.Split('`')[0] +
                  $"<{type.GetGenericArguments().Aggregate("", (sum, p) => $"{sum}, {TypeToString(p)}".Substring(2))}>"
                : type.FullName;
        }
    }
}