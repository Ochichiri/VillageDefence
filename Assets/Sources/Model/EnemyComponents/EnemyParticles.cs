using Infrastructure;
using UnityEngine;
using View.EnemyComponents;

namespace Model.EnemyComponents
{
    public class EnemyParticles : Object
    {
        private Enemy _enemy;
        private ParticlePool _hitPool;
        private ParticleSystem _death;
        private Vector3 _particleOffset = new Vector3(0, 1.5f, 0);

        public EnemyParticles(Enemy enemy, ParticleSystem death, Particle hit)
        {
            _enemy = enemy;
            _death = death;
            _hitPool = new ParticlePool(hit);

            _enemy.Dying += Die;
            _enemy.GotDamage += GetHit;
        }

        public void GetHit()
        {
            Particle particle = _hitPool.GetObject();
            particle.gameObject.SetActive(true);
            particle.gameObject.transform.SetParent(_enemy.gameObject.transform);
            particle.gameObject.transform.localPosition = Vector3.zero + _particleOffset;
            particle.Play();
        }

        public void Die()
        {
            Particle particle = _hitPool.GetObject();
            particle.gameObject.SetActive(true);
            particle.gameObject.transform.position = _enemy.Position + _particleOffset;
            particle.Play();

            ParticleSystem deathParticle = Object.Instantiate(_death);
            deathParticle.gameObject.transform.position = _enemy.Position + _particleOffset;
            deathParticle.Play();
        }
    }
}