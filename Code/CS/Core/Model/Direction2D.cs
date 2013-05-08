using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Direction2D
{
    public int Id { get; set; }
    public string Caption { get; set; }

    public List<Frame2D> Frames { get; set; }

    public Direction2D()
    {
        Frames = new List<Frame2D>();
    }

    /// <summary>
    /// 逆时针旋转
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public static int Left(int dir, int count)
    {
        return 0;
    }

    /// <summary>
    /// 顺时针调整
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public static int Right(int dir, int count)
    {
        return 0;
    }
}