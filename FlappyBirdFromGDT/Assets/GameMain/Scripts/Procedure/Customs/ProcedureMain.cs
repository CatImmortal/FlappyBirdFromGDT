using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
namespace FlappyBirdFromGDT
{
    /// <summary>
    /// 主要流程
    /// </summary>
    public class ProcedureMain : ProcedureBase
    {

        /// <summary>
        /// 管道产生时间
        /// </summary>
        private float m_PipeSpawnTime = 0f;

        /// <summary>
        /// 管道产生计时器
        /// </summary>
        private float m_PipeSpawnTimer = 0f;

        private bool isReturnMenu = false;

        private int scoreFormId = -1;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            scoreFormId = GameEntry.UI.OpenUIForm(UIFormId.ScoreForm).Value;

            GameEntry.Entity.ShowBg(new BgData(GameEntry.Entity.GenerateSerialId(),1,1f,0));
            GameEntry.Entity.ShowBird(new BirdData(GameEntry.Entity.GenerateSerialId(), 3, 5f));
            //设置初始管道产生时间
            m_PipeSpawnTime = Random.Range(3f, 5f);

            //订阅事件
            GameEntry.Event.Subscribe(ReturnMenuEventArgs.EventId, OnReturnMenu);
           
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            m_PipeSpawnTimer += elapseSeconds;
            if (m_PipeSpawnTimer >= m_PipeSpawnTime)
            {
                m_PipeSpawnTimer = 0;
                //随机设置管道产生时间
                m_PipeSpawnTime = Random.Range(3f, 5f);

                //产生管道
                GameEntry.Entity.ShowPipe(new PipeData(GameEntry.Entity.GenerateSerialId(), 2, 1f));

            }

            //切换场景
            if (isReturnMenu)
            {
                isReturnMenu = false;
                procedureOwner.SetData<VarInt>(Constant.ProcedureData.NextSceneId, GameEntry.Config.GetInt("Scene.Menu"));
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }

        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.UI.CloseUIForm(scoreFormId);
            //取消订阅事件
            GameEntry.Event.Unsubscribe(ReturnMenuEventArgs.EventId, OnReturnMenu);
          
        }

        private void OnReturnMenu(object sender,GameEventArgs e)
        {
            isReturnMenu = true;
        }
       

    }
}

