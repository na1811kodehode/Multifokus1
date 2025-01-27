# README

## Oppgave 1: Temperaturkonverterer (Multi-Plattform Oppgave)

### Beskrivelse
I denne oppgaven bygde jeg en temperaturkonverterer som lar brukeren konvertere mellom Fahrenheit, Celsius og Kelvin. Brukeren kan skrive inn en temperaturverdi og spesifisere enheten å konvertere fra og til, som f.eks. Celsius til Fahrenheit, eller Kelvin til Fahrenheit. Programmet håndterer også feilmeldinger, for eksempel hvis brukeren oppgir ugyldig input eller en temperatur under absolutt null.

### Løsning
Backend-delen er utviklet ved hjelp av ASP.NET Core. Jeg lagde en API som godtar en temperaturverdi og de valgte enhetene for konvertering (fra og til). Hvis inputen er ugyldig eller under absolutt null, returnerer API-en en feilmelding.

Frontend-delen bruker HTML og JavaScript. Brukeren kan skrive inn temperaturen, velge enhetene de vil konvertere fra og til, og deretter trykke på en knapp for å få konvertert verdien. Jeg brukte JavaScript til å sende dataene til backend og vise resultatet på skjermen.

### Implementerte funksjoner:
- Konvertering mellom Fahrenheit, Celsius og Kelvin i enhver kombinasjon.
- Feilhåndtering for ugyldig input og temperatur under absolutt null.
- Enkel og intuitiv frontend med Bootstrap for bedre brukeropplevelse.

---

## Oppgave 2: Blomsterkarusell og Kort (Front-End Fokus)

### Beskrivelse
Denne oppgaven fokuserte på frontend-utvikling ved bruk av HTML, CSS og Bootstrap. Målet var å lage en nettside som inneholder:
1. En karusell som viser bilder av blomster.
2. Kort som presenterer blomsterbilder med tilhørende beskrivelser, og som inneholder en interaktiv 3D-flippeffekt ved hover.

### Funksjonalitet:
- **Karusell**: Vist fem bilder av forskjellige blomster i en roterende karusell, med kontrollknapper for å navigere mellom bildene.
- **Interaktive kort**: Blomstene er også presentert på kort som kan flips til baksiden ved hover, som viser en kort beskrivelse av hver blomst.

### Løsning:
Jeg brukte HTML for strukturen, CSS for styling, og JavaScript for å legge til dynamiske effekter som karusellen og flippeffekten. Bootstrap ble brukt for å gjøre siden responsiv og for å håndtere karusellens layout.

---

## Oppgave 3: Fotballag Manager (Back-End Fokus)

### Beskrivelse
I denne oppgaven skulle jeg lage et backend-system for å administrere et fotballag og dets spillere. Funksjonalitet for å legge til, oppdatere og fjerne spillere ble implementert, samt muligheten til å vise spillerens informasjon.

### Løsning:
Backend-systemet ble utviklet med ASP.NET Core. Jeg opprettet API-endepunkter for å håndtere CRUD-operasjoner på spillere og lag, samt en enkel datalagring for spillerne.

### Implementerte funksjoner:
- Legge til spillere.
- Oppdatere spillerinformasjon.
- Slette spillere.
- Hente spillernes informasjon etter ulike kriterier.

---

Alle tre prosjektene fokuserte på å utvikle brukervennlige og responsive løsninger, med både frontend og backend som jobber sammen for å levere en helhetlig opplevelse for brukeren.
