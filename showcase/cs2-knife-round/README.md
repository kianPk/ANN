# CS2 Knife Round

CounterStrikeSharp-oriented **knife round** controller for CS2 dedicated servers.

Winner of the knife round picks side (`!ct` / `!t`). Loses idle on Practice pods via `SERVER_TYPE`.

## Commands
- `!knife` — force start (admin)
- `!ct` / `!t` — side pick after win
- `css_knife_status` — RCON status

## Config
```json
{ "PublicOnly": false, "IdleOnPractice": true, "PickTimeoutSeconds": 30 }
```

## Build
```bash
dotnet build -c Release
```

MIT · [@kianPk](https://github.com/kianPk)
