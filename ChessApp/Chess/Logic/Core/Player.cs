using System.Collections.Generic;

using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Core;

public class Player
{
    public FigureColor Color { get; internal set; }

    private PlayerController _playerControler;

    public Game? Game { get; set; }

    public Player(FigureColor color, PlayerController playerControler)
    {
        Color = color;
        _playerControler = playerControler;
    }

    /// <summary>
    /// Notifie le joueur que c'est à son tour de jouer et que le Game peut recevoir un mouvement de sa part.
    /// Tant que ce mouvement n'est pas valide, cette méthode est appelée.
    /// </summary>
    public void Play(Move? move) => _playerControler.Play(move);

    public void Stop() => _playerControler.Stop();

    public List<Square> PossibleMoves(BasePiece piece) => Game.PossibleMoves(piece);

    public void Move(Move move) => MoveDone?.Invoke(this, move);

    public void LeaveGame(string reason) => Game.PlayerLeave(this, reason);

    public delegate void MoveHandler(Player sender, Move move);
    public event MoveHandler? MoveDone;
}
