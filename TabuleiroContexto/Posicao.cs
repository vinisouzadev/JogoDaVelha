using JogoDaVelha.JogadorContexto;

namespace JogoDaVelha.TabuleiroContexto;


// Classe especifica para cada posição no tabuleiro, que irá ter um ID para verificação se o ID é o mesmo da posição escolhida pelo usuário
//Possui também uma propriedade para exibir os desenhos no tabuleiro e uma propriedade que atribui a posição a um jogador
public class Posicao(int id)
{
    public int ID = id;

    public string DesenhoDaPosicao = Convert.ToString(id);

    public Jogador JogadorNaPosicao = null;
}