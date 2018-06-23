using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdFromGDT
{
    /// <summary>
    /// 加分事件
    /// </summary>
    public class AddScoreEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(AddScoreEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        /// <summary>
        /// 加分数量
        /// </summary>
        public int AddCount { get; private set; }

        /// <summary>
        /// 填充事件
        /// </summary>
        public AddScoreEventArgs Fill(int addCount)
        {
            AddCount = addCount;

            return this;    
        }

        public override void Clear()
        {
            AddCount = 0;
        }
    }
}

