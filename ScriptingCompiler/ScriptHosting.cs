using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;

namespace TheHunter.Scripting
{
    /// <summary>
    /// Class ScriptHosting.
    /// </summary>
    public class ScriptHosting
    {
        private readonly ScriptEngine scriptEngine;
        private readonly Session session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptHosting"/> class.
        /// </summary>
        /// <param name="namespaces">The namespaces.</param>
        /// <param name="assemblies">The assemblies.</param>
        public ScriptHosting(IEnumerable<string> namespaces = null, IEnumerable<Assembly> assemblies = null)
        {
            List<Assembly> references = new List<Assembly> {   
                                    typeof(IEnumerable<>).Assembly,  
                                    typeof(IQueryable).Assembly,
                                    typeof(IQueryable<>).Assembly,
                                    typeof(List<>).Assembly };

            List<string> ns = new List<string> { "System", "System.Collections", "System.Collections.Generic", "System.Text" };

            if (assemblies != null)
                references.AddRange(assemblies);

            if (namespaces != null)
                ns.AddRange(namespaces);

            this.scriptEngine = new ScriptEngine();
            references.ForEach(assembly => this.scriptEngine.AddReference(assembly));
            ns.ForEach(s => this.scriptEngine.ImportNamespace(s));

            this.session = this.scriptEngine.CreateSession(this);
        }

        /// <summary>
        /// Executes the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>System.Object.</returns>
        public object Execute(string code)
        {
            return this.session.Execute(code);
        }

        /// <summary>
        /// Executes the specified code.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code">The code.</param>
        /// <returns>T.</returns>
        public T Execute<T>(string code)
        {
            return this.session.Execute<T>(code);
        }

        /// <summary>
        /// Executes the file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void ExecuteFile(string path)
        {
            this.session.ExecuteFile(path);
        }
    }
}
