using System;
using System.Collections;
using UnityEngine;

namespace Jenga.Data
{
    [Serializable]
    public class BlockTransform
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public GameObject BlockObj;

        public BlockTransform(Vector3 position, Vector3 roration, GameObject obj)
        {
            Position = position;
            Rotation = roration;
            BlockObj = obj;
        }
        
    }
}