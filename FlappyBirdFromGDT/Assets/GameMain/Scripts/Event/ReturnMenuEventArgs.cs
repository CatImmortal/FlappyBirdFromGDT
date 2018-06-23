using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdFromGDT
{
    /// <summary>
    /// 返回菜单场景事件
    /// </summary>
    public class ReturnMenuEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ReturnMenuEventArgs).GetHashCode();

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

