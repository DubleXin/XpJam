using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private float _baseValue;
    private readonly List<StatModifier> _modifiersList;
    public readonly ReadOnlyCollection<StatModifier> Modifiers;
    private float _currentValue;
    private float _lastBaseValue = float.MinValue;
    private bool _isDirtyValue = true;
    public float FinalValue
    {
        get 
        {
            if (_isDirtyValue || _baseValue != _lastBaseValue)
            {
                _lastBaseValue = _baseValue;
                _currentValue = CalculateFinalValue();
                _isDirtyValue = false;

                return _currentValue;
            }

            return _currentValue;
        }
    }
    public Stat()
    {
        _modifiersList = new List<StatModifier>();
        Modifiers = _modifiersList.AsReadOnly();
    }
    public Stat(float value) : this()
    {
        _baseValue = value;       
    }
    public void UpdateBaseValue(float value)
    {
        _baseValue += value;
    }
    public void AddModifier(StatModifier mod)
    {
        _isDirtyValue = true;
        _modifiersList.Add(mod);
        _modifiersList.Sort(CompareModifierOrder);
    }
    public bool RemoveAllMods()
    {
        bool didRemove = false;

        for (int i = _modifiersList.Count-1; i > 0; i--)
        {
            _modifiersList.RemoveAt(i);
            _isDirtyValue = true;
            didRemove = true;
        }

        return didRemove;
    }
    private int CompareModifierOrder(StatModifier modA, StatModifier modB)
    {
        if(modA.Order > modB.Order)
            return 1;
        if (modA.Order < modB.Order) 
            return -1;        
        return 0;
    }

    public bool RemoveModifier(StatModifier mod)
    {   
        if(_modifiersList.Remove(mod))
        {
            _isDirtyValue = true;

            return true;
        }

        return false;
    }
    private float CalculateFinalValue()
    {
        float finalValue = _baseValue;
        float sumPercent = 0;

        for (int i = 0; i < _modifiersList.Count; i++)
        {
            switch (_modifiersList[i].Type)
            {
                case StatModType.Flat:
                    finalValue += _modifiersList[i].ModValue;
                    break;
                case StatModType.PercentMult:
                    finalValue *= 1 + _modifiersList[i].ModValue;
                    break;
                case StatModType.PercentPlus:
                    sumPercent += _modifiersList[i].ModValue;
                    if (i + 1 > _modifiersList.Count || _modifiersList[i + 1].Type != StatModType.PercentPlus)
                    {
                        finalValue *= 1 + sumPercent;
                        sumPercent = 0;
                    }
                    break;
            }
        }

        return finalValue;
    }
}