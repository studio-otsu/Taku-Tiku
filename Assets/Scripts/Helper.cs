﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour {

	void Start () {
        Match m = Match.PrepareMatch("Data/Maps/map0");
        MatchController mc = Instantiate(Resources.Load<GameObject>("Prefabs/MatchInterface")).GetComponent<MatchController>();
        mc.match = m;
        mc.map = m.map;
        mc.InitializeInterface();
        mc.pathTracer = Instantiate(Resources.Load<GameObject>("Prefabs/PathTracer")).GetComponent<LineRenderer>();
        MatchController.instance = mc;
        m.matchController = mc;
        m.StartMatch();
	}
}
