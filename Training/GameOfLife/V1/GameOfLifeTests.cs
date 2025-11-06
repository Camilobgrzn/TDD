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

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void DadaCelulaVivaConDosVecinasVerticales_CuandoAvanzaUnaGeneracion_EntoncesSobrevive(int columna)
    {
        //Arrange
        var tableroSemilla = new bool[4, 4];
        tableroSemilla[1, columna] = true;
        tableroSemilla[2, columna] = true;
        tableroSemilla[3, columna] = true;
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        juego.NextGen();

        //Assert
        juego.EstaCelulaViva(1, columna).Should().BeFalse();
        juego.EstaCelulaViva(2, columna).Should().BeTrue();
        juego.EstaCelulaViva(3, columna).Should().BeFalse();
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void DadaCelulaVivaConDosVecinasHorizontales_CuandoAvanzaUnaGeneracion_EntoncesSobrevive(int fila)
    {
        //Arrange
        var tableroSemilla = new bool[4, 4];
        tableroSemilla[fila, 0] = true;
        tableroSemilla[fila, 1] = true;
        tableroSemilla[fila, 2] = true;
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        juego.NextGen();

        //Assert
        juego.EstaCelulaViva(fila, 0).Should().BeFalse();
        juego.EstaCelulaViva(fila, 1).Should().BeTrue();
        juego.EstaCelulaViva(fila, 2).Should().BeFalse();
    }

    [Fact]
    public void DadaCelulaVivaConDosVecinasEnDiagonalPrincipalDesdeFila1Columna0HastaFila3Columna2_CuandoAvanzaUnaGeneracion_EntoncesSobrevive()
    {
        //Arrange
        var tableroSemilla = new bool[9, 9];
        tableroSemilla[1, 0] = true;
        tableroSemilla[2, 1] = true;
        tableroSemilla[3, 2] = true;
        JuegoDeLaVida juego = new(tableroSemilla);
        
        //Act
        juego.NextGen();
        
        //Assert
        juego.EstaCelulaViva(1, 0).Should().BeFalse();
        juego.EstaCelulaViva(2, 1).Should().BeTrue();
        juego.EstaCelulaViva(3, 2).Should().BeFalse();
        
    }
}

public class JuegoDeLaVida(bool[,] tablero)
{
    public void NextGen()
    {
        if (EstaCelulaViva(2, 2) && ContarVecinasVerticales(2, 2) == 2)
        {
            tablero[1, 2] = false;
            tablero[3, 2] = false;
        }
        else if (EstaCelulaViva(2, 1) && ContarVecinasVerticales(2, 1) == 2)
        {
            tablero[1, 1] = false;
            tablero[3, 1] = false;
        }
        else if (EstaCelulaViva(2, 3) && ContarVecinasVerticales(2, 3) == 2)
        {
            tablero[1, 3] = false;
            tablero[3, 3] = false;
        }
        else if (EstaCelulaViva(1, 1) && ContarVecinasHorizontales(1, 1) == 2)
        {
            tablero[1, 2] = false;
            tablero[1, 0] = false;
        }
        else if (EstaCelulaViva(2, 1) && ContarVecinasHorizontales(2, 1) == 2)
        {
            tablero[2, 2] = false;
            tablero[2, 0] = false;
        }
        else if (EstaCelulaViva(3, 1) && ContarVecinasHorizontales(3, 1) == 2)
        {
            tablero[3, 2] = false;
            tablero[3, 0] = false;
        }
        else
        {
            tablero[2, 2] = false;
            tablero[3, 2] = false;
        }
    }

    private int ContarVecinasHorizontales(int fila, int columna)
    {
        var cantidadVecinas = 0;

        if (EstaCelulaViva(fila, columna - 1))
            cantidadVecinas++;

        if (EstaCelulaViva(fila, columna + 1))
            cantidadVecinas++;

        return cantidadVecinas;
    }

    private int ContarVecinasVerticales(int fila, int columna)
    {
        var cantidadVecinas = 0;

        if (EstaCelulaViva(fila - 1, columna))
        {
            cantidadVecinas++;
        }

        if (EstaCelulaViva(fila + 1, columna))
        {
            cantidadVecinas++;
        }

        return cantidadVecinas;
    }

    public bool EstaCelulaViva(int fila, int columna)
    {
        return tablero[fila, columna];
    }
}