# Garage 2.0

Konsolapplikation som simulerar ett garage. Fordon kan parkeras, hämtas ut, listas och sökas.

## Köra programmet
1. Klona repot och öppna `Garage-ovning5.slnx` i Visual Studio, tryck F5.
   Eller kör `dotnet run` i projektmappen.
2. Välj i huvudmenyn mellan tomt garage och garage med förparkerade fordon.

## Kända begränsningar
- Alla fordon tar en parkeringsplats, oavsett typ.
- Skapandet av garage har du en kapacitet på mellan 1-2000 (egen avgränsning) 

## Funktioner
- Parkera och hämta ut fordon
- Lista alla fordon samt antal per fordonstyp
- Söka på regnummer (skiftlägesokänsligt)
- Söka på på färg, antal hjul och fordonstyp, enskilt eller i kombination

## Regler
- Regnumret måste vara unikt och ha formatet tre bokstäver och tre siffror (ABC123) eller tre bokstäver, två siffror och en bokstav (ABC12D).
- Fordonen lagras i en privat `List<Vehicle>` i `Garage`. Kapaciteten anges när garaget skapas.

## Projektstruktur
- `Vehicles/`: basklassen `Vehicle`, subklasserna `Car`, `Motorcycle`, `Bus`, `Airplane` och `Boat` samt enums för färg, fordonstyp och bränsle
- `Garages/`: `Garage`, som hanterar fordonen och sökningarna
- `UI/`: menyn (`Menu`) och hjälpklasser för inmatning och skapande av fordon (`VehicleInput`, `VehicleCreator`, `InputHelper`)
- `Program.cs`: startpunkt för programmet

## Jag vill ha feedback på
Allt ni vill ge feedback på är välkommet, men nedan är det jag själv funderat över.

**Arkitektur och ansvar**
- `Garage.ListAllVehicles` skriver ut direkt i konsolen. Borde `Garage` i stället returnera fordonen och låta `Menu` sköta utskriften?
- Nästan alla `Handle`-metoder i `Menu` börjar med samma kontroll `_garage == null`. Finns det ett smidigare sätt?

**Garage och sökning**
- `SearchByProperties` returnerar `null` när inget matchar. Vore en tom lista tydligare?
- `HandleParkVehicle` kontrollerar fullt garage och dubblett av regnummer, och sedan gör `ParkVehicle` samma kontroller igen och returnerar bara `false`. Är det dubbelarbete?

**Felhantering och robusthet**
- Är valideringen (i properties och vid inmatning) på rätt ställe, eller är den duplicerad? Kapaciteten kontrolleras till exempel både i `Menu` och i `Garage`-konstruktorn.

