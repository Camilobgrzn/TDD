# CLAUDE.md

Este archivo proporciona orientación a Claude Code (claude.ai/code) cuando trabaja con código en este repositorio.

## Propósito del Repositorio

Este repositorio existe para aprender y enseñar Test Driven Development (TDD). Se organiza en dos áreas principales:

- **Training/**: Ejercicios y katas para practicar y reforzar la disciplina TDD
- **Workshops/**: Material para talleres orientados a enseñar TDD paso a paso

El objetivo es practicar el ciclo TDD: escribir una prueba (Rojo), verla fallar, escribir el mínimo código para que pase (Verde), y mejorar el diseño manteniendo las pruebas en verde (Refactor).

## Tecnologías Utilizadas

Actualmente el repositorio utiliza **C# con .NET 9.0**. Los proyectos usan:
- **xUnit** como framework de pruebas
- **AwesomeAssertions** para aserciones fluidas
- **coverlet.collector** para cobertura de código

## Comandos de Desarrollo

### Ejecutar todas las pruebas
```bash
dotnet test
```

### Ejecutar una prueba específica
```bash
dotnet test --filter "FullyQualifiedName~NombreDelTest"
```

### Ejecutar pruebas con mayor detalle
```bash
dotnet test --logger "console;verbosity=detailed"
```

### Compilar el proyecto
```bash
dotnet build
```

### Restaurar dependencias
```bash
dotnet restore
```

### Ejecutar pruebas con cobertura
```bash
dotnet test /p:CollectCoverage=true
```

## Convención de Commits TDD

Este repositorio usa una notación especial inspirada en Conventional Commits, enfocada en el ciclo TDD:

**Formato:**
```
TDD<Stage><Emoji>: Descripción corta en español
```

**Etapas:**
- **Red (🔴)**: Agrego/modifico pruebas que fallan. Defino comportamiento esperado
- **Green (🟢)**: Implemento lo mínimo para pasar las pruebas rojas
- **Refactor (🔵)**: Mejoro nombres, estructura, diseño sin agregar nuevas pruebas

**Ejemplos:**
```
TDD<Red>🔴: Agregar prueba de sobrepoblación: célula viva con 4 vecinas debe morir
TDD<Green>🟢: Hacer pasar prueba de sobrepoblación con 4 vecinas (muere)
TDD<Refactor>🔵: Simplificar la lógica de evolución de celdas en el juego de la vida
```

**Buenas prácticas:**
- Primera línea concisa (≤ 70 caracteres)
- Verbos en infinitivo: "Agregar", "Refactorizar", "Calcular"
- Cuerpo opcional separado por una línea en blanco si se necesita contexto extra
- Todos los mensajes en español

## Arquitectura y Patrones

### Estructura de Proyectos

Los proyectos TDD en este repositorio siguen una estructura simple:
- Las pruebas y el código de producción pueden estar en el mismo archivo durante las fases iniciales de aprendizaje
- Se enfatiza el desarrollo guiado por pruebas: primero se escribe la prueba, luego el código mínimo

### Patrón de Pruebas

Las pruebas usan el patrón **Arrange-Act-Assert (AAA)**:
```csharp
[Fact]
public void NombreDescriptivoDelComportamiento()
{
    //Arrange - Configurar el escenario
    var entrada = /* ... */;
    var esperado = /* ... */;

    //Act - Ejecutar la acción
    var resultado = /* ... */;

    //Assert - Verificar el resultado
    resultado.Should().BeEquivalentTo(esperado);
}
```

### Evolución del Código

El código evoluciona siguiendo estrictamente el ciclo TDD:
1. **Rojo**: Escribir una prueba que falle
2. **Verde**: Escribir el código mínimo para hacer pasar la prueba
3. **Refactor**: Mejorar el diseño sin cambiar el comportamiento

No se debe mezclar verde con refactor en el mismo commit. Cada etapa debe ser clara y deliberada.

### Triangulación

Las pruebas avanzan mediante triangulación:
- Primero casos simples (célula sin vecinas)
- Luego casos más complejos (vecinas verticales, horizontales, diagonales)
- Finalmente casos completos (tableros completos, osciladores, patrones conocidos)

Esto permite que la implementación emerja de forma natural sin sobre-diseño prematuro.
