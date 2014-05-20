using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Core
{
    public static class JCore
    {
        public static JScene CurrentScene { get; private set; }

        /// <summary>
        /// 显示场景
        /// </summary>
        /// <param name="scene"></param>
        public static void Show(JScene scene)
        {
            CurrentScene = scene;
        }
    }
}
