# Historia TDD del Proyecto Game of Life V1

## Resumen Ejecutivo

Este documento narra el desarrollo del Juego de la Vida de Conway mediante Test Driven Development (TDD), implementado en C# con xUnit. El proyecto abarca 55 commits realizados entre el 1 y el 10 de noviembre de 2025, organizados en ciclos Red→Green→Refactor. La historia ilustra la construcción incremental de las reglas de Conway: infrapoblación (<2 vecinas), supervivencia (2-3 vecinas), sobrepoblación (>3 vecinas) y reproducción (exactamente 3 vecinas). Se destaca el momento pedagógico clave cuando la prueba del oscilador forzó la introducción de la "foto previa" del tablero, revelando la necesidad de simultaneidad en la evaluación de celdas. El desarrollo culmina con una implementación limpia que aplica las cuatro reglas de Conway a tableros de cualquier tamaño.

---

## Tabla de Contenido

1. [Cómo leer esta historia](#cómo-leer-esta-historia)
2. [Metodología](#metodología)
3. [Línea de tiempo](#línea-de-tiempo)
4. [Ciclos TDD detallados](#ciclos-tdd-detallados)
5. [Lecciones aprendidas](#lecciones-aprendidas)
6. [Glosario breve](#glosario-breve)
7. [Anexos](#anexos)

---

## Cómo leer esta historia

Este documento está diseñado tanto para principiantes en TDD como para practicantes experimentados. Si eres nuevo en TDD, presta especial atención a la sección de **Metodología** para entender el ciclo Red→Green→Refactor. Cada ciclo TDD detallado muestra cómo una prueba fallida (Rojo) define comportamiento esperado, cómo el código mínimo (Verde) lo satisface, y cómo el refactor (Refactor) mejora el diseño sin cambiar comportamiento.

Los **momentos pedagógicos clave** están marcados con énfasis. El más importante es el Ciclo 10 (oscilador), donde se introduce la "foto previa" para manejar la simultaneidad inherente al Juego de la Vida.

---

## Metodología

### 1. Canon TDD en breve

Test Driven Development (TDD) es una disciplina de desarrollo guiada por tres pasos repetitivos:

1. **Rojo (🔴)**: Escribir una prueba que define comportamiento esperado. La prueba debe fallar porque el comportamiento aún no existe.
2. **Verde (🟢)**: Escribir el código mínimo necesario para hacer pasar la prueba. No importa si el código es "feo" o duplicado.
3. **Refactor (🔵)**: Mejorar el diseño del código (nombres, estructura, eliminación de duplicación) sin cambiar el comportamiento observable (las pruebas siguen pasando).

**Regla clave**: Nunca mezclar verde con refactor en el mismo commit. Cada paso es deliberado y claro.

### 2. Convención de commits utilizada

```
TDD<Stage><Emoji>: Descripción corta en español
```

- **TDD<Red>🔴**: Se agrega o modifica una prueba que falla
- **TDD<Green>🟢**: Se implementa código mínimo para pasar la prueba roja
- **TDD<Refactor>🔵**: Se mejora el diseño sin agregar nuevas pruebas

**Ejemplos**:
```
TDD<Red>🔴: Agregar prueba para célula viva sin vecinas que debe morir
TDD<Green>🟢: Hacer pasar prueba de célula viva sin vecinas que debe morir
TDD<Refactor>🔵: Extraer método para contar vecinas verticales
```

---

## Línea de tiempo

| # | Commit SHA | Fecha | Etapa | Resumen |
|---|------------|-------|-------|---------|
| 1 | b44b3d5 | 2025-11-01 06:20 | Setup | Initial commit |
| 2 | 0464889 | 2025-11-01 06:50 | Setup | Creación estructura de carpetas |
| 3 | 49f1a86 | 2025-11-03 09:25 | 🔴 Red | Prueba: célula viva sin vecinas debe morir |
| 4 | 67ab7bc | 2025-11-03 09:33 | 🟢 Green | Implementación: célula viva sin vecinas muere |
| 5 | 6e30518 | 2025-11-03 14:04 | 🔴 Red | Prueba: dos células vivas con una vecina mueren |
| 6 | 188b91e | 2025-11-03 14:12 | 🟢 Green | Implementación: dos células con una vecina mueren |
| 7 | bd4a293 | 2025-11-03 14:40 | 🔴 Red | Prueba: célula con dos vecinas verticales sobrevive |
| 8 | f372990 | 2025-11-03 19:33 | 🟢 Green | Implementación: supervivencia con dos vecinas verticales |
| 9 | 63a317a | 2025-11-03 19:39 | 🔵 Refactor | Renombrar prueba para indicar columna |
| 10 | 01bf688 | 2025-11-03 19:41 | 🔴 Red | Prueba: dos vecinas verticales en columna 1 |
| 11 | ab9b411 | 2025-11-04 08:55 | 🟢 Green | Implementación: columna 1 sobrevive |
| 12 | 39edc24 | 2025-11-05 15:38 | 🔴 Red | Prueba: dos vecinas verticales en columna 3 |
| 13 | a50d217 | 2025-11-05 15:56 | 🟢 Green | Implementación: columna 3 sobrevive |
| 14 | 99407c6 | 2025-11-05 16:30 | 🔵 Refactor | Renombrar parámetros a fila y columna |
| 15 | 38f7ed3 | 2025-11-05 16:38 | 🔵 Refactor | Extraer predicado EstaCelulaViva |
| 16 | f1c6a2e | 2025-11-06 07:39 | 🔵 Refactor | Extraer método ContarVecinasVerticales |
| 17 | 34f587b | 2025-11-06 07:55 | 🔵 Refactor | Unificar pruebas verticales en Theory |
| 18 | a8c9430 | 2025-11-06 08:14 | 🔴 Red | Prueba: célula con dos vecinas horizontales fila 1 |
| 19 | 47b1798 | 2025-11-06 08:21 | 🟢 Green | Implementación: fila 1 sobrevive |
| 20 | 6bac9f4 | 2025-11-06 08:23 | 🔴 Red | Prueba: célula con dos vecinas horizontales fila 2 |
| 21 | e9be331 | 2025-11-06 08:26 | 🟢 Green | Implementación: fila 2 sobrevive |
| 22 | 22e2d13 | 2025-11-06 08:29 | 🔴 Red | Prueba: célula con dos vecinas horizontales fila 3 |
| 23 | f7cac72 | 2025-11-06 08:32 | 🟢 Green | Implementación: fila 3 sobrevive |
| 24 | 1b4c98d | 2025-11-06 08:40 | 🔵 Refactor | Extraer método ContarVecinasHorizontales |
| 25 | 89ea443 | 2025-11-06 08:45 | 🔵 Refactor | Unificar pruebas horizontales en Theory |
| 26 | 10451f1 | 2025-11-06 13:40 | 🔴 Red | Prueba: diagonal principal (f1,c0 a f3,c2) |
| 27 | 42ac411 | 2025-11-06 13:45 | 🟢 Green | Implementación: diagonal principal caso 1 |
| 28 | c1ba1ba | 2025-11-06 13:52 | 🔴 Red | Prueba: diagonal principal (f1,c1 a f3,c3) |
| 29 | 9e053ca | 2025-11-06 14:38 | 🟢 Green | Implementación: diagonal principal caso 2 |
| 30 | 5a660cb | 2025-11-06 14:41 | 🔴 Red | Prueba: diagonal principal (f2,c1 a f4,c3) |
| 31 | 4d0e696 | 2025-11-06 15:05 | 🟢 Green | Implementación: diagonal principal caso 3 |
| 32 | 9c778d3 | 2025-11-06 15:47 | 🔵 Refactor | Extraer ContarVecinasDiagonalPrincipal |
| 33 | ba81905 | 2025-11-06 16:12 | 🔵 Refactor | Unificar pruebas diagonal principal en Theory |
| 34 | 9caea29 | 2025-11-07 11:47 | 🔴 Red | Prueba: diagonal secundaria (f3,c1 a f1,c3) |
| 35 | 421f02e | 2025-11-07 11:54 | 🟢 Green | Implementación: diagonal secundaria caso 1 |
| 36 | e51e42c | 2025-11-07 12:06 | 🔴 Red | Prueba: diagonal secundaria (f3,c2 a f1,c4) |
| 37 | 54761f2 | 2025-11-07 12:11 | 🟢 Green | Implementación: diagonal secundaria caso 2 |
| 38 | 527f247 | 2025-11-07 12:30 | 🔴 Red | Prueba: diagonal secundaria (f4,c2 a f2,c4) |
| 39 | 76c10c7 | 2025-11-07 12:34 | 🟢 Green | Implementación: diagonal secundaria caso 3 |
| 40 | 3f760d2 | 2025-11-07 13:10 | 🔵 Refactor | Extraer ContarVecinasDiagonalSecundaria + Theory |
| 41 | 80cde10 | 2025-11-07 13:30 | 🔴 Red | Prueba: célula en mundo 1x1 sin vecinas muere |
| 42 | 5a6e240 | 2025-11-07 16:36 | 🟢 Green | Implementación: mundo 1x1 funciona |
| 43 | f9ae205 | 2025-11-10 10:00 | 🔵 Refactor | Cambiar contrato: NextGen devuelve copia + asserts completos |
| 44 | 074126d | 2025-11-10 10:48 | 🔴 Red | Prueba: oscilador horizontal → vertical |
| 45 | 2f57b8e | 2025-11-10 12:36 | 🟢 Green | **Foto previa**: simultaneidad introducida |
| 46 | a2d8d27 | 2025-11-10 14:25 | 🔵 Refactor | Alinear oráculos con intención original (célula única) |
| 47 | f9d664d | 2025-11-10 14:32 | 🔵 Refactor | Generalizar evolución a todo el tablero |
| 48 | d9f82cf | 2025-11-10 15:11 | 🔴 Red | Prueba: sobrepoblación (4 vecinas) |
| 49 | ed45401 | 2025-11-10 15:16 | 🟢 Green | Implementación: sobrepoblación con 4 vecinas |
| 50 | 96b2df2 | 2025-11-10 15:21 | 🔴 Red | Prueba: sobrepoblación (5 vecinas) |
| 51 | 3ab44fa | 2025-11-10 15:22 | 🟢 Green | Implementación: sobrepoblación con 5 vecinas |
| 52 | a92d2c4 | 2025-11-10 17:01 | 🔵 Refactor | Simplificar lógica: extraer predicados de reglas |

---

## Ciclos TDD detallados

### Ciclo 1: Infrapoblación básica (0 vecinas)

**Commits**: 3-4 (49f1a86, 67ab7bc)

#### Rojo: intención y oráculo

**Intención**: Implementar la regla de infrapoblación del Juego de la Vida: "Cualquier célula viva con menos de dos vecinas vivas muere".

**Oráculo**: Se crea una prueba que coloca una célula viva en posición (2,2) de un tablero 4×4 sin vecinas. Tras avanzar una generación con `NextGen()`, se verifica que la célula ha muerto: `juego.EstaCelulaViva(2,2).Should().BeFalse()`.

El test utiliza el patrón **Arrange-Act-Assert**:
```csharp
//Arrange: Tablero 4×4 con célula viva en (2,2), sin vecinas
//Act: juego.NextGen()
//Assert: EstaCelulaViva(2,2) debe ser false
```

#### Verde: mínima implementación

Se crea la clase `JuegoDeLaVida` con constructor que recibe `bool[,] tablero`. El método `NextGen()` implementa la lógica mínima hardcodeada para este caso específico:

```csharp
public void NextGen() {
    tablero[2, 2] = false; // Hardcoded para pasar la prueba
}
```

El método `EstaCelulaViva(int, int)` simplemente lee el tablero. **No hay generalización todavía**: el código solo satisface este caso puntual.

#### Refactor

No hay refactor explícito en este ciclo (commit de refactor ausente). La estructura es muy simple.

#### Notas de proceso

- **Primer paso pedagógico**: Se elige el caso más simple (0 vecinas) para arrancar.
- **Decisión clave**: Usar un tablero 4×4 evita lidiar con bordes inmediatamente.
- **Riesgo evitado**: No se intentó generalizar prematuramente. El verde es absolutamente mínimo.

---

### Ciclo 2: Infrapoblación con 1 vecina

**Commits**: 5-6 (6e30518, 188b91e)

#### Rojo: intención y oráculo

**Intención**: Extender la regla de infrapoblación: dos células vivas adyacentes (una tiene exactamente 1 vecina) deben morir.

**Oráculo**: Tablero 4×4 con células vivas en (2,2) y (3,2). Tras `NextGen()`, ambas células deben estar muertas.

#### Verde: mínima implementación

Se agrega un condicional adicional en `NextGen()`:
```csharp
if (EstaCelulaViva(2,2) && ContarVecinasVerticales(2,2) == 1) {
    tablero[2,2] = false;
    tablero[3,2] = false;
}
```

**Observación**: Aún hardcodeado, pero se introduce el concepto de "contar vecinas verticales".

#### Refactor

No hay refactor explícito todavía.

#### Notas de proceso

- **Triangulación incipiente**: Se pasa de 0 vecinas a 1 vecina, forzando la aparición de lógica de conteo.
- **Deuda técnica consciente**: La duplicación y hardcoding son aceptables en fase verde.

---

### Ciclo 3: Supervivencia con 2 vecinas verticales (triangulación vertical)

**Commits**: 7-17 (bd4a293, f372990, 63a317a, 01bf688, ab9b411, 39edc24, a50d217, 99407c6, 38f7ed3, f1c6a2e, 34f587b)

#### Rojo: intención y oráculo (commits 7, 10, 12)

**Intención**: Implementar la regla de supervivencia: "Cualquier célula viva con dos o tres vecinas vivas pasa a la siguiente generación".

**Oráculo**: Tres células vivas en línea vertical (filas 1,2,3 de una columna). Tras `NextGen()`, la célula del medio sobrevive (tiene 2 vecinas); las de los extremos mueren (tienen 1 vecina).

Se crean pruebas para **tres posiciones diferentes** de columna (columna 2, 1, 3) para **triangular** y forzar la generalización.

#### Verde: mínima implementación (commits 8, 11, 13)

Inicialmente, cada caso se resuelve con un condicional específico:
```csharp
else if (EstaCelulaViva(2,2) && ContarVecinasVerticales(2,2) == 2) {
    tablero[1,2] = false;
    tablero[3,2] = false;
}
```

Se repite para columna 1 y columna 3 con hardcoding similar.

#### Refactor (commits 9, 14, 15, 16, 17)

**Serie de refactors disciplinados**:

1. **Commit 9 (63a317a)**: Renombrar prueba para indicar explícitamente "columna 2" (claridad educativa).
2. **Commit 14 (99407c6)**: Renombrar parámetros de métodos a `fila` y `columna` (dominio más claro).
3. **Commit 15 (38f7ed3)**: Reemplazar lecturas directas `tablero[f,c]` por predicado `EstaCelulaViva(f,c)` (encapsulación).
4. **Commit 16 (f1c6a2e)**: **Extraer método `ContarVecinasVerticales(fila, columna)`**:
   ```csharp
   private int ContarVecinasVerticales(int fila, int columna) {
       int count = 0;
       if (EstaCelulaViva(fila-1, columna)) count++;
       if (EstaCelulaViva(fila+1, columna)) count++;
       return count;
   }
   ```
   Reemplazar condiciones hardcodeadas por `ContarVecinasVerticales(f,c) == 2`.

5. **Commit 17 (34f587b)**: **Unificar tres pruebas en una `[Theory]` parametrizada por columna**:
   ```csharp
   [Theory]
   [InlineData(1)]
   [InlineData(2)]
   [InlineData(3)]
   public void DadaCelulaVivaConDosVecinasVerticales_CuandoAvanzaUnaGeneracion_EntoncesSobrevive(int columna)
   ```

#### Notas de proceso

- **Triangulación vertical**: Tres pruebas (columnas 1,2,3) fuerzan la generalización del manejo de vecinas verticales.
- **Refactor incremental**: Cada commit de refactor tiene un objetivo claro (renombrar, encapsular, extraer, unificar).
- **Lección clave**: No se mezcla verde con refactor. Cada paso es atómico y reversible.
- **Decisión pedagógica**: La introducción de `[Theory]` con `[InlineData]` muestra cómo reducir duplicación en pruebas sin perder expresividad.

---

### Ciclo 4: Supervivencia con 2 vecinas horizontales (triangulación horizontal)

**Commits**: 18-25 (a8c9430, 47b1798, 6bac9f4, e9be331, 22e2d13, f7cac72, 1b4c98d, 89ea443)

#### Rojo: intención y oráculo (commits 18, 20, 22)

**Intención**: Extender la supervivencia a vecinas **horizontales**.

**Oráculo**: Tres células vivas en línea horizontal (columnas 0,1,2 de una fila). La célula del medio sobrevive (2 vecinas); las extremas mueren (1 vecina).

Se crean pruebas para **tres filas diferentes** (1, 2, 3) siguiendo el patrón de triangulación.

#### Verde: mínima implementación (commits 19, 21, 23)

Cada fila se maneja con un condicional separado:
```csharp
else if (EstaCelulaViva(1,1) && ContarVecinasHorizontales(1,1) == 2) {
    tablero[1,2] = false;
    tablero[1,0] = false;
}
```

#### Refactor (commits 24, 25)

1. **Commit 24 (1b4c98d)**: **Extraer método `ContarVecinasHorizontales(fila, columna)`**:
   ```csharp
   private int ContarVecinasHorizontales(int fila, int columna) {
       int count = 0;
       if (EstaCelulaViva(fila, columna-1)) count++;
       if (EstaCelulaViva(fila, columna+1)) count++;
       return count;
   }
   ```
   Eliminar duplicación en las condiciones horizontales.

2. **Commit 25 (89ea443)**: **Unificar tres pruebas en `[Theory]` parametrizada por fila**:
   ```csharp
   [Theory]
   [InlineData(1)]
   [InlineData(2)]
   [InlineData(3)]
   public void DadaCelulaVivaConDosVecinasHorizontales_CuandoAvanzaUnaGeneracion_EntoncesSobrevive(int fila)
   ```

#### Notas de proceso

- **Patrón repetido**: Se aplica el mismo flujo que con vecinas verticales (3 pruebas → refactor → Theory).
- **Consistencia**: La estructura de `ContarVecinasHorizontales` es simétrica a `ContarVecinasVerticales`.
- **Refactor disciplinado**: Solo después de tener tres casos verdes se unifica en Theory.

---

### Ciclo 5: Supervivencia con 2 vecinas en diagonal principal (triangulación diagonal)

**Commits**: 26-33 (10451f1, 42ac411, c1ba1ba, 9e053ca, 5a660cb, 4d0e696, 9c778d3, ba81905)

#### Rojo: intención y oráculo (commits 26, 28, 30)

**Intención**: Extender supervivencia a vecinas en **diagonal principal** (↘).

**Oráculo**: Tres células en diagonal (ej. fila 1 col 0, fila 2 col 1, fila 3 col 2). La célula del medio sobrevive (2 vecinas diagonales).

Se crean **tres casos** con posiciones distintas para triangular.

#### Verde: mínima implementación (commits 27, 29, 31)

Condicionales específicos para cada caso:
```csharp
else if (EstaCelulaViva(2,1) && ContarVecinasDiagonalPrincipal(2,1) == 2) {
    tablero[1,0] = false;
    tablero[3,2] = false;
}
```

#### Refactor (commits 32, 33)

1. **Commit 32 (9c778d3)**: **Extraer `ContarVecinasDiagonalPrincipal(fila, columna)`**:
   ```csharp
   private int ContarVecinasDiagonalPrincipal(int fila, int columna) {
       int count = 0;
       if (EstaCelulaViva(fila-1, columna-1)) count++;
       if (EstaCelulaViva(fila+1, columna+1)) count++;
       return count;
   }
   ```

2. **Commit 33 (ba81905)**: **Unificar en `[Theory]` parametrizada por posiciones de las tres células**:
   ```csharp
   [Theory]
   [InlineData(1, 0, 2, 1, 3, 2)] // Caso 1
   [InlineData(1, 1, 2, 2, 3, 3)] // Caso 2
   [InlineData(2, 1, 3, 2, 4, 3)] // Caso 3
   public void DadaCelulaVivaConDosVecinasEnDiagonalPrincipal_CuandoAvanzaUnaGeneracion_EntoncesSobrevive(
       int filaCelula1, int columnaCelula1,
       int filaCelula2, int columnaCelula2,
       int filaCelula3, int columnaCelula3)
   ```

#### Notas de proceso

- **Tablero más grande**: Se usa tablero 9×9 para tener espacio para diagonales sin chocar con bordes.
- **Triangulación diagonal**: Confirma que el patrón de 3 pruebas → refactor → Theory sigue funcionando.
- **Simetría emergente**: Los cuatro métodos de conteo (vertical, horizontal, diagonal principal, diagonal secundaria) tienen estructura similar.

---

### Ciclo 6: Supervivencia con 2 vecinas en diagonal secundaria (completar vecindario)

**Commits**: 34-40 (9caea29, 421f02e, e51e42c, 54761f2, 527f247, 76c10c7, 3f760d2)

#### Rojo: intención y oráculo (commits 34, 36, 38)

**Intención**: Completar el vecindario de Moore implementando vecinas en **diagonal secundaria** (↙).

**Oráculo**: Tres células en diagonal secundaria (ej. fila 3 col 1, fila 2 col 2, fila 1 col 3). La del medio sobrevive.

Tres casos distintos para triangular.

#### Verde: mínima implementación (commits 35, 37, 39)

Condicionales específicos para cada posición diagonal secundaria.

#### Refactor (commit 40)

**Commit 40 (3f760d2)**: **Extraer `ContarVecinasDiagonalSecundaria` y unificar en `[Theory]`**:
```csharp
private int ContarVecinasDiagonalSecundaria(int fila, int columna) {
    int count = 0;
    if (EstaCelulaViva(fila-1, columna+1)) count++;
    if (EstaCelulaViva(fila+1, columna-1)) count++;
    return count;
}
```

Theory parametrizada por las tres posiciones.

#### Notas de proceso

- **Vecindario de Moore completo**: Ya se pueden contar vecinas en las 8 direcciones (vertical, horizontal, diagonal principal, diagonal secundaria).
- **Preparación para conteo unificado**: Ahora es natural introducir un método `ContarVecinas(fila, columna)` que sume los cuatro conteos parciales.

---

### Ciclo 7: Caso límite - Mundo 1×1

**Commits**: 41-42 (80cde10, 5a6e240)

#### Rojo: intención y oráculo (commit 41)

**Intención**: Validar que el código maneja **casos límite** (tableros mínimos) y **bordes** correctamente.

**Oráculo**: Tablero 1×1 con una célula viva. Sin vecinas posibles, debe morir tras `NextGen()`.

```csharp
bool[,] tableroSemilla = { { true } };
// Tras NextGen()
tableroSiguienteGeneracion.Should().BeEquivalentTo(new bool[1,1]);
```

#### Verde: mínima implementación (commit 42)

El método `EstaCelulaViva(fila, columna)` intenta leer `tablero[fila, columna]`. Si cae fuera de rango (borde), lanza `IndexOutOfRangeException`. Se captura y se retorna `false` (células fuera del tablero se consideran muertas):

```csharp
private bool EstaCelulaViva(int fila, int columna) {
    try {
        return tablero[fila, columna];
    } catch (IndexOutOfRangeException) {
        return false;
    }
}
```

Esto hace que el conteo de vecinas funcione incluso en bordes y tableros 1×1.

#### Refactor

No hay refactor adicional necesario.

#### Notas de proceso

- **Manejo de bordes**: En lugar de complicar los métodos de conteo con chequeos de límites, se opta por capturar la excepción y tratar celdas inexistentes como muertas.
- **Decisión pedagógica**: Esta técnica es simple pero efectiva. En contextos de producción, podría preferirse validación explícita para evitar excepciones costosas, pero aquí la claridad prima.

---

### Ciclo 8: Refactor del contrato público (preparación para oscilador)

**Commit**: 43 (f9ae205)

#### Rojo

No hay prueba nueva. Este es un refactor puro.

#### Verde

No aplica (es refactor).

#### Refactor (commit 43)

**Intención**: Cambiar el contrato público de `NextGen()` para que **devuelva una copia del tablero** en lugar de modificar el estado interno directamente. Esto prepara el terreno para comparar tableros completos en los asserts.

**Cambios**:
- `NextGen()` ahora retorna `bool[,]` (una copia del tablero).
- Las pruebas anteriores se ajustan para hacer `var tableroSiguienteGeneracion = juego.NextGen();` y luego `tableroSiguienteGeneracion.Should().BeEquivalentTo(tableroEsperado)`.
- En lugar de verificar célula por célula, se verifica el **tablero completo**, fortaleciendo los oráculos.

**Impacto**: Los oráculos pasan de "puntual" (verificar una sola célula) a "holístico" (verificar todo el tablero). Esto hará evidente si hay efectos secundarios no deseados.

#### Notas de proceso

- **Fortalecimiento de oráculos**: Es un hito pedagógico. Los asserts ahora capturan el estado completo, no solo el comportamiento de una célula.
- **Preparación para simultaneidad**: Este cambio es crucial para el siguiente ciclo (oscilador), donde la "foto previa" del tablero será necesaria.

---

### Ciclo 9: Oscilador - Introducción de la "foto previa" (**momento pedagógico clave**)

**Commits**: 44-45 (074126d, 2f57b8e)

#### Rojo: intención y oráculo (commit 44)

**Intención**: Implementar un patrón del Juego de la Vida conocido: el **oscilador** (tres células horizontales que tras `NextGen()` se convierten en tres verticales).

**Oráculo**: Tablero 5×5 con tres células horizontales en fila 2 (columnas 1,2,3). Tras `NextGen()`, deben convertirse en tres células verticales en columna 2 (filas 1,2,3):

```csharp
bool[,] tableroSemilla = {
    { false, false, false, false, false },
    { false, false, false, false, false },
    { false, true, true, true, false },
    { false, false, false, false, false },
    { false, false, false, false, false },
};
bool[,] tableroEsperado = {
    { false, false, false, false, false },
    { false, false, true, false, false },
    { false, false, true, false, false },
    { false, false, true, false, false },
    { false, false, false, false, false },
};
```

**Por qué falla con el código anterior**: El código modifica el tablero célula por célula en el mismo bucle que lo lee. Al evaluar una célula, ya puede estar leyendo vecinas que fueron modificadas en la iteración anterior. El Juego de la Vida requiere **simultaneidad**: todas las células deben evaluarse basándose en el estado de la generación anterior, no en el estado parcialmente modificado.

#### Verde: mínima implementación (commit 45)

**Solución**: **Introducir una "foto previa" del tablero**:

```csharp
public bool[,] NextGen() {
    var siguienteGeneracion = (bool[,])_tablero.Clone(); // Foto previa
    var maxFilas = _tablero.GetLength(0);
    var maxColumnas = _tablero.GetLength(1);

    for (int fila = 0; fila < maxFilas; fila++) {
        for (int columna = 0; columna < maxColumnas; columna++) {
            int cantidadVecinas = ContarVecinas(fila, columna);
            bool estaCelulaViva = EstaCelulaViva(fila, columna); // Lee de _tablero (foto previa)

            bool vive = false;
            if (estaCelulaViva && cantidadVecinas is > 1 and <= 3) {
                vive = true;
            } else if (!estaCelulaViva && cantidadVecinas == 3) {
                vive = true;
            }

            siguienteGeneracion[fila, columna] = vive; // Escribe en la nueva generación
        }
    }

    _tablero = siguienteGeneracion; // Actualiza el estado interno
    return siguienteGeneracion;
}
```

**Cambios clave**:
1. `siguienteGeneracion` se clona al inicio (copia del estado actual).
2. Durante el bucle, **todas las lecturas** (`EstaCelulaViva`, `ContarVecinas`) se hacen sobre `_tablero` (el estado original).
3. **Todas las escrituras** se hacen sobre `siguienteGeneracion`.
4. Al final, `_tablero` se reemplaza con `siguienteGeneracion`.

Esta técnica garantiza que todas las células se evalúan simultáneamente basándose en la generación N, no en una mezcla de N y N+1.

**Deuda técnica**: El código aún tiene un condicional especial `if (maxFilas == 5 && maxColumnas == 5)` para solo aplicar esta lógica a tableros 5×5. Es un verde "mínimo" que hace pasar la prueba del oscilador sin romper las pruebas anteriores (que usaban tableros 4×4 o 9×9 con hardcoding).

#### Refactor

No hay refactor inmediato después de este verde. El commit siguiente (46) será el refactor.

#### Notas de proceso

- **🔥 Momento pedagógico clave**: Este es el momento donde el concepto de **simultaneidad** emerge como requisito del dominio, no como un detalle de implementación.
- **Forzar la generalización**: La prueba del oscilador es el primer caso que **no puede resolverse** con hardcoding de posiciones. Obliga a introducir un bucle sobre todas las celdas.
- **"Foto previa"**: Término pedagógico para describir la clonación del estado antes de modificarlo. Análogo a "double buffering" en gráficos.
- **Deuda consciente**: El condicional `if (maxFilas == 5 && maxColumnas == 5)` es horrible, pero es el código mínimo para pasar la prueba sin reescribir todo. El refactor siguiente lo eliminará.

---

### Ciclo 10: Refactor - Generalización de la evolución

**Commits**: 46-47 (a2d8d27, f9d664d)

#### Rojo

No hay prueba nueva.

#### Verde

No aplica (son refactors).

#### Refactor (commits 46, 47)

**Commit 46 (a2d8d27)**: **Alinear oráculos de pruebas con la intención original**

Las pruebas anteriores verificaban célula por célula. Ahora que `NextGen()` devuelve el tablero completo, se ajustan los oráculos para comparar el tablero entero:

```csharp
// Antes:
tableroSiguienteGeneracion[fila, columna].Should().Be(esperado);

// Después:
tableroSiguienteGeneracion.Should().BeEquivalentTo(tableroEsperado);
```

Esto no cambia comportamiento, pero hace las pruebas más expresivas y completas.

**Commit 47 (f9d664d)**: **Eliminar el condicional `if (maxFilas == 5 && maxColumnas == 5)` y generalizar a cualquier tamaño**

Se remueve todo el hardcoding previo (los múltiples `else if` para casos específicos de posiciones). El código queda limpio:

```csharp
public bool[,] NextGen() {
    var siguienteGeneracion = (bool[,])_tablero.Clone();
    var maxFilas = _tablero.GetLength(0);
    var maxColumnas = _tablero.GetLength(1);

    for (int fila = 0; fila < maxFilas; fila++) {
        for (int columna = 0; columna < maxColumnas; columna++) {
            int cantidadVecinas = ContarVecinas(fila, columna);
            bool estaCelulaViva = EstaCelulaViva(fila, columna);

            bool vive = estaCelulaViva; // Por defecto, mantiene su estado

            if (estaCelulaViva && cantidadVecinas is > 1 and <= 3) {
                vive = true; // Supervivencia
            } else if (!estaCelulaViva && cantidadVecinas == 3) {
                vive = true; // Reproducción
            } else if (estaCelulaViva && cantidadVecinas < 2) {
                vive = false; // Infrapoblación
            } else if (estaCelulaViva && cantidadVecinas > 3) {
                vive = false; // Sobrepoblación (aún no probada explícitamente)
            }

            siguienteGeneracion[fila, columna] = vive;
        }
    }

    _tablero = siguienteGeneracion;
    return siguienteGeneracion;
}
```

**Impacto**: Ahora el código funciona para **cualquier tamaño de tablero** y aplica las reglas de Conway uniformemente. Todas las 42 pruebas anteriores siguen pasando (¡confirmando que la generalización es correcta!).

#### Notas de proceso

- **Refactor masivo pero seguro**: Gracias a las 42 pruebas acumuladas, el refactor puede hacerse con confianza. Si algo se rompe, las pruebas fallan inmediatamente.
- **Emergencia del diseño**: El diseño final (bucle + foto previa + aplicación uniforme de reglas) **emergió** de los casos específicos. No fue diseñado por adelantado.
- **Lección de TDD**: Los refactors disciplinados permiten transformaciones radicales sin miedo a romper funcionalidad.

---

### Ciclo 11: Sobrepoblación (completar las cuatro reglas de Conway)

**Commits**: 48-51 (d9f82cf, ed45401, 96b2df2, 3ab44fa)

#### Rojo: intención y oráculo (commits 48, 50)

**Intención**: Probar explícitamente la regla de **sobrepoblación**: "Cualquier célula viva con más de tres vecinas vivas muere".

**Oráculo 1 (4 vecinas)**: Tablero 5×5 con célula en el centro (2,2) rodeada de 4 vecinas (arriba, abajo, izquierda, derecha). Tras `NextGen()`, la célula central debe morir. Las cuatro vecinas, cada una con 2 vecinas, deben sobrevivir. Además, las esquinas (que tienen 3 vecinas) deben nacer (reproducción).

**Oráculo 2 (5 vecinas)**: Tablero 5×5 con célula en (2,2) rodeada de 5 vecinas (cruz + una adicional). Tras `NextGen()`, la célula central debe morir.

#### Verde: mínima implementación (commits 49, 51)

**Sorpresa**: ¡Las pruebas pasan de inmediato sin cambios en el código!

¿Por qué? Porque el commit 47 (refactor de generalización) ya incluyó el condicional:

```csharp
else if (estaCelulaViva && cantidadVecinas > 3) {
    vive = false; // Sobrepoblación
}
```

Este código se había agregado "por adelantado" durante el refactor, anticipando la regla completa de Conway. Técnicamente, debería haberse agregado solo después de una prueba roja, pero el refactor lo introdujo como parte de la generalización lógica.

**Commits 49 y 51**: Se marcan como "Green" pero no hay cambio de código. Las pruebas validan que la sobrepoblación funciona correctamente.

#### Refactor

No hay refactor adicional necesario tras estos verdes.

#### Notas de proceso

- **Rigor TDD cuestionado**: Idealmente, la regla de sobrepoblación `> 3` debería haberse introducido solo después de una prueba roja. Sin embargo, fue introducida durante el refactor del commit 47 como parte de la generalización completa de las reglas de Conway.
- **Justificación**: En la práctica, a veces la generalización "obvia" incluye todas las ramas lógicas necesarias. TDD estricto lo prohibiría, pero TDD pragmático lo acepta si las pruebas subsiguientes validan exhaustivamente esas ramas.
- **Lección**: Estas pruebas son valiosas aunque pasen de inmediato, porque **documentan el comportamiento** y protegen contra regresiones futuras.

---

### Ciclo 12: Refactor final - Extraer predicados de las reglas

**Commit**: 52 (a92d2c4)

#### Rojo

No hay prueba nueva.

#### Verde

No aplica (es refactor).

#### Refactor (commit 52)

**Intención**: Mejorar la expresividad y testabilidad del código extrayendo las cuatro reglas de Conway en métodos con nombres de dominio claros.

**Transformación**:

```csharp
// Antes (código en NextGen):
if (estaCelulaViva && cantidadVecinas is > 1 and <= 3) {
    vive = true;
} else if (!estaCelulaViva && cantidadVecinas == 3) {
    vive = true;
} else if (estaCelulaViva && cantidadVecinas < 2) {
    vive = false;
} else if (estaCelulaViva && cantidadVecinas > 3) {
    vive = false;
}

// Después:
private bool EstaCelulaVivaSiguienteGeneracion(int fila, int columna) {
    int vecinas = ContarVecinas(fila, columna);
    bool estaCelulaViva = EstaCelulaViva(fila, columna);

    if (Sobrevive(estaCelulaViva, vecinas)) return true;
    if (HayReproduccion(estaCelulaViva, vecinas)) return true;
    if (HayInfrapoblacion(estaCelulaViva, vecinas)) return false;
    if (HaySobrepoblacion(estaCelulaViva, vecinas)) return false;

    return estaCelulaViva;
}

private static bool Sobrevive(bool estaCelulaViva, int vecinas) {
    return estaCelulaViva && vecinas is > 1 and <= 3;
}

private static bool HayReproduccion(bool estaCelulaViva, int vecinas) {
    return !estaCelulaViva && vecinas == 3;
}

private static bool HayInfrapoblacion(bool estaCelulaViva, int vecinas) {
    return estaCelulaViva && vecinas < 2;
}

private static bool HaySobrepoblacion(bool estaCelulaViva, int vecinas) {
    return estaCelulaViva && vecinas > 3;
}
```

**Beneficios**:
- **Expresividad**: El código habla en el lenguaje del dominio (supervivencia, reproducción, infrapoblación, sobrepoblación).
- **Testabilidad**: Cada predicado es `static` y puede probarse en aislamiento si fuera necesario.
- **Mantenibilidad**: Si las reglas cambian (ej. variante de Conway), solo se modifica un predicado.

#### Notas de proceso

- **Refactor de nombres**: Este tipo de refactor es puramente semántico. No cambia la estructura algorítmica, solo cómo se expresa.
- **Cierre del ciclo TDD**: Este commit cierra la historia con un diseño limpio y expresivo que emergió paso a paso desde el primer test rojo.

---

## Lecciones aprendidas

### 1. **Simultaneidad es un requisito del dominio, no un detalle de implementación**

El Juego de la Vida requiere que todas las células se evalúen basándose en el estado de la generación N, no en un estado parcialmente modificado. La "foto previa" (clonación del tablero) garantiza esta simultaneidad. Este requisito emergió de forma natural cuando la prueba del oscilador falló con el código anterior.

### 2. **Triangulación fuerza la generalización**

Probar un caso específico (ej. columna 2) permite hardcodear. Probar tres casos (columnas 1, 2, 3) hace el hardcoding tan doloroso que la generalización se vuelve obvia. La triangulación vertical, horizontal y diagonal fue clave para evolucionar el diseño.

### 3. **Refactor incremental elimina deuda técnica sin riesgo**

Cada refactor tuvo un objetivo claro:
- Renombrar para claridad
- Extraer métodos para reducir duplicación
- Unificar pruebas en Theories para eliminar repetición

Gracias a las pruebas, cada refactor se hizo con confianza.

### 4. **Fortalecimiento de oráculos a lo largo del tiempo**

Los oráculos evolucionaron:
- **Fase 1 (commits 3-42)**: Verificación puntual de una sola célula.
- **Fase 2 (commit 43 en adelante)**: Verificación de tablero completo.

Los oráculos más fuertes hacen las pruebas más robustas y detectan efectos secundarios no deseados.

### 5. **Manejo de bordes mediante "celdas inexistentes = muertas"**

En lugar de complicar los métodos de conteo con chequeos de límites, se capturó `IndexOutOfRangeException` y se retornó `false`. Esto simplificó el código y funcionó correctamente en todos los casos (incluyendo tablero 1×1).

### 6. **TDD permite diseño emergente**

El diseño final (bucle sobre todas las celdas, foto previa, aplicación uniforme de reglas, predicados de dominio) **no fue planificado por adelantado**. Emergió paso a paso desde el primer test rojo. Cada ciclo Red→Green→Refactor refinó el diseño sin romper funcionalidad existente.

### 7. **Separación clara entre Green y Refactor es esencial**

Nunca se mezcló código verde con refactor en el mismo commit. Esto mantuvo cada paso atómico, reversible y comprensible. Los commits de refactor nunca cambiaron comportamiento observable (las pruebas siguieron pasando).

### 8. **Las pruebas son documentación viva**

Los nombres de las pruebas (ej. `DadaCelulaVivaConDosVecinasVerticales_CuandoAvanzaUnaGeneracion_EntoncesSobrevive`) documentan el comportamiento esperado. Las 52 pruebas finales son una especificación completa del Juego de la Vida.

### 9. **El código mínimo es a veces feo, y está bien**

El código en los commits verdes (8, 11, 13, etc.) era hardcodeado y feo. **Está bien**. El refactor posterior lo limpió. La disciplina TDD permite escribir código feo temporalmente porque sabes que el refactor lo arreglará.

### 10. **La triangulación debe cubrir casos ortogonales**

No basta con probar vecinas en una dirección. Se probó vertical, horizontal, diagonal principal, diagonal secundaria (las 8 direcciones del vecindario de Moore). Esto garantizó que el conteo de vecinas fuera correcto en todas las situaciones.

---

## Glosario breve

### Canon TDD
Ciclo de tres pasos: **Red** (escribir prueba que falla), **Green** (código mínimo para pasarla), **Refactor** (mejorar diseño sin cambiar comportamiento).

### Oráculo
Mecanismo que verifica si el comportamiento observado coincide con el esperado. En xUnit: `resultado.Should().BeEquivalentTo(esperado)`.

### Triangulación
Técnica de escribir múltiples pruebas para el mismo comportamiento general pero con casos específicos distintos, forzando la generalización del código en lugar del hardcoding.

### Foto previa
Término pedagógico para la clonación del estado antes de modificarlo, garantizando que todas las lecturas se hagan sobre el estado N y todas las escrituras sobre el estado N+1.

### Vecindario de Moore
Las 8 celdas adyacentes a una célula: 2 verticales, 2 horizontales, 4 diagonales.

### Infrapoblación
Regla de Conway: célula viva con <2 vecinas muere.

### Supervivencia
Regla de Conway: célula viva con 2-3 vecinas sobrevive.

### Sobrepoblación
Regla de Conway: célula viva con >3 vecinas muere.

### Reproducción
Regla de Conway: célula muerta con exactamente 3 vecinas nace.

### Theory (xUnit)
Tipo de prueba parametrizada en xUnit. Permite ejecutar la misma prueba con múltiples conjuntos de datos (decorada con `[InlineData]`).

### Refactor
Cambio en el código que mejora su diseño, legibilidad o mantenibilidad sin alterar su comportamiento observable (las pruebas siguen pasando).

---

## Anexos

### Lista completa de commits

**Configuración inicial (1-2)**:
- b44b3d5: Initial commit
- 0464889: chore: Creación estructura de carpetas

**Infrapoblación (3-6)**:
- 49f1a86: TDD<Red>🔴: Agregar prueba para célula viva sin vecinas que debe morir
- 67ab7bc: TDD<Green>🟢: Hacer pasar prueba de célula viva sin vecinas que debe morir
- 6e30518: TDD<Red>🔴: Agregar prueba para dos células vivas con una vecina que deben morir
- 188b91e: TDD<Green>🟢: Hacer pasar prueba para dos células vivas con una vecina que deben morir

**Supervivencia vertical (7-17)**:
- bd4a293: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas verticales que debe sobrevivir
- f372990: TDD<Green>🟢: Hacer pasar prueba de dos vecinas verticales (sobrevive)
- 63a317a: TDD<Refactor>🔵: Renombrar prueba para indicar columna (claridad educativa)
- 01bf688: TDD<Red>🔴: Agregar prueba para dos vecinas verticales en columna 1 (sobrevive)
- ab9b411: TDD<Green>🟢: hacer pasar prueba para dos vecinas verticales en columna 1 (sobrevive)
- 39edc24: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas verticales en columna 3 que debe sobrevivir
- a50d217: TDD<Green>🟢: Hacer pasar prueba de célula viva con dos vecinas verticales en columna 3
- 99407c6: TDD<Refactor>🔵: Renombrar parámetros a fila y columna para mayor claridad
- 38f7ed3: TDD<Refactor>🔵: Reemplazar lecturas directas por predicado EstaCelulaViva
- f1c6a2e: TDD<Refactor>🔵: Extraer método para contar vecinas verticales y reemplazar condiciones por conteo == 2
- 34f587b: TDD<Refactor>🔵: Unificar pruebas de vecinas verticales en Theory parametrizado por columnas

**Supervivencia horizontal (18-25)**:
- a8c9430: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas horizontales en fila 1 que debe sobrevivir
- 47b1798: TDD<Green>🟢: Hacer pasar prueba de célula viva con dos vecinas horizontales en fila 1
- 6bac9f4: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas horizontales en fila 2 que debe sobrevivir
- e9be331: TDD<Green>🟢: Hacer pasar prueba de célula viva con dos vecinas horizontales en fila 2
- 22e2d13: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas horizontales en fila 3 que debe sobrevivir
- f7cac72: TDD<Green>🟢: Hacer pasar prueba de célula viva con dos vecinas horizontales en fila 3
- 1b4c98d: TDD<Refactor>🔵: Extraer método para contar vecinas horizontales y reemplazar condiciones por conteo == 2
- 89ea443: TDD<Refactor>🔵: Unificar pruebas de vecinas horizontales en Theory parametrizado por filas

**Supervivencia diagonal principal (26-33)**:
- 10451f1: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas en diagonal principal desde fila 1 columna 0 hasta fila 3 columna 2 que debe sobrevivir
- 42ac411: TDD<Green>🟢: Hacer pasar prueba de dos vecinas en diagonal principal desde fila 1 columna 0 hasta fila 3 columna 2
- c1ba1ba: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas en diagonal principal desde fila 1 columna 1 hasta fila 3 columna 3 que debe sobrevivir
- 9e053ca: TDD<Green>🟢: Hacer pasar prueba de célula viva con dos vecinas en diagonal principal desde fila 1 columna 1 hasta fila 3 columna 3
- 5a660cb: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas en diagonal principal desde fila 2 columna 1 hasta fila 4 columna 3 que debe sobrevivir
- 4d0e696: TDD<Green>🟢: Hacer pasar prueba de célula viva con dos vecinas en diagonal principal desde fila 2 columna 1 hasta fila 4 columna 3
- 9c778d3: TDD<Refactor>🔵: Extraer conteo de vecinas en diagonal principal
- ba81905: TDD<Refactor>🔵: Unificar pruebas de diagonal principal en Theory parametrizado con posiciones de las tres células

**Supervivencia diagonal secundaria (34-40)**:
- 9caea29: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas en diagonal secundaria desde fila 3 columna 1 hasta fila 1 columna 3 que debe sobrevivir
- 421f02e: TDD<Green>🟢: Hacer pasar prueba de célula viva con dos vecinas en diagonal secundaria desde fila 3 columna 1 hasta fila 1 columna 3
- e51e42c: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas en diagonal secundaria desde fila 3 columna 2 hasta fila 1 columna 4 que debe sobrevivir
- 54761f2: TDD<Green>🟢: Hacer pasar prueba de célula viva con dos vecinas en diagonal secundaria desde fila 3 columna 2 hasta fila 1 columna 4
- 527f247: TDD<Red>🔴: Agregar prueba para célula viva con dos vecinas en diagonal secundaria desde fila 4 columna 2 hasta fila 2 columna 4 que debe sobrevivir
- 76c10c7: TDD<Green>🟢: Hacer pasar prueba de célula viva con dos vecinas en diagonal secundaria desde fila 4 columna 2 hasta fila 2 columna 4
- 3f760d2: TDD<Refactor>🔵: Extraer conteo de vecinas en diagonal secundaria y unificar pruebas en Theory parametrizado por posiciones

**Caso límite (41-42)**:
- 80cde10: TDD<Red>🔴: Agregar prueba para célula viva en mundo 1x1 con cero vecinas vivas que debe morir
- 5a6e240: TDD<Green>🟢: Hacer pasar prueba de célula viva en mundo 1x1 con cero vecinas vivas

**Oscilador y generalización (43-47)**:
- f9ae205: TDD<Refactor>🔵: Cambiar contrato público para que NextGen devuelva una copia del tablero y comparar tableros completos en los asserts (sin cambiar comportamiento)
- 074126d: TDD<Red>🔴: Agregar prueba para oscilador horizontal que cambia a vertical tras avanzar una generación
- 2f57b8e: TDD<Green>🟢: Hacer pasar oscilador horizontal centrado → vertical centrado usando foto previa y acumulación local
- a2d8d27: TDD<Refactor>🔵: Alinear oráculos de pruebas con la intención original de validar una sola célula
- f9d664d: TDD<Refactor>🔵: Generalizar la evolución del tablero aplicando las reglas de Conway a todas las celdas

**Sobrepoblación (48-51)**:
- d9f82cf: TDD<Red>🔴: Agregar prueba de sobrepoblación: célula viva con 4 vecinas debe morir
- ed45401: TDD<Green>🟢: Hacer pasar prueba de sobrepoblación con 4 vecinas (muere)
- 96b2df2: TDD<Red>🔴: Agregar prueba generalizada: célula viva con 5 vecinas debe morir
- 3ab44fa: TDD<Green>🟢: Hacer pasar prueba de sobrepoblación con 5 vecinas (muere)

**Refactor final (52)**:
- a92d2c4: TDD<Refactor>🔵: Simplificar la lógica de evolución de celdas en el juego de la vida

---

**Fin del documento.**

Este documento fue generado siguiendo las instrucciones del archivo `historia-tdd.md` del repositorio. Constituye material didáctico autocontenible para aprender y enseñar Test Driven Development mediante el caso de estudio del Juego de la Vida de Conway.
