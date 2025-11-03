using AwesomeAssertions;

namespace GameOfLife.V1.Tests;

public class GameOfLifeTests
{
    [Fact]
    public void DadaCelulaVivaSinVecinas_CuandoAvanzaUnaGeneracion_EntoncesMuere()
    {
        //Arrange
        bool[,] tableroSemilla = new bool[4, 4];
        tableroSemilla[2, 2] = true;
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        juego.NextGen();

        //Assert
        juego.EstaCelulaViva(2, 2).Should().BeFalse();
    }
}

public class JuegoDeLaVida(bool[,] tablero)
{
    public void NextGen()
    {
        throw new NotImplementedException();
    }

    public bool EstaCelulaViva(int posicionX, object posicionY)
    {
        throw new NotImplementedException();
    }
}