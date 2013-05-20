using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IADI
{
    /// <summary>
    /// 播放声音
    /// </summary>
    void Play();

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="id"></param>
    void PlayBGM(int id);

    /// <summary>
    /// 播放声音
    /// </summary>
    void PlayAttackSound(AttackSound sound);
}
