# CS2 Map Vote

Lightweight **map vote** engine for CS2 Public servers — nominate, vote, runoff.

Designed to sit next to YGuard plugins: reads a map pool JSON, tallies chat votes, returns the winner.

## Flow
1. Admin/auto opens vote with a pool of N maps
2. Players `!vote <n>`
3. Highest tally wins (tie → random among tied)

## Pool example
```json
{ "maps": ["de_mirage", "de_inferno", "de_anubis", "de_nuke", "de_overpass"] }
```

MIT · [@kianPk](https://github.com/kianPk)
