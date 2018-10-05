using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace FlappyBirdFromGDT
{
    /// <summary>
    /// 积分界面脚本
    /// </summary>
    public class ScoreForm : UGuiForm
    {
        public Text scoreText;

        /// <summary>
        /// 积分
        /// </summary>
        private int m_Score = 0;

        /// <summary>
        /// 积分计时器
        /// </summary>
        private float m_ScoreTimer = 0;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            //订阅事件
            GameEntry.Event.Subscribe(AddScoreEventArgs.EventId, OnAddScore);
            GameEntry.Event.Subscribe(BirdDeadEventArgs.EventId, OnBirdDead);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_ScoreTimer += elapseSeconds;
            if (m_ScoreTimer >= 2f)
            {
                m_ScoreTimer = 0;
                m_Score += 1;
                scoreText.text = "总分：" + m_Score;
            }
        }

        protected override void OnClose(object userData)
        {
            base.OnClose(userData);

            //取消订阅
            GameEntry.Event.Unsubscribe(AddScoreEventArgs.EventId, OnAddScore);
            GameEntry.Event.Unsubscribe(BirdDeadEventArgs.EventId, OnBirdDead);
        }

        private void OnAddScore(object sender,GameEventArgs e)
        {
            AddScoreEventArgs ase = (AddScoreEventArgs)e;
            
            m_Score += ase.AddCount;
            scoreText.text = "总分：" + m_Score;
        }

        protected override void OnPause()
        {
            base.OnPause();

            //清空数据
            m_ScoreTimer = 0;
            m_Score = 0;
            scoreText.text = "总分：" + m_Score;
        }
        private void OnBirdDead(object sender, GameEventArgs e)
        {
            //往数据结点里存积分数据
            GameEntry.DataNode.GetOrAddNode("Score").SetData<VarInt>(m_Score);
            //打开结束界面
            GameEntry.UI.OpenUIForm(UIFormId.GameOverForm);
        }
    }
}

