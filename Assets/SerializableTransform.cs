using System;
using UnityEngine;

namespace Script {
    [Serializable]
    public struct SerializableTransform {

        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public Vector3 Velocity;
        public Color Color;
    }
}