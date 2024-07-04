using JogoDaVelha.JogadorContexto;

namespace JogoDaVelha.TabuleiroContexto;

public class Posicao(int id)
{
    public int ID = id;

    public string DesenhoDaPosicao = Convert.ToString(id);

    public Jogador JogadorNaPosicao = null;
}