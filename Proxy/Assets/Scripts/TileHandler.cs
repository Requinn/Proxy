using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Networking;

public class TileHandler : NetworkBehaviour {
    public static TileHandler Instance;

    public List<Unit> availableUnits = new List<Unit>();
    public List<GameObject> Tiles;
    private readonly List<List<Unit>> columns = new List<List<Unit>>();
    public List<Unit> Column0 = new List<Unit>();//leftmost
    public List<Unit> Column1 = new List<Unit>();
    public List<Unit> Column2 = new List<Unit>();
    public List<Unit> Column3 = new List<Unit>();
    public List<Unit> Column4 = new List<Unit>();
    public List<Unit> Column5 = new List<Unit>();//rightmost

    void Start(){
        Instance = this;

        columns.Add(Column0);
        columns.Add(Column1);
        columns.Add(Column2);
        columns.Add(Column3);
        columns.Add(Column4);
        columns.Add(Column5);
    }

    [Command]
    public void CmdCreateAndAddToColumn(int column, Unit.UnitType type, Vector3 pos, Quaternion rot){
        GameObject go = Instantiate(availableUnits.Single(u => u.Type == type).gameObject, pos, rot);
        NetworkServer.Spawn(go);
        columns.ElementAt(column).Add(go.GetComponent<Unit>());
    }

    /// <summary>
    /// add an object to a column
    /// </summary>
    /// <param name="go"></param>
    /// <param name="column"></param>
    public void AddToColumn(Unit go, int column){
        columns[column].Add(go);
    }

    /// <summary>
    /// remove an object from the column
    /// </summary>
    /// <param name="go"></param>
    /// <param name="column"></param>
    [Command]
    public void CmdRemoveFromColumn(int ID, int column){
        columns[column].Remove(columns[column].Single(x => x.ID == ID));
    }

    /// <summary>
    /// fetch all enemies in the same column
    /// </summary>
    /// <param name="index"></param>
    /// <param name="requestingPlayer"></param>
    /// <returns></returns>
    public List<Unit> FetchAllEnemiesFromColumn(int index, int requestingPlayer){
        return columns[index].Where(x => x.controlledPlayer == -requestingPlayer).ToList();
    }

    [Command]
    public void CmdCheckForAttacks(){
        foreach (var c in columns){
            foreach (var e in c){
                if (Vector3.SqrMagnitude(e.transform.position - transform.position) < e.attack.range * e.attack.range){
                    e.attack.RpcAttack(e, e.Type);
                    e.SetEngaged(true);
                    return;
                }
            }
        }
    }
    
}
