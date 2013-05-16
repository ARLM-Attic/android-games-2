using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class ResourceLoader
{
    /// <summary>
    /// 获取frame对应的图片数据
    /// </summary>
    /// <param name="modelId"></param>
    /// <param name="actionId"></param>
    /// <param name="directionId"></param>
    /// <param name="frameIndex"></param>
    /// <returns></returns>
    public static byte[] GetFrameData(int modelId, int actionId, int directionId, int frameIndex)
    {
        byte[] data = null;

        string modelPath = string.Format("{0}models\\{1:d4}\\", DATUtility.GetResPath(), modelId);
        string frameFile = string.Format("{4}{0:d4}-{1:d4}-{2:d4}-{3:d4}.bmp", modelId, actionId, directionId, frameIndex, modelPath);
        if (System.IO.File.Exists(frameFile))
        {
            using (Stream inFileStream = new System.IO.FileStream(frameFile, FileMode.Open))
            {
                data = new byte[inFileStream.Length];
                inFileStream.Read(data, 0, data.Length);
            }
        }
        return data;
    }
}