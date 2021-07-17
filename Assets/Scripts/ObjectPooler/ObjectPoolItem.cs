using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolItem
    {
        public Transform Container { get; private set; }

        public Queue<GameObject> Objects;

        public ObjectPoolItem(Transform container)
        {
            Container = container;
            Objects = new Queue<GameObject>();
        }
    }    
