﻿using ProtoBuf;
using System;

namespace FlappyBirdFromGDT
{
    /// <summary>
    /// 服务器发往客户端 的 心跳包
    /// </summary>
    [Serializable, ProtoContract(Name = @"SCHeartBeat")]
    public partial class SCHeartBeat : SCPacketBase
    {
        public SCHeartBeat()
        {

        }

        public override int Id
        {
            get
            {
                return 2;
            }
        }

        public override void Clear()
        {

        }
    }
}
