using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] private Transform _entryPoint;
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private Transform _retirePoint;
    [SerializeField] private List<Dock> _docks;

    public Transform EntryPoint => _entryPoint;
    public Transform ExitPoint => _exitPoint;
    public Transform RetirePoint => _retirePoint;

    public Dock GetAvailableDock()
    {
        if (_docks == null || _docks.Count == 0) return null;
        return _docks[Random.Range(0, _docks.Count)];
    }

    public bool TryReserveAnyCustomer(out Dock availableDock)
    {
        availableDock = null;

        for (int i = 0; i < _docks.Count; i++)
        {
            if (_docks[i].TryReserveCustomer())
            {
                availableDock = _docks[i];
                return true;
            }
        }

        return false;
    }
}