using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script {
    [Serializable]
    public struct FileSave {

        public List<SerializableTransform> SerializableTransforms ;

        public FileSave(List<GameObject> gameObjects) {
            SerializableTransforms = new List<SerializableTransform>();
            foreach (GameObject o in gameObjects) {
                SerializableTransform serializableTransform = new SerializableTransform{
                    Position = o.transform.position,
                    Rotation = o.transform.rotation,
                    Scale = o.transform.localScale,
                    Velocity = o.GetComponent<Rigidbody>().velocity,
                    Color = o.GetComponent<Renderer>().material.color,
                };
                SerializableTransforms.Add(serializableTransform);
            }
        }

    }
}