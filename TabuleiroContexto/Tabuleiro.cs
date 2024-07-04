using JogoDaVelha.JogadorContexto;

namespace JogoDaVelha.TabuleiroContexto;

public class Tabuleiro
{   

    // Nessa classe é onde recebemos as propriedades e métodos relacionados ao jogo em sí e ao tabuleiro do jogo.
    public Tabuleiro()
    {
        for (int i = 1; i <= 9; i++)
        {
            PosicaoNoTabuleiro.Add(new Posicao(i));
        }

        JogadorUm = new(this, "1", "X");

        JogadorDois = new(this, "2", "O");
    }
    
    // Instancia uma lista de posições no tabuleiro que terá valores adicionados no metodo contrutor da classe Tabuleiro
    public List<Posicao> PosicaoNoTabuleiro = [];


    //_________JOGADORES____________________
    public Jogador JogadorUm { get; set; }

    public Jogador JogadorDois { get; set; }
    //______________________________________

    // Inicia o jogo criando uma variável para a vez do jogador um. Em seguida, realiza um loop no máximo nove veses que é o número máximo de turnos possíveis
    //no jogo e caso não tenha um vencedor até o fim do loop, o jogo empata e pergunta se o jogador quer continuar em outra partida ou fechar o programa.
    // A cada turno o é verificado se é a vez do jogadorUm e se não for, automaticamente é a vez do jogador dois e em ambos os casos, é chamado a ação do jogador
    //e então a vezJogadorUm é invertida novamente gerando um ciclo de troca de jogador a cada turno.
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

    // Exibe o tabuleiro do jogo através de uma interpolação mostrando o desenho de cada posição na lista de posições criada.
    public void DesignTabuleiro()
    {
        Console.Clear();
        Console.WriteLine($"{PosicaoNoTabuleiro[0].DesenhoDaPosicao}   |  {PosicaoNoTabuleiro[1].DesenhoDaPosicao}  |   {PosicaoNoTabuleiro[2].DesenhoDaPosicao}");
        Console.WriteLine("-----------------");
        Console.WriteLine($"{PosicaoNoTabuleiro[3].DesenhoDaPosicao}   |  {PosicaoNoTabuleiro[4].DesenhoDaPosicao}  |   {PosicaoNoTabuleiro[5].DesenhoDaPosicao}");
        Console.WriteLine("-----------------");
        Console.WriteLine($"{PosicaoNoTabuleiro[6].DesenhoDaPosicao}   |  {PosicaoNoTabuleiro[7].DesenhoDaPosicao}  |   {PosicaoNoTabuleiro[8].DesenhoDaPosicao}");

    }

    // Existem 8 condições de vitória. É criado uma lista contendo as posições que devem estar preenchidas para que determinada condição aconteça
    //como na linhaUm que possui todas as posições da primeira linha.
    // Em seguida, é criado uma lista de lista de posições que recebe cada uma dessas condições de vitória.
    // Usaremos isso para iterar essa lista de condições e verificar se nosso jogador preencheu totalmente alguma dessas condições.
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

    // Possui um loop com um trycatch para garantirmos que o usuário preencherá as informações corretamente.
    // Inicia exibindo o placar do jogo e então, caso o usuário decida continuar, ele primeiro zera o tabuleiro do jogo e então Inicia um novo jogo.
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

    // Zera o tabuleiro iterando cada posição da lista de posições e devolvendo as propriedades para o estado original.
    public void ZerarTabuleiro()
    {
        foreach (var item in PosicaoNoTabuleiro)
        {
            item.DesenhoDaPosicao = Convert.ToString(item.ID);
            item.JogadorNaPosicao = null;
        }
    }

    // Exibe o placar do tabuleiro interpolando o ID e o numero de vitorias daquele jogador
    public void ExibirPlacar()
    {
        Console.WriteLine($"O placar atual está de (Jogador {JogadorUm.ID}) {JogadorUm.NumeroVitoria} x {JogadorDois.NumeroVitoria} (Jogador {JogadorDois.ID})");
    }

}
