using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdFromGDT
{
    /// <summary>
    /// 子弹实体脚本
    /// </summary>
    public class Bullet : Entity
    {

        /// <summary>
        /// 子弹实体数据
        /// </summary>
        private BulletData m_BulletData = null;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_BulletData = (BulletData)userData;

            CachedTransform.SetLocalScaleX(1.8f);
            CachedTransform.position = m_BulletData.ShootPostion;

            //监听小鸟死亡事件
            GameEntry.Event.Subscribe(BirdDeadEventArgs.EventId, OnBirdDead);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            CachedTransform.Translate(Vector2.right * m_BulletData.FlySpeed * elapseSeconds, Space.World);

            //已达到最大飞行距离
            if (CachedTransform.position.x >= 9.1f)
            {
                GameEntry.Entity.HideEntity(this);
            }
        }

        protected override void OnHide(object userData)
        {
            base.OnHide(userData);
            //取消监听小鸟死亡事件
            GameEntry.Event.Unsubscribe(BirdDeadEventArgs.EventId, OnBirdDead);
        }

        private void OnBirdDead(object sender,GameEventArgs e)
        {
            //小鸟死亡后立即隐藏自身
            GameEntry.Entity.HideEntity(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //隐藏管道与自身
            GameEntry.Sound.PlaySound(1);
            collision.gameObject.SetActive(false);
            GameEntry.Entity.HideEntity(this);

            //派发加分事件
            AddScoreEventArgs e = ReferencePool.Acquire<AddScoreEventArgs>();
            GameEntry.Event.Fire(this, e.Fill(10));
        }
    }
}

