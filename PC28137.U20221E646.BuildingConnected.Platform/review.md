# Método para cualquier PC de Aplicaciones Web

> RESTful API en C# / .NET / ASP.NET Core + MySQL, con DDD, bounded contexts, arquitectura por
> capas, CQRS, Resource y Assembler. 100 minutos, 20 puntos.
>
> El caso cambia. La forma no.

---

## Lo invariante

Todos estos exámenes son la misma figura: **un aggregate, unos value objects que se validan
solos, un endpoint de escritura, y reglas de negocio que son validaciones**. Lo que varía entre
un enunciado y otro es cuántos value objects hay, qué validan, y cómo se llaman las cosas.

Por eso el orden se memoriza una vez y sirve para todas.

## La regla que ordena todo

**Las dependencias apuntan hacia adentro.**

```
Interfaces  →  Application  →  Domain  ←  Infrastructure
```

El dominio no importa nada. Todo lo demás importa dominio. Escribes de adentro hacia afuera y
nunca tienes una referencia rota en pantalla.

Corolario práctico: **nunca escribas un archivo cuyas dependencias no existan todavía.** Si lo
haces, el editor se llena de rojo y pierdes minutos distinguiendo errores reales de errores de
orden.

---

## Por qué empiezas en Shared

Dos razones, y la segunda es la que de verdad importa.

**La primera**: el enunciado lo pide. El constraint del *shared bounded context* dice que su
contenido debe seguir las especificaciones del proyecto de ejemplo `learning-center-platform`
visto en clase, y en los enunciados recientes es explícitamente el **primer feature**.

**La segunda**: el Shared **no depende del caso**. Es idéntico en todos los enunciados. Son los
primeros veinte minutos que puedes tipear de memoria, sin pensar, mientras tu cabeza todavía está
digiriendo el enunciado. Estás produciendo y leyendo a la vez.

Si empiezas por el dominio, tienes que haber entendido el caso entero antes de escribir la primera
línea. Ahí se va el tiempo.

### El Shared se parte en dos

Todo el Shared es independiente del contexto **salvo un archivo**: el `AppDbContext`, que llama a
la configuración del bounded context. Escríbelo con esa línea **comentada** y descoméntala cuando
el archivo de configuración exista.

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);
    // builder.ApplyObservabilityConfiguration();   ← se descomenta en el paso 22
    builder.UseSnakeCaseNamingConvention();
}
```

Olvidar ese paso produce un 500 al hacer el POST, porque EF no conoce la entidad. Y lo vas a
buscar en el repositorio en vez de en el `AppDbContext`.

### Archivo por archivo

| # | Archivo | Para qué sirve |
|---|---|---|
| 1 | `Domain/Repositories/IUnitOfWork` | Declara "esta operación se confirma entera o no se confirma". Vive en Domain porque el dominio decide *cuándo* termina una operación; la infraestructura solo sabe *cómo*. |
| 2 | `Domain/Model/ValueObjects/*` | Conceptos que más de un contexto usaría: dinero, rangos de fechas. Solo van aquí si el enunciado lo dice. |
| 3 | `…/Configuration/Extensions/ModelBuilderExtensions` | Traduce nomenclatura C# a nomenclatura de base en un solo sitio. Sin él escribirías `.ToTable("alert_rules")` entidad por entidad. |
| 4 | `…/Configuration/DateOnlyConverter` | Parcha la limitación del provider de MySQL al leer. Solo si hay `DateOnly` o `DateTimeOffset` en el modelo. |
| 5 | `…/Configuration/AppDbContext` | La única puerta a la base. Ensambla la configuración de cada contexto. |
| 6 | `…/Repositories/BaseRepository<T>` | El CRUD que todos repiten. Existe para que cada repositorio concreto escriba solo sus consultas propias. |
| 7 | `…/Repositories/UnitOfWork` | Implementa el contrato: `SaveChangesAsync`. |
| 8 | `Interfaces/ASP/Configuration/KebabCaseRouteNamingConvention` | Nomenclatura de URLs en un solo sitio. |
| 9 | `Infrastructure/Localization/MessageCatalog` | Mensajes según la cultura negociada por `Accept-Language`. |
| 10 | `Interfaces/ASP/Configuration/GlobalExceptionHandler` | El único lugar que sabe traducir excepción → código HTTP. Mantiene los controllers limpios. |

---

## El bounded context, en orden y con su razón

**1 · Value objects.** Un concepto sin identidad propia que **se valida al nacer**: si la
instancia existe, es válida. Aquí viven la mayoría de las reglas del enunciado. Van primero
porque no dependen de nada y porque el aggregate los instancia.

**2 · El enum de estado.** El conjunto cerrado de valores. Declara el valor por defecto
explícitamente en `0`, para que el código y la regla digan lo mismo.

**3 · El Command.** La intención de cambio, en tipos primitivos. Es la *forma del pedido*, no la
del modelo. Va antes que el aggregate porque el constructor del aggregate lo recibe.

**4 · El Aggregate.** La raíz que custodia la consistencia. Su constructor es la puerta: o
construye algo válido o lanza. Nada llega a la base sin pasar por ahí. Si el enunciado pide
auditoría, el `partial` con `CreatedAt` y `UpdatedAt` va aquí mismo.

**5 · `IRepository`.** Lo que el dominio necesita **preguntarle** al almacenamiento, escrito desde
el punto de vista del dominio: `ExistsByIdentifierAsync`, no `Query<T>`. Vive en Domain para que
el dominio no dependa de Entity Framework.

**6 · `ICommandService`.** La puerta de entrada de las escrituras. El controller depende de esto,
nunca del repositorio.

**7 · `ModelBuilderExtensions` del contexto (Fluent API).** Cómo el aggregate se mapea a tablas y
columnas, y dónde los value objects se declaran como *owned types*. Está separado para que el
dominio jamás importe EF: eso es lo que el enunciado llama *separación de Domain Concepts y
Persistence Concepts*.

**8 · El repositorio concreto.** El único archivo con consultas EF de este aggregate.

**9 · El command service.** La orquestación: el aggregate valida, el repositorio responde, el unit
of work confirma.

**10 y 11 · Los dos Resources.** El contrato HTTP de entrada y el de salida. Separados del Command
y del Aggregate para que la API cambie sin tocar el dominio. El de salida lleva **exactamente** los
campos que enumera el enunciado: ni uno más.

**12 y 13 · Los dos Assemblers.** Resource → Command, y Aggregate → Resource. Son las fronteras de
traducción; sin ellos el controller terminaría conociendo el dominio.

**14 · El Controller.** Traduce HTTP a una llamada, y un resultado a un código de estado. Cero
lógica.

---


## Si el enunciado exige Git

Los enunciados recientes califican el historial como evidencia de autoría, y pueden **anular la
entrega**: un único commit masivo, o commits fuera del horario de la evaluación, invalidan.

- Conventional Commits en inglés con scope: `feat(observability): add alert rule aggregate`
- Git Flow: una rama `feature/*` por bloque, merge a `develop` con `--no-ff`
- Un commit al terminar **cada archivo**, no cada cierto tiempo
- La URL del repositorio en una sección `Authorship` del README, después del título y de Overview
- Al final: release, y `main` y `develop` apuntando al mismo commit

```bash
git log --oneline -1 main
git log --oneline -1 develop   # los dos hashes deben coincidir
```

---


El ciclo, que son tres comandos
bash
git status                    # qué cambió — úsalo antes de cada commit
git add .                     # todo lo nuevo y lo modificado
git commit -m "feat(scope): mensaje en inglés"

Memoriza add . + commit -m, no commit -am. En el examen todo es nuevo.

Arranque
bash
git init
git branch -M main
git remote add origin <URL-del-repo-del-profesor>

git add .
git commit -m "chore: initialize project structure"
git push -u origin main

git checkout -b develop
git push -u origin develop
Ramas
bash
git checkout -b feature/shared-kernel     # crear y cambiarse
git checkout develop                      # cambiarse a una que existe
git merge --no-ff feature/shared-kernel   # fusionar dejando huella de la rama
git branch                                # en cuál estoy

El --no-ff importa: sin él, si la rama no divergió, Git mueve el puntero y la rama desaparece
del historial. Con Git Flow calificado, quieres que se vea.

Release
bash
git checkout -b release/1.0.0
git commit --allow-empty -m "chore(release): prepare version 1.0.0"
git checkout main
git merge --no-ff release/1.0.0
git tag -a v1.0.0 -m "Release 1.0.0"
git checkout develop
git merge --no-ff main
git push origin main develop --tags
Verificar
bash
git log --oneline --graph --all     # el historial como lo va a ver el profesor
git log --oneline -1 main
git log --oneline -1 develop        # los dos hash