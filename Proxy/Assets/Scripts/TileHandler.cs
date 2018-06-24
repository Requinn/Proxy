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
    public List<Unit> Column10 = new List<Unit>();//leftmost
    public List<Unit> Column11 = new List<Unit>();
    public List<Unit> Column12 = new List<Unit>();
    public List<Unit> Column13 = new List<Unit>();
    public List<Unit> Column14 = new List<Unit>();
    public List<Unit> Column15 = new List<Unit>();//rightmost

    void Start(){
        Instance = this;

        columns.Add(Column10);
        columns.Add(Column11);
        columns.Add(Column12);
        columns.Add(Column13);
        columns.Add(Column14);
        columns.Add(Column15);
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
        Unit go = columns[column].Single(x => x.ID == ID);
        columns[column].Remove(go);
        NetworkServer.Destroy(go.gameObject);
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

    public void CmdCheckForAttacks(){
        foreach (var c in columns) {
            foreach (var e in c){
                if (Vector3.SqrMagnitude(e.transform.position - transform.position) < e.attack.range * e.attack.range){
                    e.attack.Attack(e, e.Type);
                    e.SetEngaged(true);
                    return;
                }
            }
        }
    }
    
}
