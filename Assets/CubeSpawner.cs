using System;
using System.Collections.Generic;
using System.IO;
using Script;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour {

    public GameObject CubePrefab;

    private List<GameObject> _cubeList = new List<GameObject>();
    private string _savedData;
    private string _filePath;

    private void Start()
    {
        _filePath = Directory.GetCurrentDirectory() + "/Assets/SaveFile.json";
        Debug.Log(_filePath);
    }

    private void Update() {
        if (Input.GetButton("Jump")) {
            GameObject instantiate = Instantiate(CubePrefab, transform.position, Quaternion.identity);
            instantiate.GetComponent<Renderer>().material.color = Random.ColorHSV();
            _cubeList.Add(instantiate);
        }
        if (Input.GetButtonDown("Fire1")) {
            // Serialization process
            if(File.Exists(_filePath)) File.Delete(_filePath);
    
            FileSave fileSave = new FileSave(_cubeList);
            _savedData = JsonUtility.ToJson(fileSave);
            
            using(FileStream fs = File.Create(_filePath))
            {
                using var sr = new StreamWriter(fs);
                sr.Write(_savedData);
            }
        }
        if (Input.GetButtonDown("Fire2")) {
            // Clear state
            foreach (GameObject o in _cubeList) {
                Destroy(o);
            }
            _cubeList.Clear();
            // Deserialization process
            using FileStream fs = File.OpenRead(_filePath);
            using var sr = new StreamReader(fs);
            
            FileSave fileSave = JsonUtility.FromJson<FileSave>(sr.ReadToEnd());
            foreach (SerializableTransform serializableTransform in fileSave.SerializableTransforms) {
                GameObject instantiate = Instantiate(CubePrefab, transform.position, Quaternion.identity);
                instantiate.transform.position = serializableTransform.Position;
                instantiate.transform.rotation = serializableTransform.Rotation;
                instantiate.transform.localScale = serializableTransform.Scale;
                instantiate.GetComponent<Rigidbody>().velocity = serializableTransform.Velocity;
                instantiate.GetComponent<Renderer>().material.color = serializableTransform.Color;
                _cubeList.Add(instantiate);
            }
        }
    }

}