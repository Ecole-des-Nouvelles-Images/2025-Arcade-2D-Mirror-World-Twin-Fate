using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private List<GameObject> _pattern;
    [SerializeField] private Transform _startPattern;
    [SerializeField] private Transform _level;

    [Header("Paramètres")]
    [SerializeField] private float _minVerticalDistance;
    [SerializeField] private float _maxVerticalDistance;
    [SerializeField] private float _distanceAhead;
    [SerializeField] private float _timeBetweenGenerations;

    private float _lastYPosition;
    private float _timeSinceLastGeneration;
    public bool _spawn;

    private void Start()
    {
        _lastYPosition = _startPattern.position.y;

        while (_lastYPosition < _distanceAhead)
        {
            GeneratePattern();
        }
    }

    private void Update()
    {
        _timeSinceLastGeneration += Time.deltaTime;

        if (_spawn)
        {
            if (_timeSinceLastGeneration >= _timeBetweenGenerations)
            {
                GeneratePattern();
                _timeSinceLastGeneration = 0f;
            } 
        }
    }

    private void GeneratePattern()
    {
        _lastYPosition += Random.Range(_minVerticalDistance, _maxVerticalDistance);

        Vector3 newPosition = new Vector3(_startPattern.position.x, _lastYPosition, _startPattern.position.z);

        GameObject randomPattern = _pattern[Random.Range(0, _pattern.Count)];

        Instantiate(randomPattern, newPosition, Quaternion.identity, _level);
    }
}