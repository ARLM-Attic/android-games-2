﻿public interface IIDI
{
    System.Drawing.Point MousePoint { get; }
    MouseMessage Update();
    MouseMessage Mouse { get; }
}