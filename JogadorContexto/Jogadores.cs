using JogoDaVelha.TabuleiroContexto;

namespace JogoDaVelha.JogadorContexto;

public class Jogador(Tabuleiro tabuleiro, string id, string desenho)
{
    public string ID = id;

    public string Desenho = desenho;

    public int NumeroVitoria = 0;

    public void AcaoDoJogador()
    {
        Posicao jogada = FazerJogada(tabuleiro);

        ModificarPosicao(jogada);

        if (VerificarSeGanhou())
        {
            NumeroVitoria++;
            ExibirGanhador();
            tabuleiro.ContinuarOuFechar();
        }

    }

    public Posicao FazerJogada(Tabuleiro tabuleiro)
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

    public void ModificarPosicao(Posicao jogadaEscolhida)
    {
        jogadaEscolhida.DesenhoDaPosicao = Desenho;
        jogadaEscolhida.JogadorNaPosicao = this;
    }

    public bool VerificarSeGanhou()
    {
        var condicoes = tabuleiro.CondicoesVitoria();

        bool ganhou = false;
        int ganhar = 0;

        foreach (var item in condicoes)
        {
            ganhar = 0;
            foreach (var posicaoJogo in item)
            {
                if (posicaoJogo.JogadorNaPosicao == this)
                {
                    ganhar++;
                    if (ganhar == 3)
                    {
                        ganhou = true;
                    }
                }
            }
        }

        return ganhou;
    }

    public void ExibirGanhador()
    {
        tabuleiro.DesignTabuleiro();
        Console.WriteLine($"\nJogador {ID} ganhou! Parabéns!");
        Thread.Sleep(2000);
    }
}