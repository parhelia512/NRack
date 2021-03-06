using System.Collections.Generic;
using System.IO;
using NRack.Helpers;
using Yahoo.Yui.Compressor;

namespace NRack.Example.AspNet
{
    public class YuiCompressor
    {
        private readonly dynamic _app;
        private readonly string _root;

        public YuiCompressor(dynamic app, string root)
        {
            _app = app;
            _root = root;
        }

        public YuiCompressor(string root) : this(null, root)
        {}

        public dynamic[] Call(IDictionary<string, dynamic> env)
        {
            var root = _root;

            if (root.EndsWith("/"))
            {
                root = _root.Remove(_root.Length - 1);
            }

            var path = root + env["PATH_INFO"];
            
            if (!path.EndsWith(".js"))
            {
                return _app.Call(env);
            }

            var response = string.Format("Not found: {0}.", path);
            var responseCode = 400;

            if (System.IO.File.Exists(path))
            {
                var js = System.IO.File.ReadAllText(path);
                response = JavaScriptCompressor.Compress(js);
                responseCode = 200;
            }

            return new dynamic[] {responseCode, new Hash {{"Content-Type", "text/plain"}}, new[] {response}};
        }
    }
}