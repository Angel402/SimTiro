using System;
using UnityEngine;
using UnityEngine.AI;

namespace Utilities
{
    public class NpcWalkAndAnimation : MonoBehaviour
    {
        [SerializeField] private Transform walkToTransform;
        private NavMeshAgent _navMesh;
        private Animator _animator;

        private void Awake()
        {
            _navMesh = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _navMesh.destination = walkToTransform.position;
        }

        private void Update()
        {
            if (_navMesh.remainingDistance <= .1f)
            {
                _animator.SetTrigger("Arrived");
            }
        }
    }
}