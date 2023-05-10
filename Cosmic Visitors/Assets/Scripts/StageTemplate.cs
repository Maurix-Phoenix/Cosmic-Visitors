using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[CreateAssetMenu(fileName ="New Stage Template", menuName ="Scriptable Objects/Stages/New Stage")]
public class StageTemplate : ScriptableObject
{
    [TextArea(22, 22)]
    public string StageString = "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n" +
                                "-----------------------------\n";


}
