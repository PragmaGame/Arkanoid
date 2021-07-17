using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Sensor _sensor;
    [SerializeField] private ViewGameUI _viewGameUI;
    [SerializeField] private ObjectPooler _objectPooler;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private Types[] _typesBlock;
    [SerializeField] private float[] _probabilityTypesBlock;
   

    private List<Block> _blocks = new List<Block>();
    private int _score = 0;

    private readonly int _maxCapacityBlockInLine = 6;
    private readonly int _maxAmountBlockInLine = 6;
    private readonly int _minAmountBlockInLine = 0;
    private readonly float _waitSpawnBlock = 4f;
    private readonly float _blockWith = 0.8f;
    private readonly float _spawnPositionY = 4.15f;
    private readonly float _speed = 0.1f;

    public event Action<int> ChangeScoreEvent;
    public event Action GameOveredEvent;

    private void OnEnable()
    {
        _sensor.GameOverEvent += OnGameOver;
    }

    private void OnDisable()
    {
        _sensor.GameOverEvent -= OnGameOver;
    }

    private void Start()
    {   
        _particle.Stop();
        InvokeRepeating(nameof(FillingLine), 0f, _waitSpawnBlock);
    }

    private void Update()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            if (_blocks[i].IsDead)
            {
                _particle.transform.position = _blocks[i].gameObject.transform.position;
                _particle.Play();
                Debug.Log(_particle.isPlaying);
                
                AddScore(_blocks[i].Score);
                _objectPooler.ReturnObject(_blocks[i].gameObject, _blocks[i].Type);
                _blocks.RemoveAt(i);
                i--;
            }
            else
            {
                _blocks[i].gameObject.transform.Translate(Vector2.down * (Time.deltaTime * _speed));
            }
        }
    }
    
    private void AddScore(int value)
    {
        _score += value;
        ChangeScoreEvent?.Invoke(_score);
    }

    private int ChooseType(float[] probabilityTypes)
    {
        float total = 0;

        for (int i = 0; i < probabilityTypes.Length; i++)
        {
            total += probabilityTypes[i];
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probabilityTypes.Length; i ++)
        {
            if (randomPoint < probabilityTypes[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probabilityTypes[i];
            }
        }

        return probabilityTypes.Length - 1;
    }

    private int RandomCountBlockInLine()
    {
        return Random.Range(_minAmountBlockInLine, _maxAmountBlockInLine + 1);
    }
    
    private void FillingLine()
    {
        int numToChoose = RandomCountBlockInLine();

        for (int numLeft = _maxCapacityBlockInLine; numLeft > 0; numLeft--)
        {
            float prob = (float)numToChoose / numLeft;

            if (Random.value <= prob)
            {
                numToChoose--;
                SpawnBlock(CoordinateX(numLeft - 1));

                if (numToChoose == 0)
                {
                    break;
                }
            }
        }
    }

    private void SpawnBlock(float posX)
    {
        int randType = ChooseType(_probabilityTypesBlock);
        Debug.Log("rand Type" + randType);
        Debug.Log("Type : " + _typesBlock[randType]);
        GameObject block = _objectPooler.GetObject(_typesBlock[randType], true);
        block.transform.position = new Vector2(posX, _spawnPositionY);

        _blocks.Add(block.GetComponent<Block>());
    }
    
    
    private float CoordinateX(int position)
    {
        float xPos;
        int maxCapacityBlockInLineOneSide = _maxCapacityBlockInLine / 2 ;
        float correctionFactor = 0.4f;
        
        if (position < maxCapacityBlockInLineOneSide)
        {
            xPos = (maxCapacityBlockInLineOneSide - position) * -_blockWith + correctionFactor;
        }
        else
        {
            xPos = (position - maxCapacityBlockInLineOneSide) * _blockWith + correctionFactor;
        }

        return xPos;
    }

    private void OnGameOver()
    {
        CancelInvoke();
        
        for (int i = 0; i < _blocks.Count; i++)
        {
            _blocks[i].IsDead = true;
        }
        
        GameOveredEvent?.Invoke();
    }
}
