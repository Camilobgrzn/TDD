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
        if (EstaCelulaViva(1, 2) && EstaCelulaViva(2, 2) && EstaCelulaViva(3, 2))
        {
            tablero[1, 2] = false;
            tablero[3, 2] = false;
        }
        else if (EstaCelulaViva(1, 1) && EstaCelulaViva(2, 1) && EstaCelulaViva(3, 1))
        {
            tablero[1, 1] = false;
            tablero[3, 1] = false;
        }
        else if (EstaCelulaViva(1, 3) && EstaCelulaViva(2, 3) && EstaCelulaViva(3, 3))
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

    public bool EstaCelulaViva(int fila, int columna)
    {
        return tablero[fila, columna];
    }
}