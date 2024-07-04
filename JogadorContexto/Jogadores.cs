using JogoDaVelha.TabuleiroContexto;

namespace JogoDaVelha.JogadorContexto;

// Classe para representar cada jogador. Recebe o tabuleiro do jogo como parametro para que possamos acessar suas propriedades e metodos.
// Exige também a especificação do ID e do desenho associado ao jogador
public class Jogador(Tabuleiro tabuleiro, string id, string desenho)
{
    public string ID = id;

    public string Desenho = desenho;

    // Numero de vitorias desse jogador ao longo do programa
    public int NumeroVitoria = 0;

    // Encapsula todos os metodos referentes ao turno do jogador
    public void AcaoDoJogador()
    {   
        Posicao jogada = FazerJogada();

        ModificarPosicao(jogada);

        if (VerificarSeGanhou())
        {
            NumeroVitoria++;
            ExibirGanhador();
            tabuleiro.ContinuarOuFechar();
        }

    }

    // Deve retornar uma posição, que posteriormente no método AcaoDoJogador, será associado a sua jogada. 
    // Recebe um inteiro referente a escolha da posição do tabuleiro que o jogador quer preencher (que é exibido como numeros inteiros) e então é criado uma variável que será
    //a sua jogada. Essa varável filtra entre a lista de posições do tabuleiro, todas as pocições que não foram preenchidos pelo jogador e então seleciona a posição
    //que possui o mesmo ID que a jogada inputada pelo usuário.
    // Caso o First() não encontre uma posição com o mesmo ID que a jogada do usuário, o programa irá disparar um erro que será tratado no TryCatch e então obrigará o usuário a
    //preencher o campo novamente. O Programa então retorna a jogada escolhida pelo usuário.
    public Posicao FazerJogada()
    {
        Posicao jogadaEscolhida = null;
        while (true)
        {
            try
            {
                Console.WriteLine($"\nVez do jogador {ID}.");

                Console.WriteLine("Qual será sua escolha?");
                int jogada = int.Parse(Console.ReadLine());

                jogadaEscolhida = tabuleiro.PosicaoNoTabuleiro.Where(posicaoDisponivel => posicaoDisponivel.JogadorNaPosicao == null).First(posicaoDisp => posicaoDisp.ID == jogada);

                break;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("\nDigite uma jogada que seja válida!");
                Thread.Sleep(1300);
                tabuleiro.DesignTabuleiro();
            }
            catch (Exception)
            {
                Console.WriteLine("\nDigite uma jogada válida!");
                Thread.Sleep(1300);
                tabuleiro.DesignTabuleiro();
            }
        }

        return jogadaEscolhida;
    }

    // Seleciona a posição que o usuário escolheu e modifica suas propriedades, associando seu DesenhoDaPosicao ao desenho associado a este jogador e associa este jogador a esta posição
    public void ModificarPosicao(Posicao jogadaEscolhida)
    {
        jogadaEscolhida.DesenhoDaPosicao = Desenho;
        jogadaEscolhida.JogadorNaPosicao = this;
    }

    
    // Retorna um booleano para saber se o jogador ganhou ou não.
    // Inicia iterando cada condição dentro da lista de condições de vitória e então inicia um contador em 0. Em seguida, ele itera na condição de vitória cada posição
    //e verifica se este jogador está atribuido a essa posição, se sim, ele incrementa o contador em 1. Se após esse incremento o contador estiver em 3 (que é o número máximo de posições
    //em uma linha ou coluna) ele declara que o jogadorGanhou = true, ou seja, o jogador atual ganhou a partida.
    public bool VerificarSeGanhou()
    {
        bool jogadorGanhou = false;
        

        foreach (var condicao in tabuleiro.CondicoesVitoria())
        {
            int contadorDeCondicaoAtendida = 0;
            foreach (var posicaoJogo in condicao)
            {
                if (posicaoJogo.JogadorNaPosicao == this)
                {
                    contadorDeCondicaoAtendida++;
                    if (contadorDeCondicaoAtendida == 3)
                    {
                        jogadorGanhou = true;
                    }
                }
            }
        }

        return jogadorGanhou;
    }


    public void ExibirGanhador()
    {
        tabuleiro.DesignTabuleiro();
        Console.WriteLine($"\nJogador {ID} ganhou! Parabéns!");
        Thread.Sleep(2000);
    }
}