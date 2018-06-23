using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdFromGDT
{
    /// <summary>
    /// 小鸟实体脚本
    /// </summary>
    public class Bird : Entity
    {
        /// <summary>
        /// 小鸟实体数据
        /// </summary>
        private BirdData m_BirdData = null;

        /// <summary>
        /// 射击时间
        /// </summary>
        private float m_ShootTime = 10f;

        /// <summary>
        /// 射击计时器
        /// </summary>
        private float m_ShootTimer = 0f;

        private Rigidbody2D m_Rigidbody = null;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_BirdData = (BirdData)userData;
            //修改缩放
            CachedTransform.localScale = Vector2.one * 2;
            if (m_Rigidbody == null)
            {
                m_Rigidbody = GetComponent<Rigidbody2D>();
            }
            //重置射击冷却
            m_ShootTimer = 10f;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);


            //上升控制
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameEntry.Sound.PlaySound(2);
                m_Rigidbody.velocity = new Vector2(0, m_BirdData.FlyForce);
            }

            //射击控制
            m_ShootTimer += elapseSeconds;
            if (m_ShootTimer >= m_ShootTime && Input.GetKeyDown(KeyCode.J))
            {
                m_ShootTimer = 0;
                GameEntry.Sound.PlaySound(3);
                GameEntry.Entity.ShowBullet(new BulletData(GameEntry.Entity.GenerateSerialId(), 4,CachedTransform.position + (CachedTransform.right), 6));
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameEntry.Sound.PlaySound(1);
            GameEntry.Entity.HideEntity(this);
            //派发小鸟死亡事件
            GameEntry.Event.Fire(this, ReferencePool.Acquire<BirdDeadEventArgs>());
           
        }
    }
}

