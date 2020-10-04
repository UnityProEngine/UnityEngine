using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace engine
{
    public class Globals : MonoBehaviour
    {
        // Static instance
        private static Globals instance;


        public static Globals Instance
        {
            // using the ? operator, to return instance if instance does not equal null. 
            // Otherwise we assign instance to a new component and return that.
            get { return instance != null ? instance : (instance = new GameObject("Singleton").AddComponent<Globals>()); }
        }

        public void WriteToConsole()
        {
            Debug.Log(" ");
        }
    }

    // Used as follows : 
    // Globals.Instance.Function();
}
