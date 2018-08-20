using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace PacketScanner.Packer
{
    public class ResourceFile
    {
        private string _resourceName = null;
        private string _targetfileName;

        //We pick a value that is the largest multiple of 4096 that is still smaller than the large object heap threshold (85K).
        // The CopyTo/CopyToAsync buffer is short-lived and is likely to be collected at Gen0, and it offers a significant
        // improvement in Copy performance.
        private const int _DefaultCopyBufferSize = 81920;

        public string TargetfileName { get => _targetfileName; }

        public ResourceFile(string resourceName)
        {
            _resourceName = resourceName;
        }

        public string FileName { get; set; }

        public void Delete()
        {
            if(string.IsNullOrEmpty(_targetfileName))
            {
                return;
            }
            try
            {
                File.Delete(_targetfileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        
        private void CopyTo(Stream input, Stream output)
        {
            byte[] buffer = new byte[_DefaultCopyBufferSize];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) != 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public void CopyTo(Stream output)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var inputStream = assembly.GetManifestResourceStream(_resourceName))
            using (GZipStream decompressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                CopyTo(decompressionStream, output);
            }
        }

        public void CopyTo(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                return;
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
            using (var outputStream = File.Create(_targetfileName))
            {
                CopyTo(outputStream);
            }
        }

        public Assembly LoadAssembly()
        {
            CopyTo(FileName);
            if (string.IsNullOrEmpty(_targetfileName))
            {
                return null;
            }
            return Assembly.LoadFile(_targetfileName);
        }

        public Assembly LoadMemoryAssembly()
        {
            byte[] rawAssembly = null;

            using (MemoryStream output = new MemoryStream())
            {
                CopyTo(output);
                rawAssembly = output.ToArray();
            }

            if (rawAssembly == null)
            {
                return null;
            }
            return Assembly.Load(rawAssembly);
        }
    }
}