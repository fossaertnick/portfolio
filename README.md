# Naam van jouw project

    MEMORIÁ

## Projectbeschrijving

De app laat je je leuke momenten opslaan. Wanneer gewenst, nu of later, kun je je herinneringen opslaan gebaseerd op een locatie. Hiermee kan je foto materiaal bijsturen als bewijs en een beschrijving van wat of hoe. De locatie en ingave kan 'gedaan' worden via tekst of bij druk op de knop/map (voor locatie)  FYI: speech-to-text is enkel in het engels en gebruikt de keywords die in de entries/editor staan (name, country, city, place, number, description). De andere pages werken in service tot deze memorias. Detailspage, statisticspage, manualpage, mappage, listpage, 

## Extra info
Plaats hier de nodig informatie om het
project te kunnen uitvoeren:
- Dev tunnel : Source of truth 
        - "https://06dfrpsm-44338.brs.devtunnels.ms/api/"

- API keys of nodige secrets (API key voor de google services (MAP && VOICE))
        - public const string GeoCodeaApiKey = "AIzaSyD6BQDVKAtnEm6N3LFjEm2s3XY_nDujX8U";

- Logingegevens 
        - De applicatie werkt apparaat specifiek. Alle 'Memorias' zullen gelogd worden in de db     onder dit specifieke apparaat. Bij verwijderen van apparaat worden de instanties ook verwijderd.

- Bronmateriaal
        - Cursus, internet, en een aangekocht boek (.Net Maui Cross-platform Application Development)

(vast waarden die vaak voorkomen heb ik geplaatst in een klasse genaamd 'Constants' gelokaliseerd in de data folder van het .Core project. (van de mobile))