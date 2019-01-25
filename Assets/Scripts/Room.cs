﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Room : MonoBehaviour
    {
        public RoomPosition PositionPrefab;
        public Vector2Int Size;
        public Vector2 Resolution;
        public RoomPosition[,] Positions;

        private Transform positionParent;

        private void Awake()
        {
            SetupNewRoom();
        }

        [ContextMenu("Setup")]
        public void SetupNewRoom()
        {
            if (positionParent != null)
            {
                positionParent.DestroyGameObject();
            }

            positionParent = new GameObject("Positions").transform;
            positionParent.SetParent(transform);
            positionParent.localPosition = Vector3.zero;

            Positions = new RoomPosition[Size.x, Size.y];

            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    var position = Instantiate(PositionPrefab, positionParent);
                    position.Position = new Vector2Int(x, y);

                    position.transform.position = new Vector3(x * Resolution.x, transform.position.y, y * Resolution.y);
                    Positions[x, y] = position;
                }
            }
        }

        public bool CanPlaceAtPosition(int x, int y)
        {
            if (x < 0 || x >= Size.x)
            {
                return false;
            }

            if (y < 0 || y >= Size.y)
            {
                return false;
            }

            var position = Positions[x, y];
            
            return !position.IsTaken;
        }
    }
}