using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGEditor
{
    public class PackagingUtility
    {
        #region 打包模型文件
        public static void PublishModel(Model2D model, string publishPath)
        {
            if (Directory.Exists(publishPath))
            {
                Directory.Delete(publishPath, true);
            }
            else
            {
                Directory.CreateDirectory(publishPath);
            }

            foreach (var action in model.Actions)
            {
                foreach (var direction in action.Directions)
                {
                    foreach (var frame in direction.Frames)
                    {
                        CompressF2(publishPath, model, action, direction, frame);
                    }
                }
            }
        }

        private static void CompressF2(string publishPath, Model2D model, Action2D action, Direction2D direction, Frame2D frame)
        {
            string fileToAdd = string.Format("{4}{0:d4}-{1:d4}-{2:d4}-{3:d4}.bmp", model.Id, action.Id, direction.Id, frame.Index, publishPath);
            File.WriteAllBytes(fileToAdd, frame.Data);
        }
        #endregion

        public static void CompressM(Model2D model, string zipFileName)
        {
            if (File.Exists(zipFileName))
            {
                File.Delete(zipFileName);
            }

            using (Package zip = System.IO.Packaging.Package.Open(zipFileName, FileMode.CreateNew))
            {
                foreach (var action in model.Actions)
                {
                    foreach (var direction in action.Directions)
                    {
                        foreach (var frame in direction.Frames)
                        {
                            CompressF(zip, model, action, direction, frame);   
                        }
                    }
                }
            }
        }

        private static void CompressF(Package zip, Model2D model, Action2D action, Direction2D direction, Frame2D frame)
        {
            string fileToAdd = string.Format("{0:d4}-{1:d4}-{2:d4}-{3:d4}", model.Id, action.Id, direction.Id, frame.Index);
            Uri uri = PackUriHelper.CreatePartUri(new Uri(fileToAdd, UriKind.Relative));
            if (zip.PartExists(uri))
            {
                zip.DeletePart(uri);
            }
            PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);
            using (Stream dest = part.GetStream())
            {
                dest.Write(frame.Data, 0, frame.Data.Length);
            }
        }

        private const long BUFFER_SIZE = 4096;
        public static void CompressFiles(Dictionary<string, string> fileNames, string zipFileName)
        {
            if (File.Exists(zipFileName))
            {
                File.Delete(zipFileName);
            }

            using (Package zip = System.IO.Packaging.Package.Open(zipFileName, FileMode.CreateNew))
            {
                foreach (string key in fileNames.Keys)
                {
                    CompressFile(zip, key, fileNames[key]);
                }
            }
        }

        private static void CompressFile(Package zip, string fileToAdd, string addFilePath)
        {
            string destFilename = addFilePath;// ".\\" + Path.GetFileName(fileToAdd);
            Uri uri = PackUriHelper.CreatePartUri(new Uri(fileToAdd, UriKind.Relative));
            if (zip.PartExists(uri))
            {
                zip.DeletePart(uri);
            }
            PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);
            using (FileStream fileStream = new FileStream(destFilename, FileMode.Open, FileAccess.Read))
            {
                using (Stream dest = part.GetStream())
                {
                    CopyStream(fileStream, dest);
                }
            }
        }

        public static void DecompressFile(string zipFilename, string outPath)
        {
            using (Package zip = System.IO.Packaging.Package.Open(zipFilename, FileMode.Open))
            {
                foreach (PackagePart part in zip.GetParts())
                {
                    string outFileName = Path.Combine(outPath, part.Uri.OriginalString.Remove(0, 1));
                    FileInfo fi = new FileInfo(outFileName);
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }
                    using (System.IO.FileStream outFileStream = new System.IO.FileStream(outFileName, FileMode.CreateNew))
                    {
                        using (Stream inFileStream = part.GetStream())
                        {
                            CopyStream(inFileStream, outFileStream);
                        }
                    }
                }
            }
        }

        public static void DecompressFile(Stream stream, string outPath)
        {
            using (Package zip = System.IO.Packaging.Package.Open(stream, FileMode.Open))
            {
                foreach (PackagePart part in zip.GetParts())
                {
                    string outFileName = Path.Combine(outPath, part.Uri.OriginalString.Remove(0, 1));
                    FileInfo fi = new FileInfo(outFileName);
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }
                    using (System.IO.FileStream outFileStream = new System.IO.FileStream(outFileName, FileMode.CreateNew))
                    {
                        using (Stream inFileStream = part.GetStream())
                        {
                            CopyStream(inFileStream, outFileStream);
                        }
                    }
                }
            }
        }

        private static void CopyStream(System.IO.Stream inputStream, System.IO.Stream outputStream)
        {
            long bufferSize = inputStream.Length < BUFFER_SIZE ? inputStream.Length : BUFFER_SIZE;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            long bytesWritten = 0;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                bytesWritten += bufferSize;
            }
        }
    }
}
