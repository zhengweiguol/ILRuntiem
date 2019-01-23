using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TestDelegate(int t);
public class Test : MonoBehaviour {

    public static TestDelegate TestD;
    public int a;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
