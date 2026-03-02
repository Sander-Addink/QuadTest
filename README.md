# Quad Solutions - Trivia
---
In deze repo bevinden zich 2 producten die het probleem oplossen wat betreft de trivia api, de 2 oplossingen zijn:
- SSR
- API
---
## SSR
De Server Side render applicatie (te vinden in ./ServerSideRender/Quad.SessionTrivia) gebruikt encryption om op in de browser de vragen + antwoorden op te slaan, door de rendering server side te houden is de key om dit te decrypten nooit in handen van de client en blijft de data veilig.

### Stappen:
1. Intalleer [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Navigeer naar de Quad.SessionTrivia folder in de terminal
3. Run `dotnet publish -c Release`
4. Navigeer naar bin\Release\net8.0\win-x64\publish en start Quad.SessionTrivia.exe

---
## API
De api oplossing gebruikt een API als middle man om de gevoelige data (antwoorden) eruit te halen totdat de user hier een antwoord op heeft gegeven.

### Stappen
1. Installeer [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Installeer dotnet serve (lokaal runnen frontend) met `dotnet tool install -g dotnet-serve`
3. Navigeer naar de Quad.TriviaApi (./ApiMiddleMan/Backend/Quad.TriviaApi) in de terminal
4. Run `dotnet publish -c Release`
5. Navigeer naar de Quad.ApiTrivia (./ApiMiddleMan/Frontend/Quad.ApiTrivia) in de terminal
6. Run `dotnet publish -c Release`
7. Navigeer naar bin/Release/net8.0/win-x64/publish/wwwroot en run `dotnet serve`
8. Kopieer de url
9. Navigeer naar de Quad.TriviaApi executable (./ApiMiddleMan/Backend/Quad.TriviaApi/bin/Release/net8.0/win-x64/publish)
10. Open appsettings.json en vervang de allowedOrigins url met de gekopierde url en sla dit op
11. Start Quad.TriviaApi.exe