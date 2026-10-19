# CS2 Log Tail

Parse & tail CS2 / Source dedicated **console logs** — kills, connects, chat, rcon.

Useful on game nodes when you want structured JSON lines for shipping to Loki/Grafana or a panel.

```bash
npm i && npm run build
node dist/cli.js /opt/5stack/servers/*/logs/console.log
```

MIT · [@kianPk](https://github.com/kianPk)
