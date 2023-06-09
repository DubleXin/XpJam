﻿using UnityEngine;
using System.Collections;
using System;

public class PudgeAI : MonoBehaviour
{
    [SerializeField] private float _hookCD, _meleeCD, _meleeRange;
    [SerializeField] private Transform _hookOrigin;
    [SerializeField] private Animator _hook;
    [SerializeField] private GameObject _slash;
    [SerializeField] private float _damage;
    private bool _hookOnCD = false, _meleeOnCD = false, _moveOnCD = false;
    private Transform _player;
    private TopDownMovement _movement;
    private DamageReceiver _receiver;
    private Transform Player 
    {
        get
        {
            if (_player is null)
                _player = GameObject.FindGameObjectWithTag("Player").transform;
            return _player;
        }
    }
    private void Start() 
    { 
        _movement = GetComponent<TopDownMovement>();
        _receiver = GetComponent<DamageReceiver>();
        _receiver.OnDeath += OnDeath;
    }
    private void OnDeath() => Destroy(gameObject);
    private void FixedUpdate()
    {
        if (Vector2.Distance(Player.position, transform.position) < _meleeRange)
        {
            if (!_meleeOnCD)
                StartCoroutine(Melee());
        }

        else if (!_hookOnCD)
            StartCoroutine(Hook());

        else if (!_moveOnCD)
            _movement.OnMove((Player.position - transform.position).normalized);
        else
            _movement.OnMove(Vector2.zero);
    }
    
    private IEnumerator Hook()
    {
        yield return new WaitForSeconds(0.6f);
        _moveOnCD = true;
        _hookOnCD = true;
        _hookOrigin.rotation = Angle.FromAtoB(_hookOrigin.transform.position, Player.position + new Vector3(0, 0.1f));
        _hook.SetTrigger("Activate");
        yield return new WaitForSeconds(0.6f);
        _moveOnCD = false;
        yield return new WaitForSeconds(_hookCD - 1.2f);
        _hookOnCD = false;
    }
    private IEnumerator Melee()
    {

        yield return new WaitForSeconds(0.2f);
        _moveOnCD = true;
        _meleeOnCD = true;
        Transform slash = Instantiate(_slash, transform).transform;
        slash.localPosition = new Vector3(0, 0.25f);
        slash.rotation = Angle.FromAtoB(transform.position, Player.position + new Vector3(0, 0.1f));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.position - transform.position, _meleeRange,LayerMask.GetMask("Player"));
        if(hit)
        {
            DamageReceiver receiver = hit.transform.GetComponent<DamageReceiver>();
            receiver.TakeDamage(_damage, transform.gameObject);
        }
        yield return new WaitForSeconds(0.6f);
        _moveOnCD = false;
        yield return new WaitForSeconds(_meleeCD - 0.8f);
        _meleeOnCD = false;
    }
}