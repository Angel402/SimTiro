using System;
using UnityEngine;

public class DieOnHit : MonoBehaviour
{
    private Collider[] _colliders;
    private Animator _animator;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<Collider>();
        _animator = GetComponent<Animator>();
        foreach (var collider1 in _colliders)
        {
            collider1.gameObject.AddComponent<DieOnHitForColliders>().MainParentAnimator = _animator;
        }
    }
}