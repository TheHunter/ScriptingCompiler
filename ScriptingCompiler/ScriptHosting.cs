
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;

namespace TheHunter.Scripting
{
    
    public class ScriptHosting
    {
        private readonly ScriptEngine scriptEngine;
        private readonly Session session;

        public ScriptHosting(IEnumerable<string> namespaces = null, IEnumerable<Assembly> assemblies = null)
        {
            List<Assembly> references = new List<Assembly>() {   
                                    typeof(IEnumerable<>).Assembly,  
                                    typeof(IQueryable).Assembly,
                                    typeof(IQueryable<>).Assembly,
                                    typeof(List<>).Assembly };
            List<string> ns = new List<string>() { "System", "System.Collections", "System.Collections.Generic", "System.Text" };

            if (assemblies != null)
                references.AddRange(assemblies);

            if (namespaces != null)
                ns.AddRange(namespaces);

            this.scriptEngine = new ScriptEngine();
            references.ForEach(assembly => scriptEngine.AddReference(assembly));
            ns.ForEach(s => scriptEngine.ImportNamespace(s));

            this.session = scriptEngine.CreateSession(this);
        }

        public object Execute(string code)
        {
            return session.Execute(code);
        }

        public T Execute<T>(string code)
        {
            return session.Execute<T>(code);
        }

        public void ExecuteFile(string path)
        {
            session.ExecuteFile(path);
        }
    }
}
