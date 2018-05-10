﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour {

    public int currentTurn;
    public enum TurnPhase { Choice, Solve }
    public TurnPhase phase;
    public int playerTurn;

    public MatchController matchController;
    public Map map;
    public TurnSolver solver;

    public Player player {
        get { return players[playerTurn]; }
    }

    public List<Player> players = new List<Player>();
    public List<Unit> teamA = new List<Unit>();
    public List<Unit> teamB = new List<Unit>();
    public List<Unit> neutrals = new List<Unit>();

    public static Color colorTeamA = new Color(0, 0.8f, 0.2f);
    public static Color colorTeamB = new Color(0.6f, 0, 0.6f);

    public static Match PrepareMatch(string mapPath) {
        Match output = new GameObject("Match").AddComponent<Match>();
        output.map = new GameObject("Map").AddComponent<Map>();
        output.map.transform.SetParent(output.transform);
        // build map
        MapBuilder builder = new MapBuilder();
        builder.map = output.map;
        builder.cellPrefab = Resources.Load<GameObject>("Prefabs/Cell");
        builder.playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        builder.GenerateMap(mapPath); // todo : fill in cell prefab
        // build players
        Player p1 = Instantiate(builder.playerPrefab, output.transform).GetComponent<Player>();
        p1.name = "Player 0";
        p1.team = Team.TeamA;
        p1.teamColor = colorTeamA;
        output.teamA.Add(p1);
        output.players.Add(p1);
        Player p2 = Instantiate(builder.playerPrefab, output.transform).GetComponent<Player>();
        p2.name = "Player 1";
        p2.team = Team.TeamB;
        p2.teamColor = colorTeamB;
        output.teamB.Add(p2);
        output.players.Add(p2);
        // put players on map
        Cell cell1 = output.map.startingACells[0]; // player 1 start pos
        Map.AddUnitToCell(p1, cell1);
        p1.transform.position = cell1.transform.position + new Vector3(0, .5f, 0);
        Cell cell2 = output.map.startingBCells[0]; // player 2 start pos
        Map.AddUnitToCell(p2, cell2);
        p2.transform.position = cell2.transform.position + new Vector3(0, .5f, 0);
        // other stuff
        output.solver = new TurnSolver();
        output.solver.match = output;
        output.solver.map = output.map;
        return output;
    }

    public void StartMatch() {
        //Init match turn
        currentTurn = 0;
        phase = TurnPhase.Choice;
        playerTurn = players.Count - 1;
        StartNewTurn();
    }

    private void StartNewTurn() {
        playerTurn = (playerTurn + 1) % players.Count;
        if (playerTurn == 0) { // heavy turn
            currentTurn++;
            foreach (Player p in players) {
                if (currentTurn % 2 == 0)
                    p.RegenMP(2);
                p.UpdateCooldown();
                // pseudo IA
                if (matchController.toggleAI.isOn) {
                    int move = Random.Range(0, 4 - 1);
                    int x = move == 1 ? -1 : move == 3 ? 1 : 0;
                    int y = move == 0 ? -1 : move == 2 ? 1 : 0;
                    Cell dest = map.GetCell(p.currentCell.x + x, p.currentCell.y + y);
                    if (dest != null && dest.type == Cell.CellType.NORMAL && dest)
                        p.currentAction.move.Add(dest);
                    else
                        dest = p.currentCell;
                    PlayerSpell s = p.spells[currentTurn % 4];
                    List<Cell> range = map.GetCellsArea(dest, currentTurn % 2 == 0 ? s.spell.rangeHeavy : s.spell.rangeLight);
                    if (range.Count > 0) {
                        p.currentAction.spell.spell = s;
                        p.currentAction.spell.target = range[Random.Range(0, range.Count - 1)];
                    }
                }
            }
        }
        matchController.OnTurnStart(currentTurn, 15, playerTurn);
    }

    public void EndTurn() {
        EndCurrentPlayerTurn();
    }

    public void EndCurrentPlayerTurn() {
        matchController.OnTurnEnd();
        Player currentPlayer = players[playerTurn];
        if (playerTurn < (players.Count - 1)) {
            StartNewTurn();
        } else {
            StartSolvePhase();
        }
    }

    private void StartSolvePhase() {
        StartCoroutine(DoSolvePhase());
    }

    private IEnumerator DoSolvePhase() {
        phase = TurnPhase.Solve;
        StartCoroutine(solver.DoSolveMovements());
        yield return new WaitWhile(delegate { return solver.isSolvingMovements; });
        StartCoroutine(solver.DoSolveSpells());
        yield return new WaitWhile(delegate { return solver.isSolvingSpells; });
        EndSolvePhase();
    }

    private void EndSolvePhase() {
        foreach (Player p in players) {
            p.ClearTurnAction();
        }
        phase = TurnPhase.Choice;

        StartNewTurn();
    }

    public Player CurrentPlayer() {
        return players[playerTurn];
    }
}
