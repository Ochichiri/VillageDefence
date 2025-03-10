using Model.EnemyComponents;
using UnityEngine;
using View.PlayerComponents;

namespace View.EnemyComponents
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class EnemyAttackZone : MonoBehaviour
    {
        private Player _target;
        private EnemyAnimator _animator;
        private float _damage;
        private bool _isAttacking;

        public void Init(EnemyAnimator animator, float damage)
        {
            _animator = animator;
            _isAttacking = false;

            if (damage >= 0)
                _damage += damage;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_isAttacking)
                return;

            if (other.TryGetComponent(out Player player))
            {
                _target = player;
                _isAttacking = true;
                _animator.Shoot();
            }
        }

        public void HitPlayer()
        {
            _target.GetDamage(_damage);
            _isAttacking = false;
        }
    }
}