using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace PacketScanner.Packer
{
    public class ResourceFile
    {
        private string _resourceName = null;
        private string _targetfileName;

        public string TargetfileName { get => _targetfileName; }

        public ResourceFile(string resourceName)
        {
            _resourceName = resourceName;
        }

        public void Delete()
        {
            try
            {
                File.Delete(_targetfileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public string CopyTo(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }

            _targetfileName = fileName;
            if (fileName.IndexOf('\\') == -1)
            {
                _targetfileName = string.Format("{0}PacketScanner_{1}\\{2}", Path.GetTempPath(),Process.GetCurrentProcess().Id, fileName);
            }

            if(File.Exists(_targetfileName))
            {
                try { File.Delete(_targetfileName); } catch { }
            }

            string targetDir = Path.GetDirectoryName(_targetfileName);
            if(!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            var assembly = Assembly.GetExecutingAssembly();
            using (var inputStream = assembly.GetManifestResourceStream(_resourceName))
            using (var outputStream = File.Create(_targetfileName))
            {
                CopyStream(inputStream, outputStream);
            }

            return _targetfileName;
        }

        public Assembly LoadAssembly()
        {
            if (string.IsNullOrEmpty(_targetfileName))
            {
                return null;
            }
           return Assembly.LoadFile(_targetfileName);
        }
    }
}