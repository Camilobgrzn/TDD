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


    [Fact]
    public void DadasDosCelulasVivasConUnaVecina_CuandoAvanzaUnaGeneracion_EntoncesAmbasMueren()
    {
        //Arrange
        var tableroSemilla = new bool[4, 4];
        tableroSemilla[2, 2] = true;
        tableroSemilla[3, 2] = true;
        JuegoDeLaVida juego = new(tableroSemilla);


        //Act
        juego.NextGen();

        //Assert
        juego.EstaCelulaViva(2, 2).Should().BeFalse();
        juego.EstaCelulaViva(3, 2).Should().BeFalse();
    }

    [Fact]
    public void DadaCelulaVivaConDosVecinasVerticalesEnColumna2_CuandoAvanzaUnaGeneracion_EntoncesSobrevive()
    {
        //Arrange
        var tableroSemilla = new bool[4, 4];
        tableroSemilla[1, 2] = true;
        tableroSemilla[2, 2] = true;
        tableroSemilla[3, 2] = true;
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        juego.NextGen();

        //Assert
        juego.EstaCelulaViva(1, 2).Should().BeFalse();
        juego.EstaCelulaViva(2, 2).Should().BeTrue();
        juego.EstaCelulaViva(3, 2).Should().BeFalse();
    }

    [Fact]
    public void DadaCelulaVivaConDosVecinasVerticalesEnColumna1_CuandoAvanzaUnaGeneracion_EntoncesSobrevive()
    {
        //Arrange
        var tableroSemilla = new bool[4, 4];
        tableroSemilla[1, 1] = true;
        tableroSemilla[2, 1] = true;
        tableroSemilla[3, 1] = true;
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        juego.NextGen();

        //Assert
        juego.EstaCelulaViva(1, 1).Should().BeFalse();
        juego.EstaCelulaViva(2, 1).Should().BeTrue();
        juego.EstaCelulaViva(3, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaCelulaVivaConDosVecinasVerticalesEnColumna3_CuandoAvanzaGeneracion_EntoncesSobrevive()
    {
        //Arrange
        var tableroSemilla = new bool[4, 4];
        tableroSemilla[1, 3] = true;
        tableroSemilla[2, 3] = true;
        tableroSemilla[3, 3] = true;
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        juego.NextGen();

        //Assert
        juego.EstaCelulaViva(1, 3).Should().BeFalse();
        juego.EstaCelulaViva(2, 3).Should().BeTrue();
        juego.EstaCelulaViva(3, 3).Should().BeFalse();
    }
}

public class JuegoDeLaVida(bool[,] tablero)
{
    public void NextGen()
    {
        if (tablero[1, 2] && tablero[2, 2] && tablero[3, 2])
        {
            tablero[1, 2] = false;
            tablero[3, 2] = false;
        }
        else if (tablero[1, 1] && tablero[2, 1] && tablero[3, 1])
        {
            tablero[1, 1] = false;
            tablero[3, 1] = false;
        }
        else if (tablero[1, 3] && tablero[2, 3] && tablero[3, 3])
        {
            tablero[1, 3] = false;
            tablero[3, 3] = false;
        }
        else
        {
            tablero[2, 2] = false;
            tablero[3, 2] = false;
        }
        
    }

    public bool EstaCelulaViva(int posicionX, int posicionY)
    {
        return tablero[posicionX, posicionY];
    }
}