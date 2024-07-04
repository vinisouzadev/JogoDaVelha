using JogoDaVelha.JogadorContexto;

namespace JogoDaVelha.TabuleiroContexto;

public class Tabuleiro
{
    public Tabuleiro()
    {
        for (int i = 1; i <= 9; i++)
        {
            PosicaoNoTabuleiro.Add(new Posicao(i));
        }

        JogadorUm = new(this, "1", "X");

        JogadorDois = new(this, "2", "O");


    }
    
    public List<Posicao> PosicaoNoTabuleiro = [];

    public Jogador JogadorUm { get; set; }

    public Jogador JogadorDois { get; set; }

    public void IniciarJogo()
    {
        var vezJogadorUm = true;

        for (int i = 1; i <= 9; i++)
        {
            DesignTabuleiro();

            if (vezJogadorUm == true)
            {
                JogadorUm.AcaoDoJogador();
                vezJogadorUm = false;
            }
            else
            {
                JogadorDois.AcaoDoJogador();
                vezJogadorUm = true;
            }
        }

        Console.WriteLine("Deu velha!");
        Thread.Sleep(2000);
        ContinuarOuFechar();
    }

    public void DesignTabuleiro()
    {
        Console.Clear();
        Console.WriteLine($"{PosicaoNoTabuleiro[0].DesenhoDaPosicao}   |  {PosicaoNoTabuleiro[1].DesenhoDaPosicao}  |   {PosicaoNoTabuleiro[2].DesenhoDaPosicao}");
        Console.WriteLine("-----------------");
        Console.WriteLine($"{PosicaoNoTabuleiro[3].DesenhoDaPosicao}   |  {PosicaoNoTabuleiro[4].DesenhoDaPosicao}  |   {PosicaoNoTabuleiro[5].DesenhoDaPosicao}");
        Console.WriteLine("-----------------");
        Console.WriteLine($"{PosicaoNoTabuleiro[6].DesenhoDaPosicao}   |  {PosicaoNoTabuleiro[7].DesenhoDaPosicao}  |   {PosicaoNoTabuleiro[8].DesenhoDaPosicao}");

    }

    public List<List<Posicao>> CondicoesVitoria()
    {
        var linhaUm = new List<Posicao>
        {
            PosicaoNoTabuleiro[0],
            PosicaoNoTabuleiro[1],
            PosicaoNoTabuleiro[2]
        };

        var linhaDois = new List<Posicao>
        {
            PosicaoNoTabuleiro[3],
            PosicaoNoTabuleiro[4],
            PosicaoNoTabuleiro[5]
        };

        var linhaTres = new List<Posicao>
        {
            PosicaoNoTabuleiro[6],
            PosicaoNoTabuleiro[7],
            PosicaoNoTabuleiro[8]
        };

        var colunaUm = new List<Posicao>
        {
            PosicaoNoTabuleiro[0],
            PosicaoNoTabuleiro[3],
            PosicaoNoTabuleiro[6]
        };

        var colunaDois = new List<Posicao>
        {
            PosicaoNoTabuleiro[1],
            PosicaoNoTabuleiro[4],
            PosicaoNoTabuleiro[7]
        };

        var colunaTres = new List<Posicao>
        {
            PosicaoNoTabuleiro[2],
            PosicaoNoTabuleiro[5],
            PosicaoNoTabuleiro[8]
        };

        var diagonalUmNove = new List<Posicao>
        {
            PosicaoNoTabuleiro[0],
            PosicaoNoTabuleiro[4],
            PosicaoNoTabuleiro[8]
        };

        var diagonalTresSete = new List<Posicao>
        {
            PosicaoNoTabuleiro[2],
            PosicaoNoTabuleiro[4],
            PosicaoNoTabuleiro[6]
        };

        List<List<Posicao>> PosicoesDeVitoria =
        [
            linhaUm,
            linhaDois,
            linhaTres,
            diagonalUmNove,
            diagonalTresSete,
            colunaUm,
            colunaDois,
            colunaTres
        ];

        return PosicoesDeVitoria;
    }

    public void ContinuarOuFechar()
    {
        while (true)
        {
            try
            {
                Console.Clear();
                ExibirPlacar();
                Console.WriteLine("Deseja jogar mais um?");
                Console.WriteLine("1 - Sim");
                Console.WriteLine("2 - Não");
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        {
                            ZerarTabuleiro();
                            IniciarJogo();
                            break;
                        }
                    case 2: Environment.Exit(0); break;
                    default: Console.WriteLine("Digite uma opção que tenha sido apresentada! "); break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\nDigite uma opção válida!");
                Thread.Sleep(1300);
            }
        }
    }

    public void ZerarTabuleiro()
    {
        foreach (var item in PosicaoNoTabuleiro)
        {
            item.DesenhoDaPosicao = Convert.ToString(item.ID);
            item.JogadorNaPosicao = null;
        }
    }

    public void ExibirPlacar()
    {
        Console.WriteLine($"O placar atual está de (Jogador {JogadorUm.ID}) {JogadorUm.NumeroVitoria} x {JogadorDois.NumeroVitoria} (Jogador {JogadorDois.ID})");
    }

}
