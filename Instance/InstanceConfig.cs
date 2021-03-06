﻿using System;
using UnityEngine;
using System.Collections;

namespace StateFunding {
  public class InstanceConfig : MonoBehaviour {
    private String saveName = "statefunding.conf";
    private String saveFilePath;
    private NewInstanceConfigView NewView;

    public InstanceConfig () {
      saveFilePath = "saves/" + HighLogic.fetch.GameSaveFolder + "/" + saveName;

    }

    public void createInstance (Action <Instance>Callback) {
      NewView = new NewInstanceConfigView ();
      NewView.OnCreate (Callback);
    }

    public Instance loadInstance () {
      ConfigNode CnfNode = ConfigNode.Load (saveFilePath);
      if (CnfNode == null) {
        return null;
      } else {
        Instance Inst = new Instance ();
        ConfigNode.LoadObjectFromConfig (Inst, CnfNode);

        for (int i = 0; i < StateFundingGlobal.fetch.Governments.ToArray ().Length; i++) {
          Government Gov = StateFundingGlobal.fetch.Governments.ToArray () [i];
          if (Gov.name == Inst.govName) {
            Inst.Gov = Gov;
          }
        }

        return Inst;
      }
    }

    public bool saveExists () {
      return KSP.IO.File.Exists<String> (saveFilePath);
    }

    public void saveInstance (Instance Inst) {
      ConfigNode CnfNode = ConfigNode.CreateConfigFromObject (Inst);
      CnfNode.Save (saveFilePath);
    }

  }
}

