using GameFramework;
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //隐藏管道与自身
            collision.gameObject.SetActive(false);
            GameEntry.Entity.HideEntity(this);
            //派发加分事件
            AddScoreEventArgs e = ReferencePool.Acquire<AddScoreEventArgs>();
            GameEntry.Event.Fire(this, e.Fill(10));
        }
    }
}

