using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYellowpaper.SerializedCollections
{
    public class SerializedDictionarySample : MonoBehaviour
    {
        [SerializedDictionary("Element Type", "Description")]
        public SerializedDictionary<bool, string> ElementDescriptions;
        
        public enum ElementType
        {
            Fire,
            Air,
            Earth,
            Water
        }
    }
}