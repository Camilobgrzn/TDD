# TDD

## Propósito

Este repositorio existe para aprender y enseñar Test Driven Development (TDD). La estructura se organiza en dos áreas:

- `Training/`: ejercicios y katas para practicar y reforzar la disciplina TDD.
- `Workshops/`: material para talleres orientados a enseñar TDD paso a paso.

Practico el ciclo: escribir una prueba, verla fallar (Rojo), escribir el mínimo código para que pase (Verde) y mejorar el diseño manteniendo las pruebas en verde (Refactor).

## Reglas de Commits

Uso una notación inspirada en Conventional Commits enfocada en el ciclo TDD. Formato del mensaje:

```
TDD<Stage><Emoji>: Descripción corta en español
```

Donde `<Stage>` ∈ {`Red`, `Green`, `Refactor`} y el emoji ayuda a la lectura rápida.

### Ejemplos

```
TDD<Red>🔴: Convertir valor a decimal
TDD<Green>🟢: Convertir valor a decimal
TDD<Refactor>🔵: Convertir valor a decimal
```

### Significado

- Red (🔴): Agrego/modifico pruebas que fallan. Defino comportamiento esperado.
- Green (🟢): Implemento lo mínimo para pasar las pruebas rojas.
- Refactor (🔵): Mejoro nombres, estructura, diseño sin agregar nuevas pruebas.

### Buenas prácticas

- Primera línea concisa (≤ 70 caracteres).
- Verbos en infinitivo: "Agregar", "Refactorizar", "Calcular".
- Cuerpo opcional separado por una línea en blanco si necesito contexto extra.

### Ejemplo con cuerpo

```
TDD<Red>🔴: Calcular total con descuentos

Pruebas para múltiples niveles de descuento: 0%, 5%, 10%, 15%.
Casos límite incluidos.
```

## Estructura

```
Training/
	README.md
Workshops/
	README.md
```


Amplío y adapto este documento según evoluciona la práctica. Mantengo consistencia en la notación de commits para facilitar revisión y aprendizaje.