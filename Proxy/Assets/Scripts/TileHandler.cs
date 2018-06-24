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
    private readonly List<List<Unit>> P1columns = new List<List<Unit>>();
    public List<Unit> Column10 = new List<Unit>();//leftmost
    public List<Unit> Column11 = new List<Unit>();
    public List<Unit> Column12 = new List<Unit>();
    public List<Unit> Column13 = new List<Unit>();
    public List<Unit> Column14 = new List<Unit>();
    public List<Unit> Column15 = new List<Unit>();//rightmost

    private readonly List<List<Unit>> P2columns = new List<List<Unit>>();
    public List<Unit> Column20 = new List<Unit>();//leftmost
    public List<Unit> Column21 = new List<Unit>();
    public List<Unit> Column22 = new List<Unit>();
    public List<Unit> Column23 = new List<Unit>();
    public List<Unit> Column24 = new List<Unit>();
    public List<Unit> Column25 = new List<Unit>();//rightmost

    void Start(){
        Instance = this;

        P1columns.Add(Column10);
        P1columns.Add(Column11);
        P1columns.Add(Column12);
        P1columns.Add(Column13);
        P1columns.Add(Column14);
        P1columns.Add(Column15);

        P2columns.Add(Column20);
        P2columns.Add(Column21);
        P2columns.Add(Column22);
        P2columns.Add(Column23);
        P2columns.Add(Column24);
        P2columns.Add(Column25);
    }

    [Command]
    public void CmdCreateAndAddToColumn(int column, Unit.UnitType type, Vector3 pos, Quaternion rot){
        GameObject go = Instantiate(availableUnits.Single(u => u.Type == type).gameObject, pos, rot);
        NetworkServer.Spawn(go);
        P1columns.ElementAt(column).Add(go.GetComponent<Unit>());
    }

    /// <summary>
    /// add an object to a column
    /// </summary>
    /// <param name="go"></param>
    /// <param name="column"></param>
    public void AddToColumn(Unit go, int column){
        P1columns[column].Add(go);
    }

    /// <summary>
    /// remove an object from the column
    /// </summary>
    /// <param name="go"></param>
    /// <param name="column"></param>
    [Command]
    public void CmdRemoveFromColumn(int ID, int column){
        P1columns[column].Remove(P1columns[column].Single(x => x.ID == ID));
    }

    /// <summary>
    /// fetch all enemies in the same column
    /// </summary>
    /// <param name="index"></param>
    /// <param name="requestingPlayer"></param>
    /// <returns></returns>
    public List<Unit> FetchAllEnemiesFromColumn(int index, int requestingPlayer){
        return P1columns[index].Where(x => x.controlledPlayer == -requestingPlayer).ToList();
    }

    public void CmdCheckForAttacks(){
        foreach (var c in P1columns) {
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
