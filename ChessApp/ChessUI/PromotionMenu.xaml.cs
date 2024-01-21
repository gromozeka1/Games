using System.Windows.Controls;
using System.Windows.Input;

using ChessLogic;

namespace ChessUI;
/// <summary>
/// Interaction logic for PromotionMenu.xaml
/// </summary>
public partial class PromotionMenu : UserControl
{
    public event Action<PieceType> PieceSelected;
    public PromotionMenu(Player player)
    {
        InitializeComponent();

        QueenImage.Source = Images.GetImage(player, PieceType.Queen);
        BishopImage.Source = Images.GetImage(player, PieceType.Bishop);
        RookImage.Source = Images.GetImage(player, PieceType.Rook);
        KnightImage.Source = Images.GetImage(player, PieceType.Knight);
    }

    private void QueenImg_MouseDown(object sender, MouseButtonEventArgs e)
    {
        PieceSelected?.Invoke(PieceType.Queen);
    }

    private void BishopImg_MouseDown(object sender, MouseButtonEventArgs e)
    {
        PieceSelected?.Invoke(PieceType.Bishop);
    }

    private void RookImg_MouseDown(object sender, MouseButtonEventArgs e)
    {
        PieceSelected?.Invoke(PieceType.Rook);
    }

    private void KnightImg_MouseDown(object sender, MouseButtonEventArgs e)
    {
        PieceSelected?.Invoke(PieceType.Knight);
    }
}
