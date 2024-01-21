using System.Collections.Generic;

using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Core;

public abstract class PlayerController
{
    public Player? Player { get; set; }

    /// <summary>
    /// Indique au contrôleur du joueur que c'est à lui de jouer
    /// </summary>
    public abstract void Play(Move? move);

    /// <summary>
    /// Donne au joueur le mouvement à effectuer
    /// </summary>
    /// <param name="move"></param>
    public abstract void Move(Move move);

    /// <summary>
    /// Méthode appelée si le mouvement est invalide
    /// </summary>
    /// <param name="reasonsList">Liste des règles non vérifiées par le mouvement</param>
    public abstract void InvalidMove(List<string> reasonsList);

    /// <summary>
    /// Demande au joueur de retourner la liste des déplacements possibles pour une pièce donnée
    /// </summary>
    /// <param name="piece">Pièce dont on veut connaitre les mouvements possibles</param>
    /// <returns>Liste des cases accessibles par la pièce passé en paramètre</returns>
    public abstract List<Square> PossibleMoves(BasePiece piece);

    public abstract void Stop();
}
