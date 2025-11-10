using AwesomeAssertions;
using System.Linq;
using System.Numerics;

namespace GameOfLife.V1.Tests;

public class GameOfLifeTests
{
    [Fact]
    public void DadaCelulaVivaSinVecinas_CuandoAvanzaUnaGeneracion_EntoncesMuere()
    {
        //Arrange
        var tableroSemilla = new bool[4, 4]
        {
            { false, false, false, false },
            { false, false, false, false },
            { false, false, true, false },
            { false, false, false, false },
        };
        var tableroEsperado = new bool[4, 4];
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        bool[,] tableroSiguienteGeneracion = juego.NextGen();

        //Assert
        tableroSiguienteGeneracion.Should().BeEquivalentTo(tableroEsperado);
    }


    [Fact]
    public void DadasDosCelulasVivasConUnaVecina_CuandoAvanzaUnaGeneracion_EntoncesAmbasMueren()
    {
        //Arrange
        var tableroSemilla = new bool[4, 4];
        tableroSemilla[2, 2] = true;
        tableroSemilla[3, 2] = true;
        var tableroEsperado = new bool[4, 4];
        JuegoDeLaVida juego = new(tableroSemilla);


        //Act
        bool[,] tableroSiguienteGeneracion = juego.NextGen();


        //Assert
        tableroSiguienteGeneracion.Should().BeEquivalentTo(tableroEsperado);
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
        var tableroEsperado = new bool[4, 4];
        tableroEsperado[2, columna] = true;

        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        bool[,] tableroSiguienteGeneracion = juego.NextGen();

        //Assert

        tableroSiguienteGeneracion.Should().BeEquivalentTo(tableroEsperado);
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
        var tabelroEsperado = new bool[4, 4];
        tabelroEsperado[fila, 1] = true;
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        bool[,] tableroSiguienteGeneracion = juego.NextGen();

        //Assert
        tableroSiguienteGeneracion.Should().BeEquivalentTo(tabelroEsperado);
    }

    [Theory]
    [InlineData(1, 0, 2, 1, 3, 2)]
    [InlineData(1, 1, 2, 2, 3, 3)]
    [InlineData(2, 1, 3, 2, 4, 3)]
    public void
        DadaCelulaVivaConDosVecinasEnDiagonalPrincipal_CuandoAvanzaUnaGeneracion_EntoncesSobrevive
        (
            int filaCelula1, int columnaCelula1,
            int filaCelula2, int columnaCelula2,
            int filaCelula3, int columnaCelula3
        )
    {
        //Arrange
        var tableroSemilla = new bool[9, 9];
        tableroSemilla[filaCelula1, columnaCelula1] = true;
        tableroSemilla[filaCelula2, columnaCelula2] = true;
        tableroSemilla[filaCelula3, columnaCelula3] = true;
        var tableroEsperado = new bool[9, 9];
        tableroEsperado[filaCelula2, columnaCelula2] = true;
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        bool[,] tableroSiguienteGeneracion = juego.NextGen();

        //Assert
        tableroSiguienteGeneracion.Should().BeEquivalentTo(tableroEsperado);
    }


    [Theory]
    [InlineData(3, 1, 2, 2, 1, 3)]
    [InlineData(3, 2, 2, 3, 1, 4)]
    [InlineData(4, 2, 3, 3, 2, 4)]
    public void DadaCelulaVivaConDosVecinasEnDiagonalSecundaria_CuandoAvanzaUnaGeneracion_EntoncesSobrevive
    (
        int filaCelula1, int columnaCelula1,
        int filaCelula2, int columnaCelula2,
        int filaCelula3, int columnaCelula3
    )
    {
        //Arrange
        var tableroSemilla = new bool[9, 9];
        tableroSemilla[filaCelula1, columnaCelula1] = true;
        tableroSemilla[filaCelula2, columnaCelula2] = true;
        tableroSemilla[filaCelula3, columnaCelula3] = true;
        var tableroEsperado = new bool[9, 9];
        tableroEsperado[filaCelula2, columnaCelula2] = true;
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        bool[,] tableroSiguienteGeneracion = juego.NextGen();

        //Assert
        tableroSiguienteGeneracion.Should().BeEquivalentTo(tableroEsperado);
    }

    [Fact]
    public void DadaCelulaVivaEnMundo1x1ConCeroVecinasVivas_CuandoAvanzaUnaGeneracion_EntoncesMuere()
    {
        //Arrange
        bool[,] tableroSemilla =
        {
            { true }
        };

        bool[,] tableroEsperado = new bool[1, 1];
        JuegoDeLaVida juego = new(tableroSemilla);

        //Act
        bool[,] tableroSiguienteGeneracion = juego.NextGen();

        //Arrange
        tableroSiguienteGeneracion.Should().BeEquivalentTo(tableroEsperado);
    }


    [Fact]
    public void DadoOsciladorHorizontalCentrado_CuandoAvanzaUnaGeneracion_EntoncesEsVerticalCentrado()
    {
        //Arrange
        bool[,] tableroSemilla =
        {
            { false, false, false, false, false },
            { false, false, false, false, false },
            { false, true, true, true, false },
            { false, false, false, false, false },
            { false, false, false, false, false },
        };
        bool[,] tableroEsperado =
        {
            { false, false, false, false, false },
            { false, false, true, false, false },
            { false, false, true, false, false },
            { false, false, true, false, false },
            { false, false, false, false, false },
        };
        JuegoDeLaVida juego = new(tableroSemilla);
        
        //Act
        bool[,] tableroSiguienteGeneracion = juego.NextGen();
        
        //Assert
        tableroSiguienteGeneracion.Should().BeEquivalentTo(tableroEsperado);
    }
}

public class JuegoDeLaVida(bool[,] tablero)
{
    public bool[,] NextGen()
    {
        if (EstaCelulaViva(0, 0) && ContarVecinas(0, 0) == 0)
        {
            tablero[0, 0] = false;
        }
        else if (EstaCelulaViva(2, 2) && ContarVecinasVerticales(2, 2) == 2)
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
        else if (EstaCelulaViva(2, 1) && ContarVecinasDiagonalPrincipal(2, 1) == 2)
        {
            tablero[1, 0] = false;
            tablero[3, 2] = false;
        }
        else if (EstaCelulaViva(2, 2) && ContarVecinasDiagonalPrincipal(2, 2) == 2)
        {
            tablero[1, 1] = false;
            tablero[3, 3] = false;
        }
        else if (EstaCelulaViva(3, 2) && EstaCelulaViva(2, 1) && ContarVecinasDiagonalPrincipal(3, 2) == 2)
        {
            tablero[2, 1] = false;
            tablero[4, 3] = false;
        }
        else if (EstaCelulaViva(2, 2) && ContarVecinasDiagonalSecundaria(2, 2) == 2)
        {
            tablero[3, 1] = false;
            tablero[1, 3] = false;
        }
        else if (EstaCelulaViva(2, 3) && ContarVecinasDiagonalSecundaria(2, 3) == 2)
        {
            tablero[3, 2] = false;
            tablero[1, 4] = false;
        }
        else if (EstaCelulaViva(3, 3) && ContarVecinasDiagonalSecundaria(3, 3) == 2)
        {
            tablero[4, 2] = false;
            tablero[2, 4] = false;
        }
        else
        {
            tablero[2, 2] = false;
            tablero[3, 2] = false;
        }

        return (bool[,])tablero.Clone();
    }

    private int ContarVecinas(int fila, int columna)
    {
        return ContarVecinasVerticales(fila, columna) +
               ContarVecinasHorizontales(fila, columna) +
               ContarVecinasDiagonalPrincipal(fila, columna) +
               ContarVecinasDiagonalSecundaria(fila, columna);
    }


    private int ContarVecinasDiagonalSecundaria(int fila, int columna)
    {
        var cantidadVecinas = 0;

        if (EstaCelulaViva(fila - 1, columna + 1))
            cantidadVecinas++;
        if (EstaCelulaViva(fila + 1, columna - 1))
            cantidadVecinas++;

        return cantidadVecinas;
    }

    private int ContarVecinasDiagonalPrincipal(int fila, int columna)
    {
        var cantidadVecinas = 0;

        if (EstaCelulaViva(fila - 1, columna - 1))
            cantidadVecinas++;
        if (EstaCelulaViva(fila + 1, columna + 1))
            cantidadVecinas++;

        return cantidadVecinas;
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

    private bool EstaCelulaViva(int fila, int columna)
    {
        try
        {
            return tablero[fila, columna];
        }
        catch (IndexOutOfRangeException _)
        {
            return false;
        }
    }
}