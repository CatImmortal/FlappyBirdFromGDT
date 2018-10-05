using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdFromGDT
{
    /// <summary>
    /// 重新开始游戏事件
    /// </summary>
    public class RestartEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(RestartEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public override void Clear()
        {

        }
    }
}

