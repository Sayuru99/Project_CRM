interface TableHeader {
  text: string;
  align: "start" | "end";
  value: string;
}

const headers: TableHeader[] = [
  { text: "Name", align: "start", value: "name" },
  { text: "Email", align: "start", value: "email" },
];

export { headers };
