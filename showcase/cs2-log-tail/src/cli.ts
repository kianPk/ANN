import fs from "node:fs";
import { parseLine } from "./parse.js";

const file = process.argv[2];
if (!file) {
  console.error("Usage: node dist/cli.js <console.log>");
  process.exit(1);
}

let pos = 0;
const tick = () => {
  const st = fs.statSync(file);
  if (st.size < pos) pos = 0;
  if (st.size === pos) return;
  const fd = fs.openSync(file, "r");
  const len = st.size - pos;
  const buf = Buffer.alloc(len);
  fs.readSync(fd, buf, 0, len, pos);
  fs.closeSync(fd);
  pos = st.size;
  for (const line of buf.toString("utf8").split(/\r?\n/)) {
    if (!line.trim()) continue;
    console.log(JSON.stringify({ ts: new Date().toISOString(), ...parseLine(line) }));
  }
};

setInterval(tick, 500);
tick();
