# Garage 2.0

Konsolapplikation som simulerar ett garage. Fordon kan parkeras, hämtas ut, listas och sökas.

## Användning
Programmet startar i huvudmenyn, där man väljer mellan:
- ett tomt garage, där antalet parkeringsplatser anges (1-2000)
- ett garage med förparkerade fordon

## Funktioner
- Parkera och hämta ut fordon
- Lista alla fordon samt antal per fordonstyp
- Söka på regnummer (skiftlägesokänsligt) eller på färg, antal hjul och fordonstyp, enskilt eller i kombination

## Regler
- Regnumret måste vara unikt och ha formatet tre bokstäver och tre siffror (ABC123) eller tre bokstäver, två siffror och en bokstav (ABC12D).
- Fordonen lagras i en privat `List<Vehicle>` i `Garage`. Kapaciteten anges när garaget skapas.

## Projektstruktur
- `Vehicles/`: basklassen `Vehicle`, subklasserna `Car`, `Motorcycle`, `Bus`, `Airplane` och `Boat` samt enums för färg, fordonstyp och bränsle
- `Garages/`: `Garage`, som hanterar fordonen och sökningarna
- `UI/`: menyn (`Menu`) och hjälpklasser för inmatning och skapande av fordon (`VehicleInput`, `VehicleCreator`, `InputHelper`)
- `Program.cs`: startpunkt för programmet
