export type LogEvent =
  | { kind: "connect"; name: string; steamid?: string }
  | { kind: "disconnect"; name: string }
  | { kind: "kill"; attacker: string; victim: string; weapon: string }
  | { kind: "chat"; name: string; text: string }
  | { kind: "unknown"; raw: string };

export function parseLine(line: string): LogEvent {
  const kill = line.match(/"(.+?)"\s+killed\s+"(.+?)"\s+with\s+"(.+?)"/i);
  if (kill) return { kind: "kill", attacker: kill[1], victim: kill[2], weapon: kill[3] };

  const chat = line.match(/say(?:_team)?\s+"(.+?)"\s*:\s*"(.+)"/i);
  if (chat) return { kind: "chat", name: chat[1], text: chat[2] };

  const conn = line.match(/"(.+?)"\s+(?:connected|entered)/i);
  if (conn) return { kind: "connect", name: conn[1] };

  const disc = line.match(/"(.+?)"\s+disconnected/i);
  if (disc) return { kind: "disconnect", name: disc[1] };

  return { kind: "unknown", raw: line };
}
