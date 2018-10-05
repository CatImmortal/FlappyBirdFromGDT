using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdFromGDT
{
    /// <summary>
    /// 管道实体数据
    /// </summary>
    public class PipeData : EntityData
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        public float MoveSpeed { get; private set; }

        /// <summary>
        /// 上管道偏移
        /// </summary>
        public float OffsetUp { get; private set; }
        /// <summary>
        /// 下管道偏移
        /// </summary>
        public float OffsetDown { get; private set; }

        /// <summary>
        /// 到达此目标时隐藏自身
        /// </summary>
        public float HideTarget { get; private set; }

        public PipeData(int entityId, int typeId,float moveSpeed) : base(entityId, typeId)
        {
            MoveSpeed = moveSpeed;
            OffsetUp = Random.Range(3f, 6f);
            OffsetDown = Random.Range(-3f, -5f);
            HideTarget = -9.4f;
        }
    }
}

